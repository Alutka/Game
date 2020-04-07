using Microsoft.Extensions.Configuration;
using Shared.Configuration;

namespace MapGenerator
{
    public static class Startup
    {
        public static void LoadConfiguration()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                            .AddJsonFile("globalsettings.json", optional: false)
                            .Build();

            ConfigurationInstance.Config = config.GetSection("globalConfig").Get<TConfig>();
        }
    }
}
