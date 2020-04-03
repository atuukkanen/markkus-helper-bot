using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordBot.Bot.MessageHandlers
{
    internal interface IMessageHandler
    {
        Task MessageReceived(SocketMessage message, SocketCommandContext context);
    }
}