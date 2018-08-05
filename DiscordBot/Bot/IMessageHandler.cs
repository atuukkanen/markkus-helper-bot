using Discord.WebSocket;
using System.Threading.Tasks;

namespace DiscordBot.Bot.MessageHandlers
{
    internal interface IMessageHandler
    {
        Task MessageReceived(SocketMessage message);
    }
}