namespace Shared.Configuration
{
    public static class ConfigurationInstance
    {
        public static TConfig Config { get; set; }
        public static TSwaggerConfig SwaggerConfig { get; set; }
    }
}
