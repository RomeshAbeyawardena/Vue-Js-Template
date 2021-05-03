using System.Collections.Generic;

namespace PackageManager.Shared.Domain.Models
{
    public class Output
    {
        public string Name { get; set; }
        public string Action { get; set; }
        public IEnumerable<File> Files { get; set; }
    }
}
