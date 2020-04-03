using System;
using System.Threading.Tasks;
using Discord;
using DiscordBot.Bot;
using DiscordBot.Bot.MessageHandlers;
using DiscordBot.Configuration;

namespace DiscordBot
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public async Task MainAsync()
        {
            Console.WriteLine("Starting...");

            var token = Config.Instance.Discord.ClientToken;
            var bot = new MyDiscordBot(token);
            bot.AddHandler("ping", new PingHandler());
            bot.AddHandler("doge", new DogPicHandler());
            bot.AddHandler("voice", new VoiceChannelDataHandler(bot.Client));

            var issueHandler = new IssueHandler();
            bot.AddHandler(issueHandler.Prefix, issueHandler);

            Console.WriteLine("Running bot...");

            await bot.Run().ConfigureAwait(false);
        }

        private Task Log(LogMessage msg)
        {
            return Task.CompletedTask;
        }
    }
}