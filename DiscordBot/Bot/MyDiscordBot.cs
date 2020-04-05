using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Bot.MessageHandlers;
using DiscordBot.Bot.StateChangeHandlers;

namespace DiscordBot.Bot
{
    internal class MyDiscordBot
    {
        private readonly Dictionary<string, IMessageHandler> _commandHandlers = new Dictionary<string, IMessageHandler>();
        private readonly List<IStateChangeHandler> _stateChangeHandlers = new List<IStateChangeHandler>();
        private readonly string _token;

        public MyDiscordBot(string token)
        {
            _token = token;
            Client.MessageReceived += MessageReceived;
            Client.UserVoiceStateUpdated += UserVoiceStateUpdated;
            Client.Ready += async () => { await Initialize(); };
        }

        public DiscordSocketClient Client { get; } = new DiscordSocketClient();

        public void AddCommandHandler(string prefix, IMessageHandler handler)
        {
            if (_commandHandlers.ContainsKey(prefix))
            {
                throw new ArgumentException(nameof(prefix));
            }

            _commandHandlers.Add(prefix, handler);
        }

        public void AddStateChangeHandler(IStateChangeHandler handler)
        {
            _stateChangeHandlers.Add(handler);
        }

        private async Task Initialize()
        {
            foreach (var handler in _stateChangeHandlers)
            {
                await handler.Initialize();
            }
        }

        private async Task MessageReceived(SocketMessage message)
        {
            var context = new SocketCommandContext(Client, message as SocketUserMessage);

            var prefix = message.Content.Split(' ').First();

            if (_commandHandlers.ContainsKey(prefix))
            {
                await _commandHandlers[prefix].MessageReceived(message, context).ConfigureAwait(false);
            }
        }

        private Task UserVoiceStateUpdated(SocketUser arg1, SocketVoiceState arg2, SocketVoiceState arg3)
        {
            foreach (var handler in _stateChangeHandlers)
            {
                handler.OnStateChanged();
            }

            return Task.CompletedTask;
        }

        public Task Run()
        {
            return Task.Run(async () =>
            {
                await Client.LoginAsync(TokenType.Bot, _token).ConfigureAwait(false);
                await Client.StartAsync().ConfigureAwait(false);
                await Task.Delay(-1);
            });
        }
    }
}