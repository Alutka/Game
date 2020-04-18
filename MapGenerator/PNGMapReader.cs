using MapGenerator.Structures;
using Newtonsoft.Json;
using Shared;
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
        private readonly string _mapName;
        private readonly string _mapDirectory;

        public PNGMapReader(string mapName)
        {
            _mapName = mapName;
            _mapDirectory = Path.Combine(ConfigurationInstance.Config.StoragePaths.DevStatic, _mapName);
        }

        public TMap ReadMap()
        {
            List<TMapLayer> resourceLayers = new List<TMapLayer>();
            List<TMapLayer> biomeLayers = new List<TMapLayer>();
            IEnumerable<string> layerNames = Directory.GetFiles(_mapDirectory).Select(name => Path.GetFileNameWithoutExtension(name)).Distinct();
            foreach (var layerName in layerNames)
            {
                TMapLayerHeader header = ReadLayerHeader(layerName);
                var bitmap = new Bitmap(Path.Combine(_mapDirectory, layerName + ".png"), true);
                var layerReader = new PNGMapLayerReader(bitmap, header);
                TMapLayer layer = layerReader.ReadLayer();
                switch (layer.Type)
                {
                    case DefinitionType.Biome:
                        biomeLayers.Add(layer);
                        break;
                    case DefinitionType.Resource:
                        resourceLayers.Add(layer);
                        break;
                    default:
                        throw new InvalidDataException($"Invalid layer type {layer.Type}");
                }
            }
            ValidateLayers(resourceLayers, biomeLayers);
            return new TMap() { Name = _mapName, ResourceLayers = resourceLayers.ToArray(), BiomeLayer = biomeLayers.First() };
        }

        private void ValidateLayers(List<TMapLayer> mapLayers, List<TMapLayer> biomeLayers)
        {
            if (biomeLayers.Count() != 1)
            {
                throw new Exception("Only one biome layer allowed!");
            }
            var layersHeights = mapLayers.Select(m => m.Height).Union(biomeLayers.Select(b => b.Height));
            var layersWidths = mapLayers.Select(m => m.Width).Union(biomeLayers.Select(b => b.Width));
            if (layersHeights.Distinct().Count() != 1 || layersWidths.Distinct().Count() != 1)
            {
                throw new Exception("Layers have different sizes!");
            }
        }

        private TMapLayerHeader ReadLayerHeader(string layerName)
        {
            using (StreamReader r = new StreamReader(Path.Combine(_mapDirectory, layerName + ".json")))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<TMapLayerHeader>(json);
            }
        }
    }
}
