using System.Net.Http;
using System.Threading.Tasks;

namespace DiscordBot.Bot.Common
{
    public class HttpClientWrapper
    {
        private static readonly HttpClient Client = new HttpClient();

        public Task<HttpResponseMessage> GetAsync(string url)
        {
            return Client.GetAsync(url);
        }
    }
}