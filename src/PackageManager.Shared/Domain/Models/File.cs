using System.Collections.Generic;

namespace PackageManager.Shared.Domain.Models
{
    public class File
    {
        public IEnumerable<FileExtension> FileExtensions { get; set; }
        public string Source { get; set; }
        public string To { get; set; }
        public bool RequiresPackageManager { get; set; }
    }
}
