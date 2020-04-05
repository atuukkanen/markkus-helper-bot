using System.Linq;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordBot.Bot.MessageHandlers
{
    internal class VoiceChannelDataHandler : BaseHandler
    {
        private readonly DiscordSocketClient _client;

        public VoiceChannelDataHandler(DiscordSocketClient client)
        {
            _client = client;
        }

        protected override string GetMessageReply(string content, SocketCommandContext context)
        {
            var guild = _client.Guilds.Count > 1
                ? _client.Guilds.FirstOrDefault(x => x.Name == "KameraBurgerit")
                : _client.Guilds.FirstOrDefault();

            if (guild == null)
            {
                return "Apua";
            }

            var someoneActive = guild.Channels.Where(x => x is SocketVoiceChannel).Any(x => x.Users.Any(u => !u.IsBot));
            return someoneActive ? "Jeij!" : "Sniff.";
        }
    }
}