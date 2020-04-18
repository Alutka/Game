using Shared.Map;
using StaticFilesIO;
using System;

namespace MapGenerator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Startup.LoadConfiguration();
            var reader = new PNGMapReader("DefaultMap");
            TMap map = reader.ReadMap();
            MapIO.Export(map, FileProvider.GetMapExportStream());
            Console.WriteLine("Map generated!");
        }
    }
}
