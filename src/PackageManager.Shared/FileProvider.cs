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
        private IEnumerable<FileInfo> FilterFiles(IEnumerable<string> allowedExtensions, IEnumerable<FileInfo> files)
        {
            return files.Where(a => allowedExtensions.Any(b => a.Name.EndsWith(b)));
        }

        public IEnumerable<FileInfo> GetFiles(DirectoryInfo directory)
        {
            var directories = directory.GetDirectories("*", SearchOption.AllDirectories);
            return directories.SelectMany(a => a.GetFiles());
        }

        public IEnumerable<FileInfo> GetFiles(string path, string extensions, char delimiter)
        {
            var extensionList = extensions
                .Split(delimiter, StringSplitOptions.RemoveEmptyEntries)
                .Select(a => a.Trim('*'));

            var fileList = new List<FileInfo>();
            var directories = Directory.EnumerateDirectories(path, "*", SearchOption.AllDirectories);
            foreach (var directory in directories)
            {
                var directoryInfo = new DirectoryInfo(directory);

                fileList.AddRange(FilterFiles(extensionList, directoryInfo.GetFiles()));
            }

            fileList.AddRange(FilterFiles(extensionList, new DirectoryInfo(path).GetFiles()));

            return fileList;
        }
    }
}
