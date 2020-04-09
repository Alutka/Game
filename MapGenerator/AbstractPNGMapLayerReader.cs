using Shared.Map;
using System.Drawing;
using System.IO;

namespace MapGenerator
{
    public abstract class AbstractPNGMapLayerReader
    {
        protected const string PNG_EXTENSION = ".png";
        protected readonly string _layerName;
        protected readonly string _path;
        protected readonly Bitmap _bitmap;

        public AbstractPNGMapLayerReader(string layerName, string directoryPath)
        {
            _layerName = layerName;
            _path = Path.Combine(directoryPath, layerName);
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

        protected abstract int TranslateColor(Color pixel);
    }
}
