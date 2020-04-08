using Newtonsoft.Json.Linq;
using Shared.Converters;
using System;
using System.Linq;

namespace MapGenerator.Structures
{
    public class MapLayerHeaderConverter : JsonCreationConverter<AbstractLayerHeader>
    {
        protected override AbstractLayerHeader Create(Type objectType, JObject jObject)
        {
            if (jObject.TryGetValue("type", out JToken type))
            {
                string typeName = type.ToString();
                typeName = typeName.First().ToString().ToUpper() + typeName.Substring(1);
                Type t = Type.GetType($"Shared.{typeName}, Shared, Version = 1.0.0.0, Culture = neutral, PublicKeyToken = null");
                Type generic = typeof(TMapLayerHeader<>);
                var result = generic.MakeGenericType(t);
                return (AbstractLayerHeader)Activator.CreateInstance(result);
            }
            else
            {
                throw new InvalidOperationException("Cannot read header without type!");
            }
        }
    }
}
