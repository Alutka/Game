using Shared.Structures;

namespace Shared.Map
{
    public class TMapLayer
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int[] Values { get; set; }
        public TEnum LayerEnum { get; set; }

        public DefinitionType Type { get; set; }

        public TMapLayer(int width, int height, int[] values, TEnum layerEnum, DefinitionType type)
        {
            Width = width;
            Height = height;
            Values = values;
            LayerEnum = layerEnum;
            Type = type;
        }

        public int GetKey(int x, int y)
        {
            return Values[GetIndex(x, y)];
        }

        public string GetValue(int x, int y)
        {
            return LayerEnum.GetValue(GetKey(x, y));
        }

        private int GetIndex(int x, int y)
        {
            return y * Width + x;
        }
    }
}
