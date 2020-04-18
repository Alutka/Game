using Shared.Structures;

namespace Shared.Map
{
    public struct TMapLayer
    {
        public DefinitionType Type { get; set; }
        public int[] Values { get; set; }
        public TEnum LayerEnum { get; set; }
    }
}
