using Newtonsoft.Json;

namespace MapGenerator.Structures
{
    [JsonConverter(typeof(MapLayerHeaderConverter))]
    public class AbstractLayerHeader
    {
        public string Type { get; set; }
    }
}
