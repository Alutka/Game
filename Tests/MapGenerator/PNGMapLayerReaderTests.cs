using MapGenerator;
using MapGenerator.Structures;
using Shared.Map;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests.MapGenerator
{
    public class PNGMapLayerReaderTests : IClassFixture<MapLayerFixture>
    {
        private readonly MapLayerFixture _fixture;

        public PNGMapLayerReaderTests(MapLayerFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void ReadBiomeLayerTest(int index)
        {
            TLayer layer = _fixture.Layers[index];
            var reader = new PNGMapLayerReader(layer.Bitmap, layer.MapLayerHeader);
            TMapLayer result = reader.ReadLayer();
            Assert.Equal(layer.MapLayerHeader.Type, result.Type);

            Dictionary<string, TColor> dict = layer.MapLayerHeader.Colors.ToDictionary(col => col.Type, col => col.Color);
            for (int y = 0; y < layer.Bitmap.Height; y++)
            {
                for (int x = 0; x < layer.Bitmap.Width; x++)
                {
                    var pixel = layer.Bitmap.GetPixel(x, y);
                    var color = dict[result.GetValue(x, y)];
                    Assert.Equal(pixel.R, color.R);
                    Assert.Equal(pixel.G, color.G);
                    Assert.Equal(pixel.B, color.B);
                }
            }
        }
    }
}
