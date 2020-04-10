using Shared.Configuration;
using Shared.Map;
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
            _mapExtension = ConfigurationInstance.Config.MapExtension;
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
                writer.Write(map.Height);
                writer.Write(map.Width);
                writer.Write(map.Layers.Length);
                for (int i = 0; i < map.Layers.Length; i++)
                {
                    WriteLayer(writer, map.Layers[i], map.Height * map.Width);
                }
            }
        }

        public TMap Import(string mapName)
        {
            string mapPath = GetMapPath(mapName);
            using (BinaryReader reader = new BinaryReader(File.OpenRead(mapPath)))
            {
                string name = reader.ReadString();
                int height = reader.ReadInt32();
                int width = reader.ReadInt32();
                int layersCount = reader.ReadInt32();
                var layers = new TMapLayer[layersCount];
                for (int i = 0; i < layersCount; i++)
                {
                    layers[i] = ReadLayer(reader, height * width);
                }
                return new TMap() { Name = name, Height = height, Width = width, Layers = layers };
            }
        }

        private string GetMapPath(string mapName)
        {
            return Path.Combine(_mapDirectory, mapName + _mapExtension);
        }

        private TMapLayer ReadLayer(BinaryReader reader, int length)
        {
            string type = reader.ReadString();
            int[] values = new int[length];
            for (int i = 0; i < length; i++)
            {
                values[i] = reader.ReadInt32();
            }
            return new TMapLayer() { Type = type, Values = values };
        }

        private void WriteLayer(BinaryWriter writer, TMapLayer layer, int length)
        {
            writer.Write(layer.Type);
            for (int i = 0; i < length; i++)
            {
                writer.Write(layer.Values[i]);
            }
        }
    }
}
