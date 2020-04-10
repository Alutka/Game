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
            var mapIO = new MapIO();
            mapIO.Export(map);
            Console.WriteLine("Map generated!");
        }
    }
}
