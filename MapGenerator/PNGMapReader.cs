using MapGenerator.Structures;
using Newtonsoft.Json;
using Shared.Configuration;
using Shared.Map;
using System;
using System.Collections.Generic;
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
            _mapDirectory = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, ConfigurationInstance.Config.StoragePaths.DevStatic, _mapName);
        }

        public TMap ReadMap()
        {
            int height = -1;
            int width = -1;
            List<TMapLayer> mapLayers = new List<TMapLayer>();
            IEnumerable<string> layerNames = Directory.GetFiles(_mapDirectory).Select(name => Path.GetFileNameWithoutExtension(name)).Distinct();
            foreach (var layer in layerNames)
            {
                AbstractLayerHeader header = ReadLayerHeader(layer);
                Type t = header.GetType();
                Type generic = typeof(PNGMapLayerReader<>);
                Type enumType = t.GetGenericArguments()[0];
                Type readerType = generic.MakeGenericType(enumType);
                var layerReader = Activator.CreateInstance(readerType, layer, _mapDirectory, header) as AbstractPNGMapLayerReader;
                if (height == -1)
                {
                    height = layerReader.GetHeight();
                    width = layerReader.GetWidth();
                }
                if (height != layerReader.GetHeight() || width != layerReader.GetWidth())
                {
                    throw new InvalidDataException($"Map {_mapName} contains layers of different sizes!");
                }
                mapLayers.Add(layerReader.ReadLayer());
            }
            return new TMap() { Name = _mapName, Width = width, Height = height, Layers = mapLayers.ToArray() };
        }

        private AbstractLayerHeader ReadLayerHeader(string layerName)
        {
            using (StreamReader r = new StreamReader(Path.Combine(_mapDirectory, layerName + HEADER_EXTENSION)))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<AbstractLayerHeader>(json);
            }
        }
    }
}
