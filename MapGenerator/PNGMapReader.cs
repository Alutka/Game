using MapGenerator.Structures;
using Newtonsoft.Json;
using Shared.Configuration;
using Shared.Map;
using System;
using System.Drawing;
using System.IO;

namespace MapGenerator
{
    public class PNGMapReader
    {
        private string _mapName;
        private readonly string _projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        private const string PNG_EXTENSION = ".png";
        private const string HEADER_EXTENSION = ".json";

        public PNGMapReader(string mapName)
        {
            _mapName = mapName;
        }

        public TMapLayer ReadMap()
        {
            var dupa = ReadHeader();
            return ReadLayer(_mapName);
        }

        private TMapLayer ReadLayer(string layerName)
        {
            var path = Path.Combine(_projectDirectory, ConfigurationInstance.Config.StoragePaths.DevStatic, _mapName + PNG_EXTENSION);
            var dupa = new Bitmap(path, true);
            var pixel = dupa.GetPixel(0, 0);
            return new TMapLayer();
        }

        private TColorDefinition[] ReadHeader()
        {
            var path = Path.Combine(_projectDirectory, ConfigurationInstance.Config.StoragePaths.DevStatic, _mapName + HEADER_EXTENSION);
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<TColorDefinition[]>(json);
            }
        }
    }
}
