using MapGenerator.Structures;
using Shared;
using Shared.Map;
using Shared.Structures;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MapGenerator
{
    public class PNGMapLayerReader
    {
        private readonly DefinitionType _layerType;
        private readonly Bitmap _bitmap;
        private readonly TEnum _enum;
        private readonly Dictionary<TColor, string> _colorDictionary;

        public PNGMapLayerReader(Bitmap bitmap, TMapLayerHeader header)
        {
            _bitmap = bitmap;
            _enum = TranslateToEnum(header);
            _colorDictionary = header.Colors.ToDictionary(col => col.Color, col => col.Type);
            _layerType = header.Type;
        }

        public int GetHeight() => _bitmap.Height;

        public int GetWidth() => _bitmap.Width;

        public TMapLayer ReadLayer()
        {
            int length = _bitmap.Width * _bitmap.Height;
            int[] layerValues = new int[length];
            int index = 0;
            for (int y = 0; y < _bitmap.Height; y++)
            {
                for (int x = 0; x < _bitmap.Width; x++)
                {
                    layerValues[index++] = TranslateColor(_bitmap.GetPixel(x, y));
                }
            }
            return new TMapLayer(_bitmap.Width, _bitmap.Height, layerValues, _enum, _layerType);
        }

        private int TranslateColor(Color pixel)
        {
            return _enum.GetKey(_colorDictionary[new TColor() { R = pixel.R, B = pixel.B, G = pixel.G }]);
        }

        private TEnum TranslateToEnum(TMapLayerHeader header)
        {
            return new TEnum(header.Colors.Select(col => col.Type).ToArray());
        }
    }
}
