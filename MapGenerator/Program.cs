using System;

namespace MapGenerator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Startup.LoadConfiguration();
            var reader = new PNGMapReader("biomes");
            var dupa = reader.ReadMap();
            Console.ReadLine();
        }
    }
}
