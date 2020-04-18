using Game.Interfaces;
using Shared.Map;
using StaticFilesIO;

namespace Game.Map
{
    public class MapService : IMapService
    {
        private TMap _map;

        public MapService()
        {
        }

        public void Import()
        {
            using (System.IO.Stream stream = MapFileProvider.GetMapImportStream())
            {
                _map = MapIO.Import(stream);
            }
        }
    }
}
