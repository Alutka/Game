using Newtonsoft.Json;
using Shared.Converters;

namespace Shared.Definitions
{
    [JsonConverter(typeof(AbstractDefinitionSetConverter))]
    public abstract class AbstractDefinitionSet
    {
        public DefinitionType Type { get; set; }
    }
}
