using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DiscordBot.Bot.Common;
using Newtonsoft.Json;

namespace DiscordBot
{
    public class DogPicProvider
    {
        private static readonly Random _random = new Random();
        private static readonly HttpClientWrapper _client = new HttpClientWrapper();

        private static readonly List<string[]> _breeds = new List<string[]>
        {
            new[] { "keeshond" },
            new[] { "malamute" },
            new[] { "husky" },
            new[] { "eskimo" },
            new[] { "retriever", "golden" },
            new[] { "retriever", "flatcoated" },
            new[] { "samoyed" },
            new[] { "malinois" },
            new[] { "pyrenees" },
            new[] { "mountain", "bernese" },
            new[] { "mountain", "swiss" },
            new[] { "labrador" },
            new[] { "collie", "border" },
            new[] { "germanshepherd" },
            new[] { "sheepdog", "shetland" },
            new[] { "pomeranian" }
        };

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
                var breedSpecifier = GetBreedSpecifier();
                var breedUrl = $"https://dog.ceo/api/breed/{breedSpecifier}/images/random";
                var response = await _client.GetAsync(breedUrl);
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

        private static string GetBreedSpecifier()
        {
            var index = _random.Next(0, _breeds.Count);
            var breedSpecifier = string.Join("/", _breeds[index]);
            return breedSpecifier;
        }

        private class Result
        {
            public string Message { get; set; }
            public string Status { get; set; }
        }
    }
}