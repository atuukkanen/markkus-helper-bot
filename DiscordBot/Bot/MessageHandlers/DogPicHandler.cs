using System;
using System.Collections.Generic;

namespace DiscordBot.Bot.MessageHandlers
{
    internal class DogPicHandler : BaseHandler
    {
        private readonly List<string> _links = new List<string>();
        private readonly static Random random = new Random();

        public DogPicHandler()
        {
            _links.Add("https://pbs.twimg.com/profile_images/639599645925076994/7Egv8qXQ.jpg");
            _links.Add("http://cdn2-www.dogtime.com/assets/uploads/gallery/siberian-husky-dog-breed-pictures/thumbs/thumbs_siberian-husky-dog-breed-pictures-3.jpg");
            _links.Add("https://upload.wikimedia.org/wikipedia/commons/thumb/a/a7/Czech-wolfdog.jpg/220px-Czech-wolfdog.jpg");
        }

        protected override string GetMessageReply(string content)
        {
            if (_links.Count == 0)
            {
                return null;
            }

            var index = random.Next(_links.Count);
            return "<" + _links[index] + ">";
        }
    }
}