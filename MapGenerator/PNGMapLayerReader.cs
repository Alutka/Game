using MapGenerator.Structures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MapGenerator
{
    public class PNGMapLayerReader<T> : AbstractPNGMapLayerReader where T : System.Enum
    {
        private readonly Dictionary<TColor, T> _header;

        public PNGMapLayerReader(string layerName, string directoryPath, AbstractLayerHeader header) : base(layerName, directoryPath)
        {
            _header = (header as TMapLayerHeader<T>).Colors.ToDictionary(h => h.Color, h => h.Type);
        }

        protected override int TranslateColor(Color pixel)
        {
            return Convert.ToInt32(_header[new TColor() { R = pixel.R, B = pixel.B, G = pixel.G }]);
        }
    }
}
