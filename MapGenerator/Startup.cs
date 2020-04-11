using Microsoft.Extensions.Configuration;
using Shared.Configuration;
using System;
using System.IO;

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
            Directory.SetCurrentDirectory(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName);
        }
    }
}
