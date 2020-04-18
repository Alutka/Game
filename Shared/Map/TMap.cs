using Shared.Interfaces;

namespace Shared.Map
{
    public class TMap
    {
        public string Name { get; set; }
        public IMapLayer[] ResourceLayers { get; set; }
        public IMapLayer BiomeLayer { get; set; }
    }
}
