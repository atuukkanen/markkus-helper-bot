namespace DiscordBot.Configuration
{
    public class Config
    {
        private Config()
        {
        }

        private static Config _config = null;

        public static Config Instance
        {
            get
            {
                if (_config == null)
                {
                    _config = ReadConfig();
                }
                return _config;
            }
        }

        private static Config ReadConfig()
        {
            var newConfig = new Config();
            newConfig.Discord = new DiscordConfig { ClientToken = "" };
            return newConfig;
        }

        public class DiscordConfig
        {
            public string ClientToken { get; set; }
        }

        public DiscordConfig Discord { get; set; }
    }
}