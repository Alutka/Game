using Shared;
using Shared.Configuration;
using Shared.Map;
using Shared.Structures;
using System.IO;

namespace StaticFilesIO
{
    public class MapIO
    {
        private readonly string _mapDirectory;
        private readonly string _mapExtension;

        public MapIO()
        {
            _mapDirectory = Path.Combine(ConfigurationInstance.Config.StoragePaths.Static, ConfigurationInstance.Config.StoragePaths.Maps);
            _mapExtension = ConfigurationInstance.Config.Files.MapExtension;
        }

        public void Export(TMap map)
        {
            var mapPath = GetMapPath(map.Name);
            if (File.Exists(mapPath))
            {
                File.Delete(mapPath);
            }
            using (BinaryWriter writer = new BinaryWriter(File.Create(mapPath)))
            {
                writer.Write(map.Name);
                writer.Write(map.Layers.Length);
                for (int i = 0; i < map.Layers.Length; i++)
                {
                    WriteLayer(writer, map.Layers[i]);
                }
            }
        }

        public TMap Import(string mapName)
        {
            string mapPath = GetMapPath(mapName);
            using (BinaryReader reader = new BinaryReader(File.OpenRead(mapPath)))
            {
                string name = reader.ReadString();
                int layersCount = reader.ReadInt32();
                var layers = new TMapLayer[layersCount];
                for (int i = 0; i < layersCount; i++)
                {
                    layers[i] = ReadLayer(reader);
                }
                return new TMap() { Name = name, Layers = layers };
            }
        }

        private string GetMapPath(string mapName)
        {
            return Path.Combine(_mapDirectory, mapName + _mapExtension);
        }

        private TMapLayer ReadLayer(BinaryReader reader)
        {
            DefinitionType type = (DefinitionType)reader.ReadInt32();
            int width = reader.ReadInt32();
            int height = reader.ReadInt32();
            int length = width * height;
            int[] values = new int[length];
            for (int i = 0; i < length; i++)
            {
                values[i] = reader.ReadInt32();
            }
            TEnum layerEnum = ReadLayerEnum(reader);
            return new TMapLayer(width, height, values, layerEnum, type);
        }

        private TEnum ReadLayerEnum(BinaryReader reader)
        {
            int length = reader.ReadInt32();
            string[] names = new string[length];
            for (int i = 0; i < length; i++)
            {
                names[i] = reader.ReadString();
            }
            return new TEnum(names);
        }

        private void WriteLayer(BinaryWriter writer, TMapLayer layer)
        {
            writer.Write((int)layer.Type);
            writer.Write(layer.Width);
            writer.Write(layer.Height);
            for (int i = 0; i < layer.Width * layer.Height; i++)
            {
                writer.Write(layer.Values[i]);
            }
            WriteLayerEnum(writer, layer.LayerEnum);
        }

        private void WriteLayerEnum(BinaryWriter writer, TEnum layerEnum)
        {
            writer.Write(layerEnum.Length);
            for (int i = 0; i < layerEnum.Length; i++)
            {
                writer.Write(layerEnum.GetValue(i));
            }
        }
    }
}
