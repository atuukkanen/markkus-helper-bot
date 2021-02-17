using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Discord.WebSocket;
using DiscordBot.Bot.Common;
using DiscordBot.Configuration;

namespace DiscordBot.Bot.StateChangeHandlers
{
    public class VoiceStateChangeHandler : IStateChangeHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly HttpClientWrapper _httpClientWrapper = new HttpClientWrapper();

        public VoiceStateChangeHandler(DiscordSocketClient client)
        {
            _client = client;
        }

        public async Task Initialize()
        {
            await Refresh();
        }

        public async Task OnStateChanged()
        {
            await Refresh();
        }

        private async Task Refresh()
        {
            var guild = _client.Guilds.Count > 1
                ? _client.Guilds.FirstOrDefault(x => x.Name == "KameraBurgerit")
                : _client.Guilds.FirstOrDefault();

            if (guild == null)
            {
                return;
            }

            var someoneActive = guild.Channels.Where(x => x is SocketVoiceChannel).Any(x => x.Users.Any(u => !u.IsBot));

            var updatePath = @"https://api.ttkamerat.fi/darkroom/api/v1/sensors/post?value="
                             + (someoneActive ? 1 : 0)
                             + @"&sensor=voice1&token="
                             + Config.Instance.Discord.DarkroomApiToken;
            await _httpClientWrapper.GetAsync(updatePath);
        }
    }
}
