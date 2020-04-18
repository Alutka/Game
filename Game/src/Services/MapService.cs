using Game.Interfaces;
using Shared.Map;
using Shared.Structures;
using System.Linq;

namespace Game.Services
{
    public class MapService : IMapService
    {
        private TMap _map;
        private readonly IStaticDefinitionsService _staticDefinitionsService;

        public MapService(IStaticDefinitionsService staticDefinitionsService)
        {
            _staticDefinitionsService = staticDefinitionsService;
        }

        public TMapTile GetTile(int x, int y)
        {
            var biome = _map.BiomeLayer.GetValue(x, y);
            return new TMapTile()
            {
                Biome = biome,
                Resources = _map
                    .ResourceLayers
                    .Select(layer => layer.GetValue(x, y))
                    .Where(res => !string.IsNullOrEmpty(res))
                    .Union(_staticDefinitionsService.GetBiomeDefinition(biome).Resources)
                    .ToArray()
            };
        }

        public void SetMap(TMap map)
        {
            _map = map;
        }
    }
}
