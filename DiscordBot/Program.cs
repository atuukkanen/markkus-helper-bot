using System;
using System.Threading.Tasks;
using Discord;
using DiscordBot.Bot;
using DiscordBot.Bot.MessageHandlers;
using DiscordBot.Bot.StateChangeHandlers;
using DiscordBot.Configuration;

namespace DiscordBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        async Task MainAsync()
        {
            Console.WriteLine("Starting...");

            var token = Config.Instance.Discord.ClientToken;
            var bot = new MyDiscordBot(token);
            bot.AddCommandHandler("ping", new PingHandler());
            bot.AddCommandHandler("doge", new DogPicHandler());
            bot.AddCommandHandler("voice", new VoiceChannelDataHandler(bot.Client));
            bot.AddStateChangeHandler(new VoiceStateChangeHandler(bot.Client));

            var issueHandler = new IssueHandler();
            bot.AddCommandHandler(issueHandler.Prefix, issueHandler);

            Console.WriteLine("Running bot...");

            await bot.Run().ConfigureAwait(false);
        }

        private Task Log(LogMessage msg)
        {
            return Task.CompletedTask;
        }
    }
}