using Shared.Configuration;
using System.IO;

namespace StaticFilesIO
{
    public static class FileProvider
    {
        public static Stream GetMapExportStream()
        {
            string mapPath = GetMapPath();
            if (File.Exists(mapPath))
            {
                File.Delete(mapPath);
            }
            return File.Create(mapPath);
        }

        public static Stream GetMapImportStream()
        {
            return File.OpenRead(GetMapPath());
        }

        private static string GetMapPath()
        {
            string mapDirectory = Path.Combine(ConfigurationInstance.Config.StoragePaths.Static, ConfigurationInstance.Config.StoragePaths.Maps);
            return Path.Combine(mapDirectory, ConfigurationInstance.Config.Files.MapFile + ConfigurationInstance.Config.Files.MapExtension);
        }
    }
}
