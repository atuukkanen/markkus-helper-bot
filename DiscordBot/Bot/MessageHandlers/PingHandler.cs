namespace DiscordBot.Bot.MessageHandlers
{
    public class PingHandler : BaseHandler
    {
        protected override string GetMessageReply(string content)
        {
            return "pong";
        }
    }
}