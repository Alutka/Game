using Shared.Configuration;
using System;
using System.Drawing;
using System.IO;

namespace MapGenerator
{
    public class PNGMapReader
    {
        private string _mapName;
        private const string PNG_EXTENSION = ".png";

        public PNGMapReader(string mapName)
        {
            _mapName = mapName;
        }

        public Bitmap ReadMap()
        {
            var projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            var path = Path.Combine(projectDirectory, ConfigurationInstance.Config.StoragePaths.DevStatic, _mapName + PNG_EXTENSION);
            return new Bitmap(path, true);
        }
    }
}
