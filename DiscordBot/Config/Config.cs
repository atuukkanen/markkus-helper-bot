namespace DiscordBot.Configuration
{
    public class Config
    {
        private static Config _config;

        private Config()
        {
        }

        public static Config Instance => _config ?? (_config = ReadConfig());

        public DiscordConfig Discord { get; set; }

        private static Config ReadConfig()
        {
            var newConfig = new Config
            {
                Discord = new DiscordConfig
                {
                    ClientToken = "",
                    DarkroomApiToken = ""
                }
            };
            return newConfig;
        }

        public class DiscordConfig
        {
            public string ClientToken { get; set; }
            public string DarkroomApiToken { get; set; }
        }
    }
}