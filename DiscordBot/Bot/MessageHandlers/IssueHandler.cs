using System.Collections.Generic;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Bot.Model;

namespace DiscordBot.Bot.MessageHandlers
{
    public class IssueHandler : IMessageHandler
    {
        private static readonly ICollection<Issue> issues = new List<Issue>();
        public readonly string Prefix = "issue";

        public async Task MessageReceived(SocketMessage message, SocketCommandContext context)
        {
            var trimmed = message.Content.Trim().ToLower().Substring(Prefix.Length).Trim();
            var firstPart = trimmed.Split(' ')[0];

            if (firstPart == string.Empty || firstPart == Commands.Get)
            {
                await HandleGet(message).ConfigureAwait(false);
            }
            else if (firstPart == Commands.Add)
            {
                var rest = trimmed.Replace(firstPart, "").TrimStart();
                await HandleAdd(message, rest).ConfigureAwait(false);
            }
            else
            {
                await HandleInvalidCommand(message, firstPart).ConfigureAwait(false);
            }
        }

        private Task HandleInvalidCommand(SocketMessage message, string firstPart)
        {
            return message.Channel.SendMessageAsync($"Invalid command: {firstPart}");
        }

        private Task HandleAdd(SocketMessage message, string issueDescription)
        {
            var newIssue = new Issue
            {
                Author = message.Author.Username,
                Created = message.CreatedAt,
                Description = issueDescription
            };
            issues.Add(newIssue);

            return message.Channel.SendMessageAsync("New issue added.");
        }

        private Task HandleGet(SocketMessage message)
        {
            var issueString = "";
            foreach (var issue in issues)
            {
                issueString += issue + "\n\n";
            }

            issueString = issueString.Trim();

            if (issueString == string.Empty)
            {
                issueString = "No issues found.";
            }

            return message.Channel.SendMessageAsync(issueString);
        }

        private static class Commands
        {
            public const string Get = "get";
            public const string Add = "add";
        }
    }
}