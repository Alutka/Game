using MapGenerator.Structures;
using Newtonsoft.Json;
using Shared.Configuration;
using Shared.Map;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace MapGenerator
{
    public class PNGMapReader
    {
        private const string HEADER_EXTENSION = ".json";
        private readonly string _mapName;
        private readonly string _mapDirectory;

        public PNGMapReader(string mapName)
        {
            _mapName = mapName;
            _mapDirectory = Path.Combine(ConfigurationInstance.Config.StoragePaths.DevStatic, _mapName);
        }

        public TMap ReadMap()
        {
            List<TMapLayer> mapLayers = new List<TMapLayer>();
            IEnumerable<string> layerNames = Directory.GetFiles(_mapDirectory).Select(name => Path.GetFileNameWithoutExtension(name)).Distinct();
            foreach (var layerName in layerNames)
            {
                TMapLayerHeader header = ReadLayerHeader(layerName);
                var bitmap = new Bitmap(Path.Combine(_mapDirectory, layerName + ".png"), true);
                var layerReader = new PNGMapLayerReader(bitmap, header);
                TMapLayer layer = layerReader.ReadLayer();
                mapLayers.Add(layer);
            }
            ValidateLayers(mapLayers);
            return new TMap() { Name = _mapName, Layers = mapLayers.ToArray() };
        }

        private void ValidateLayers(List<TMapLayer> mapLayers)
        {
            var layersHeights = mapLayers.Select(m => m.Height);
            var layersWidths = mapLayers.Select(m => m.Width);
            if (layersHeights.Distinct().Count() != 1 || layersWidths.Distinct().Count() != 1)
            {
                throw new Exception("Layers have different sizes!");
            }
        }

        private TMapLayerHeader ReadLayerHeader(string layerName)
        {
            using (StreamReader r = new StreamReader(Path.Combine(_mapDirectory, layerName + HEADER_EXTENSION)))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<TMapLayerHeader>(json);
            }
        }
    }
}
