using Shared.Structures;

namespace Shared.Interfaces
{
    public interface IMapLayer
    {
        int Width { get; }
        int Height { get; }
        int[] Values { get; }
        TEnum LayerEnum { get; }
        DefinitionType Type { get; }

        int GetKey(int x, int y);

        string GetValue(int x, int y);
    }
}
