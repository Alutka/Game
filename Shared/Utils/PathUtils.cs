using System;
using System.IO;

namespace Shared.Utils
{
    public static class PathUtils
    {
        public static string GetPath(string relativePath)
        {
            return Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, relativePath);
        }
    }
}
