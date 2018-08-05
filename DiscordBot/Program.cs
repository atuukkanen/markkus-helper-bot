using Discord;
using DiscordBot.Bot;
using DiscordBot.Bot.MessageHandlers;
using System;
using System.Threading.Tasks;
using DiscordBot.Configuration;

namespace DiscordBot
{
    class Program
    {
        public static void Main(string[] args)
               => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            Console.WriteLine("Starting...");

            var token = Config.Instance.Discord.ClientToken;
            var bot = new MyDiscordBot("");
            bot.AddHandler("ping", new PingHandler());
            bot.AddHandler("doge", new DogPicHandler());

            Console.WriteLine("Running bot...");

            await bot.Run().ConfigureAwait(false);
        }

        private Task Log(LogMessage msg)
        {
            return Task.CompletedTask;
        }
    }
}
