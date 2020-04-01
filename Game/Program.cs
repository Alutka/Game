using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Game
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseWebRoot(@"..\wwwroot")
                .UseStartup<Startup>();
    }
}
