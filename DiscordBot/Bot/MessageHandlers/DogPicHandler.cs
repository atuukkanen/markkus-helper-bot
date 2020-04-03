using Discord.Commands;

namespace DiscordBot.Bot.MessageHandlers
{
    internal class DogPicHandler : BaseHandler
    {
        private readonly DogPicProvider _dogPicProvider;

        public DogPicHandler()
        {
            _dogPicProvider = new DogPicProvider();
        }

        protected override string GetMessageReply(string content, SocketCommandContext context)
        {
            var path = _dogPicProvider.GetDogFilePath();
            if (path == null)
            {
                return "Sniff, ei onnistunut nyt.";
            }

            var channel = context.Channel;
            channel.SendFileAsync(path);
            return null;
        }
    }
}