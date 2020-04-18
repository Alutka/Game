using Game.Interfaces;
using Shared.Definitions;
using System.Linq;

namespace Game.Services
{
    public class StaticDefinitionsService : IStaticDefinitionsService
    {
        private TStaticDefinitions _definitions;

        public TBiome GetBiomeDefinition(string name)
        {
            return GetItemByName(_definitions.Biomes.Items, name);
        }

        public TRaw GetRawDefinition(string name)
        {
            return GetItemByName(_definitions.Raws.Items, name);
        }

        public TResource GetResourceDefinition(string name)
        {
            return GetItemByName(_definitions.Resources.Items, name);
        }

        public void SetDefinitions(TStaticDefinitions definitions)
        {
            _definitions = definitions;
        }

        private T GetItemByName<T>(T[] collection, string name) where T : AbstractDefinition
        {
            return collection.First(item => item.Name == name);
        }
    }
}
