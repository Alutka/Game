using Shared;

namespace MapGenerator.Structures
{
    public class TMapLayerHeader
    {
        public DefinitionType Type { get; set; }
        public TColorDefinition[] Colors { get; set; }
    }
}
