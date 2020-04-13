using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Game
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddJsonFile("globalsettings.json", optional: false)
                .Build();
            CreateWebHostBuilder(args)
                .UseConfiguration(config)
                .Build()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("https://localhost:5001/")
                .UseStartup<Startup>();
    }
}
