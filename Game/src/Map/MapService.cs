using Game.Interfaces;
using Shared.Configuration;
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
            var mapIO = new MapIO();
            _map = mapIO.Import(ConfigurationInstance.Config.DefaultMapName);
        }
    }
}
