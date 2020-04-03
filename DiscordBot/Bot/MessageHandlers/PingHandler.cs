using Discord.Commands;

namespace DiscordBot.Bot.MessageHandlers
{
    public class PingHandler : BaseHandler
    {
        protected override string GetMessageReply(string content, SocketCommandContext context)
        {
            return "pong";
        }
    }
}