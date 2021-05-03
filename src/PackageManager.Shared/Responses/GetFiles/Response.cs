using System.Collections.Generic;
using System.IO;

namespace PackageManager.Shared.Responses.GetFiles
{
    public class Response
    {
        public IEnumerable<FileInfo> Files { get; set; }
        public DirectoryInfo Directory { get; set; }
    }
}
