using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace DiscordBot.Bot.MessageHandlers
{
    public abstract class BaseHandler : IMessageHandler
    {
        public async Task MessageReceived(SocketMessage message)
        {
            try
            {
                var reply = GetMessageReply(message.Content);
                if (reply == null)
                {
                    return;
                }
                await message.Channel.SendMessageAsync(reply);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e?.Message + "\n" + e?.StackTrace);
            }
        }

        protected virtual string GetMessageReply(string content) => null;
    }
}