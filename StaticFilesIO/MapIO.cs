using Shared;
using Shared.Interfaces;
using Shared.Map;
using Shared.Structures;
using System.IO;
using System.Text;

namespace StaticFilesIO
{
    public static class MapIO
    {
        public static void Export(TMap map, Stream stream, bool leaveOpen = false)
        {
            using (BinaryWriter writer = new BinaryWriter(stream, Encoding.Default, leaveOpen))
            {
                writer.Write(map.Name);
                WriteLayer(writer, map.BiomeLayer);
                writer.Write(map.ResourceLayers.Length);
                for (int i = 0; i < map.ResourceLayers.Length; i++)
                {
                    WriteLayer(writer, map.ResourceLayers[i]);
                }
            }
        }

        public static TMap Import(Stream stream)
        {
            using (BinaryReader reader = new BinaryReader(stream))
            {
                string name = reader.ReadString();
                TMapLayer biomeLayer = ReadLayer(reader);
                int layersCount = reader.ReadInt32();
                var layers = new TMapLayer[layersCount];
                for (int i = 0; i < layersCount; i++)
                {
                    layers[i] = ReadLayer(reader);
                }
                return new TMap() { Name = name, ResourceLayers = layers, BiomeLayer = biomeLayer };
            }
        }

        private static TMapLayer ReadLayer(BinaryReader reader)
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

        private static TEnum ReadLayerEnum(BinaryReader reader)
        {
            int length = reader.ReadInt32();
            string[] names = new string[length];
            for (int i = 0; i < length; i++)
            {
                names[i] = reader.ReadString();
            }
            return new TEnum(names);
        }

        private static void WriteLayer(BinaryWriter writer, IMapLayer layer)
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

        private static void WriteLayerEnum(BinaryWriter writer, TEnum layerEnum)
        {
            writer.Write(layerEnum.Length);
            for (int i = 0; i < layerEnum.Length; i++)
            {
                writer.Write(layerEnum.GetValue(i));
            }
        }
    }
}
