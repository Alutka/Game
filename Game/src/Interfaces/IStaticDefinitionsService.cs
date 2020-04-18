using Shared.Definitions;

namespace Game.Interfaces
{
    public interface IStaticDefinitionsService
    {
        void SetDefinitions(TStaticDefinitions definitions);

        TBiome GetBiomeDefinition(string name);

        TResource GetResourceDefinition(string name);

        TRaw GetRawDefinition(string name);
    }
}
