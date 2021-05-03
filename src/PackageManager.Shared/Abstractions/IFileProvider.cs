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
        IEnumerable<FileInfo> GetFiles(DirectoryInfo directoryInfo, IEnumerable<string> extensions, char delimiter);
    }
}
