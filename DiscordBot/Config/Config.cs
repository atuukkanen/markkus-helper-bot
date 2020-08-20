using System;

namespace DiscordBot.Configuration
{
    public class Config
    {
        private const string EnvironmentClientTokenVariableName = "DiscordClientToken";
        private const string EnvironmentDarkroomApiTokenName = "DarkroomApiToken";

        private static Config _config;

        private Config()
        {
        }

        public static Config Instance => _config ??= ReadConfig();

        public DiscordConfig Discord { get; set; }

        private static Config ReadConfig()
        {
            var environmentConfig = ReadConfigFromEnvironment();
            if (environmentConfig != null)
            {
                return environmentConfig;
            }
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

        private static Config ReadConfigFromEnvironment()
        {
            var t = Environment.GetEnvironmentVariable(EnvironmentClientTokenVariableName);
            var d = Environment.GetEnvironmentVariable(EnvironmentDarkroomApiTokenName);
            if (t != null && d != null)
            {
                return new Config()
                {
                    Discord = new DiscordConfig()
                    {
                        ClientToken = t,
                        DarkroomApiToken = d
                    }
                };
            }

            return null;
        }

        public class DiscordConfig
        {
            public string ClientToken { get; set; }
            public string DarkroomApiToken { get; set; }
        }
    }
}