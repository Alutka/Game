using System;

namespace MapGenerator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Startup.LoadConfiguration();
            var reader = new PNGMapReader("DefaultMap");
            var dupa = reader.ReadMap();
            Console.ReadLine();
        }
    }
}
