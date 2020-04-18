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
            for (int row = 0; row < _bitmap.Width; row++)
            {
                for (int col = 0; col < _bitmap.Height; col++)
                {
                    layerValues[index++] = TranslateColor(_bitmap.GetPixel(row, col));
                }
            }
            return new TMapLayer()
            {
                Type = _layerType,
                Values = layerValues,
                LayerEnum = _enum
            };
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
