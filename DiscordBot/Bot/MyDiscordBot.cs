using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Bot.MessageHandlers;

namespace DiscordBot.Bot
{
    internal class MyDiscordBot
    {
        private readonly Dictionary<string, IMessageHandler> _handlers = new Dictionary<string, IMessageHandler>();
        private readonly string _token;

        public MyDiscordBot(string token)
        {
            _token = token;
            Client.MessageReceived += MessageReceived;
        }

        public DiscordSocketClient Client { get; } = new DiscordSocketClient();

        public void AddHandler(string prefix, IMessageHandler handler)
        {
            if (_handlers.ContainsKey(prefix))
            {
                throw new ArgumentException(nameof(prefix));
            }

            _handlers.Add(prefix, handler);
        }

        private async Task MessageReceived(SocketMessage message)
        {
            var context = new SocketCommandContext(Client, message as SocketUserMessage);

            var prefix = message.Content.Split(' ').First();

            if (_handlers.ContainsKey(prefix))
            {
                await _handlers[prefix].MessageReceived(message, context).ConfigureAwait(false);
            }
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