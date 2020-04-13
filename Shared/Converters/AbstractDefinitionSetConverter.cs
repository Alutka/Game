using Newtonsoft.Json.Linq;
using Shared.Definitions;
using System;

namespace Shared.Converters
{
    public class AbstractDefinitionSetConverter : JsonCreationConverter<AbstractDefinitionSet>
    {
        // just to reuse it in the future
        protected override AbstractDefinitionSet Create(Type objectType, JObject jObject)
        {
            var type = jObject.GetValue("type").ToObject<DefinitionType>();
            {
                switch (type)
                {
                    case DefinitionType.Biome:
                        return new TDefinitionSet<TBiome>();
                    default:
                        throw new NotImplementedException();
                }
            }
        }
    }
}
