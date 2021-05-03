using PackageManager.Shared.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PackageManager.Shared
{
    public class FileProvider : IFileProvider
    {
        private IEnumerable<FileInfo> FilterFiles(IEnumerable<string> allowedExtensions, IEnumerable<FileInfo> files)
        {
            return files.Where(a => allowedExtensions.Any(b => a.Name.EndsWith(b)));
        }

        public IEnumerable<FileInfo> GetFiles(DirectoryInfo directory)
        {
            var directories = directory.GetDirectories("*", SearchOption.AllDirectories);
            return directories.SelectMany(a => a.GetFiles());
        }

        public IEnumerable<FileInfo> GetFiles(DirectoryInfo directoryInfo, IEnumerable<string> extensions, char delimiter)
        {
            
            var fileList = new List<FileInfo>();
            var directories = Directory.EnumerateDirectories(directoryInfo.FullName, "*", SearchOption.AllDirectories);
            foreach (var directory in directories)
            {
                var currentDirectoryInfo = new DirectoryInfo(directory);

                fileList.AddRange(FilterFiles(extensions, currentDirectoryInfo.GetFiles()));
            }

            fileList.AddRange(FilterFiles(extensions, directoryInfo.GetFiles()));

            return fileList;
        }
    }
}
