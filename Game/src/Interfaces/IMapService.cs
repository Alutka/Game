using Shared.Map;
using Shared.Structures;

namespace Game.Interfaces
{
    public interface IMapService
    {
        TMapTile GetTile(int x, int y);

        void SetMap(TMap map);
    }
}
