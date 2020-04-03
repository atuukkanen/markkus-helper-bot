using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordBot.Bot.MessageHandlers
{
    public abstract class BaseHandler : IMessageHandler
    {
        public async Task MessageReceived(SocketMessage message, SocketCommandContext context)
        {
            try
            {
                var reply = GetMessageReply(message.Content, context);
                if (reply == null)
                {
                    return;
                }

                await message.Channel.SendMessageAsync(reply);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + "\n" + e.StackTrace);
            }
        }

        protected virtual string GetMessageReply(string content, SocketCommandContext context)
        {
            return null;
        }
    }
}