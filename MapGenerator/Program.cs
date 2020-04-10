using Shared.Configuration;
using Shared.Map;
using Shared.Utils;
using StaticFilesIO;
using System;
using System.IO;

namespace MapGenerator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Startup.LoadConfiguration();
            var reader = new PNGMapReader("DefaultMap");
            TMap map = reader.ReadMap();
            string mapFolder = PathUtils.GetPath(Path.Combine(ConfigurationInstance.Config.StoragePaths.Static, ConfigurationInstance.Config.StoragePaths.Maps));
            var mapIO = new MapIO(mapFolder, ConfigurationInstance.Config.MapExtension);
            mapIO.Export(map);
            Console.WriteLine("Map generated!");
        }
    }
}
