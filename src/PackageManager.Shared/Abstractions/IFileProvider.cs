using System.Collections.Generic;
using System.IO;

namespace PackageManager.Shared.Abstractions
{
    public interface IFileProvider
    {
        IEnumerable<FileInfo> GetFiles(DirectoryInfo directory);
        IEnumerable<FileInfo> GetFiles(DirectoryInfo directoryInfo, IEnumerable<string> extensions, char delimiter);
    }
}
