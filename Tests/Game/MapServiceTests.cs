using Game.Interfaces;
using Game.Services;
using Moq;
using Shared.Definitions;
using Shared.Interfaces;
using Shared.Map;
using System.Linq;
using Xunit;

namespace Tests.Game
{
    public class MapServiceTests
    {
        private IMapService _mapService;

        [Fact]
        public void GetTileTest()
        {
            string biomeName = "test";
            string[] tileResources = new string[] { "x", "y" };
            string[] resources = new string[] { "a", "b" };
            var staticDefinitions = new Mock<IStaticDefinitionsService>();
            staticDefinitions
                .Setup(s => s.GetBiomeDefinition(It.IsAny<string>()))
                .Returns(new TBiome()
                {
                    Name = biomeName,
                    Resources = resources
                });
            var biomeLayer = new Mock<IMapLayer>();
            biomeLayer
                .Setup(l => l.GetValue(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(biomeName);
            var resourceLayer1 = new Mock<IMapLayer>();
            resourceLayer1
                .Setup(l => l.GetValue(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(tileResources[0]);
            var resourceLayer2 = new Mock<IMapLayer>();
            resourceLayer2
                .Setup(l => l.GetValue(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(tileResources[1]);
            _mapService = new MapService(staticDefinitions.Object);
            _mapService.SetMap(new TMap()
            {
                BiomeLayer = biomeLayer.Object,
                ResourceLayers = new IMapLayer[] { resourceLayer1.Object, resourceLayer2.Object }
            });
            var result = _mapService.GetTile(10, 11);
            Assert.Equal(biomeName, result.Biome);
            Assert.Equal(tileResources.Union(resources), result.Resources);
        }
    }
}
