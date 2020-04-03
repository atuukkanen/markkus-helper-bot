using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DiscordBot
{
    public class DogPicProvider
    {
        private static readonly HttpClient _client = new HttpClient();
        private string _filePath;

        public DogPicProvider()
        {
            GetNext();
        }

        public string GetDogFilePath()
        {
            var path = _filePath;
            GetNext();
            return path;
        }

        private async Task GetNext()
        {
            try
            {
                var response = await _client.GetAsync("https://dog.ceo/api/breed/keeshond/images/random");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<Result>(responseBody);
                if (result.Status != "success")
                {
                    return;
                }

                var url = result.Message;
                var tempPath = Path.GetTempPath();
                var guid = Guid.NewGuid();
                var tempFilePath = Path.Combine(tempPath, $"{guid}.jpg");
                using (var client = new WebClient())
                {
                    client.DownloadFile(new Uri(url), tempFilePath);
                }

                _filePath = tempFilePath;
            }
            catch (HttpRequestException e)
            {
            }
        }

        private class Result
        {
            public string Message { get; set; }
            public string Status { get; set; }
        }
    }
}