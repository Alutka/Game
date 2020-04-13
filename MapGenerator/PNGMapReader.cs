﻿using MapGenerator.Structures;
using Newtonsoft.Json;
using Shared;
using Shared.Configuration;
using Shared.Definitions;
using Shared.Map;
using StaticFilesIO;
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
        private readonly TStaticDefinitions _definitions;

        public PNGMapReader(string mapName)
        {
            _definitions = DefinitionsReader.Import();
            _mapName = mapName;
            _mapDirectory = Path.Combine(ConfigurationInstance.Config.StoragePaths.DevStatic, _mapName);
        }

        public TMap ReadMap()
        {
            int height = -1;
            int width = -1;
            List<TMapLayer> mapLayers = new List<TMapLayer>();
            IEnumerable<string> layerNames = Directory.GetFiles(_mapDirectory).Select(name => Path.GetFileNameWithoutExtension(name)).Distinct();
            foreach (var layer in layerNames)
            {
                TMapLayerHeader header = ReadLayerHeader(layer);
                var layerReader = new PNGMapLayerReader(layer, _mapDirectory, header, TranslateToEnum(header));
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

        private TMapLayerHeader ReadLayerHeader(string layerName)
        {
            using (StreamReader r = new StreamReader(Path.Combine(_mapDirectory, layerName + HEADER_EXTENSION)))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<TMapLayerHeader>(json);
            }
        }

        private TEnum TranslateToEnum(TMapLayerHeader header)
        {
            if (header.Type == DefinitionType.Biome)
            {
                return new TEnum(_definitions.Biomes.Items.Select(i => i.Name).ToArray());
            }
            throw new NotImplementedException();
        }
    }
}
