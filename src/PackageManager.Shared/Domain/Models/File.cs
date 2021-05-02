using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageManager.Shared.Domain.Models
{
    public class File
    {
        public IEnumerable<FileExtension> FileExtensions { get; set; }
        public string Source { get; set; }
        public string To { get; set; }
    }
}
