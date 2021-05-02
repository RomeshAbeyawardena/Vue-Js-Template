using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageManager.Shared.Abstractions
{
    public interface IFileProvider
    {
        IEnumerable<FileInfo> GetFiles(DirectoryInfo directory);
        IEnumerable<FileInfo> GetFiles(string path, string extensions, char delimiter);
    }
}
