using MapGenerator.Structures;
using Shared.Map;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace MapGenerator
{
    public class PNGMapLayerReader<T>
    {
        private const string PNG_EXTENSION = ".png";
        private readonly string _layerName;
        private Dictionary<TColor, T> _header;
        private readonly string _path;
        private readonly Bitmap _bitmap;

        public PNGMapLayerReader(string layerName, string directoryPath, TMapLayerHeader<T> header)
        {
            _layerName = layerName;
            _path = Path.Combine(directoryPath, layerName);
            _header = header.Colors.ToDictionary(h => h.Color, h => h.Type);
            _bitmap = new Bitmap(_path + PNG_EXTENSION, true);
        }

        public int GetHeight() => _bitmap.Height;

        public int GetWidth() => _bitmap.Width;

        public TMapLayer ReadLayer()
        {
            int length = _bitmap.Width * _bitmap.Height;
            int[] layerValues = new int[length];
            int index = 0;
            for (int row = 0; row < _bitmap.Width; row++)
            {
                for (int col = 0; col < _bitmap.Height; col++)
                {
                    layerValues[index++] = TranslateColor(_bitmap.GetPixel(row, col));
                }
            }
            return new TMapLayer() { Name = _layerName, Values = layerValues };
        }

        private int TranslateColor(Color pixel)
        {
            return 0;
        }
    }
}
