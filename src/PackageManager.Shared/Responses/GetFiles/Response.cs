using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageManager.Shared.Responses.GetFiles
{
    public class Response
    {
        public IEnumerable<FileInfo> Files { get; set; }
        public DirectoryInfo Directory { get; set; }
    }
}
