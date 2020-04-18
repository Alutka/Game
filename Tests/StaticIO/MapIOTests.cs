using Shared;
using Shared.Map;
using Shared.Structures;
using StaticFilesIO;
using System.IO;
using Xunit;

namespace Tests.StaticIO
{
    public class MapIOTests
    {
        [Fact]
        public void TestInputOutput()
        {
            int width = 100;
            int height = 1000;
            int[] values = new int[width * height];
            int[] values2 = new int[width * height];
            values[70] = 1;
            values[1000] = 2;
            values[0] = 3;
            values2[4983] = 1;
            values2[444] = 2;
            TEnum lenum1 = new TEnum(new string[] { "a", "b", "c", "d" });
            TEnum lenum2 = new TEnum(new string[] { "x", "y", "z" });
            var map = new TMap()
            {
                Name = "test",
                BiomeLayer = new TMapLayer(width, height, values, lenum1, DefinitionType.Biome),
                ResourceLayers = new TMapLayer[]
                {
                    new TMapLayer(width, height, values2, lenum2, DefinitionType.Resource)
                }
            };
            using (var stream = new MemoryStream())
            {
                MapIO.Export(map, stream, true);
                stream.Seek(0, SeekOrigin.Begin);
                TMap result = MapIO.Import(stream);
                Assert.Equal(map.Name, result.Name);
            }
        }
    }
}
