using PackageManager.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageManager.Shared
{
    public class FileProvider : IFileProvider
    {
        public IEnumerable<FileInfo> GetFiles(DirectoryInfo directory)
        {
            return directory
                .GetDirectories()
                .SelectMany(a => a.GetDirectories())
                .SelectMany(a => a.GetFiles())
                .Union(directory.GetFiles());
        }

        public IEnumerable<FileInfo> GetFiles(string path)
        {
            var directory = new DirectoryInfo(path);
            return GetFiles(directory);
        }
    }
}
