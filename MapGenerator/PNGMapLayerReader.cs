﻿using MapGenerator.Structures;
using Shared.Map;
using Shared.Structures;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace MapGenerator
{
    public class PNGMapLayerReader
    {
        private const string PNG_EXTENSION = ".png";
        private readonly string _layerType;
        private readonly string _path;
        private readonly Bitmap _bitmap;
        private readonly TDefinition _definition;
        private readonly Dictionary<TColor, string> _header;

        public PNGMapLayerReader(string layerName, string directoryPath, TMapLayerHeader header, TDefinition definition)
        {
            _path = Path.Combine(directoryPath, layerName + PNG_EXTENSION);
            _bitmap = new Bitmap(_path, true);
            _definition = definition;
            _header = header.Colors.ToDictionary(col => col.Color, col => col.Type);
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
            return new TMapLayer() { Type = _layerType, Values = layerValues };
        }

        private int TranslateColor(Color pixel)
        {
            return _definition.GetKey(_header[new TColor() { R = pixel.R, B = pixel.B, G = pixel.G }]);
        }
    }
}
