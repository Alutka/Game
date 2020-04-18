using Game.Interfaces;
using Game.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Configuration;
using StaticFilesIO;
using System.IO;

namespace Game
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigurationInstance.Config = _configuration.GetSection("globalConfig").Get<TConfig>();
            ConfigurationInstance.SwaggerConfig = _configuration.GetSection("swaggerConfig").Get<TSwaggerConfig>();

            services.AddSingleton<IMapService, MapService>();
            services.AddSingleton<IStaticDefinitionsService, StaticDefinitionsService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddCors(options =>
            {
                options.AddPolicy("LocalPolicy",
                    policy => policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                );
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IMapService mapService, IStaticDefinitionsService staticDefinitionsService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMvc();
            using (Stream stream = MapFileProvider.GetMapImportStream())
            {
                mapService.SetMap(MapIO.Import(stream));
            }
            staticDefinitionsService.SetDefinitions(DefinitionsReader.Import());
        }
    }
}
