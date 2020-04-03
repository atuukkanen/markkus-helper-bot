using System;

namespace DiscordBot.Bot.Model
{
    internal class Issue
    {
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTimeOffset Created { get; set; }

        public override string ToString()
        {
            var str = "";
            str += $"Author: {Author}\n";
            str += $"Created: {Created.ToString()}\n";
            str += $"Description: {Description}";
            return str;
        }
    }
}