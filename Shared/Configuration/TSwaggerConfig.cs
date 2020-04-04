namespace Shared.Configuration
{
    public class TSwaggerConfig
    {
        public string Version { get; set; }
        public string EndpointUrl { get; set; }
        public string Title { get; set; }
        public string EndpointName => Title + " " + Version;
    }
}
