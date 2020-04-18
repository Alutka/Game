using MapGenerator.Structures;
using Shared;
using System.Drawing;

namespace Tests.MapGenerator
{
    public class MapLayerFixture
    {
        public TLayer BiomeLayer { get; set; }
        public TLayer ResourceLayer { get; set; }
        public TLayer InvalidSizeLayer { get; set; }
        public TLayer[] Layers => new TLayer[] { BiomeLayer, ResourceLayer, InvalidSizeLayer };

        public MapLayerFixture()
        {
            int width = 10;
            int height = 11;
            var header = new TMapLayerHeader()
            {
                Type = DefinitionType.Biome,
                Colors = new TColorDefinition[]
                {
                    new TColorDefinition() { Type = "biome0", Color = new TColor() { R = 0, G = 0, B = 0 } },
                    new TColorDefinition() { Type = "biome1", Color = new TColor() { R = 1, G = 2, B = 3 } },
                    new TColorDefinition() { Type = "biome2", Color = new TColor() { R = 2, G = 2, B = 3 } }
                }
            };
            var bitmap = new Bitmap(width, height);
            bitmap.SetPixel(1, 1, Color.FromArgb(1, 2, 3));
            bitmap.SetPixel(9, 9, Color.FromArgb(1, 2, 3));
            bitmap.SetPixel(0, 0, Color.FromArgb(2, 2, 3));
            bitmap.SetPixel(5, 6, Color.FromArgb(2, 2, 3));
            BiomeLayer = new TLayer() { Bitmap = bitmap, MapLayerHeader = header };
            header = new TMapLayerHeader()
            {
                Type = DefinitionType.Resource,
                Colors = new TColorDefinition[]
                {
                    new TColorDefinition() { Type = "test0", Color = new TColor() { R = 0, G = 0, B = 0 } },
                    new TColorDefinition() { Type = "test1", Color = new TColor() { R = 1, G = 2, B = 3 } },
                    new TColorDefinition() { Type = "test2", Color = new TColor() { R = 2, G = 2, B = 3 } }
                }
            };
            bitmap = new Bitmap(width, height);
            bitmap.SetPixel(3, 5, Color.FromArgb(1, 2, 3));
            bitmap.SetPixel(2, 1, Color.FromArgb(1, 2, 3));
            bitmap.SetPixel(1, 5, Color.FromArgb(2, 2, 3));
            bitmap.SetPixel(4, 7, Color.FromArgb(2, 2, 3));
            ResourceLayer = new TLayer() { Bitmap = bitmap, MapLayerHeader = header };
            header = new TMapLayerHeader()
            {
                Type = DefinitionType.Biome,
                Colors = new TColorDefinition[]
                {
                    new TColorDefinition() { Type = "test0", Color = new TColor() { R = 0, G = 0, B = 0 } },
                    new TColorDefinition() { Type = "test1", Color = new TColor() { R = 1, G = 2, B = 3 } },
                    new TColorDefinition() { Type = "test2", Color = new TColor() { R = 2, G = 2, B = 3 } }
                }
            };
            bitmap = new Bitmap(width + 1, height);
            bitmap.SetPixel(1, 1, Color.FromArgb(1, 2, 3));
            bitmap.SetPixel(9, 9, Color.FromArgb(1, 2, 3));
            bitmap.SetPixel(0, 0, Color.FromArgb(2, 2, 3));
            bitmap.SetPixel(5, 6, Color.FromArgb(2, 2, 3));
            InvalidSizeLayer = new TLayer() { Bitmap = bitmap, MapLayerHeader = header };
        }
    }
}
