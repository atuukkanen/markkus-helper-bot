using Discord;
using Discord.WebSocket;
using DiscordBot.Bot.MessageHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBot.Bot
{
    internal class MyDiscordBot
    {
        private DiscordSocketClient _client = new DiscordSocketClient();
        private readonly string _token;

        private readonly Dictionary<string, IMessageHandler> _handlers = new Dictionary<string, IMessageHandler>();

        public MyDiscordBot(string token)
        {
            _token = token;
            _client.MessageReceived += MessageReceived;
        }

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
            var prefix = message.Content.Split(' ').First();

            if (_handlers.ContainsKey(prefix))
            {
                await _handlers[prefix].MessageReceived(message).ConfigureAwait(false);
            }
        }

        public Task Run()
        {
            return Task.Run(async () =>
            {
                await _client.LoginAsync(TokenType.Bot, _token).ConfigureAwait(false);
                await _client.StartAsync().ConfigureAwait(false);
                await Task.Delay(-1);
            });
        }
    }
}