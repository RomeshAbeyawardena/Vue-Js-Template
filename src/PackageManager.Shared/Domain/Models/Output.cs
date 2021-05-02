using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageManager.Shared.Domain.Models
{
    public class Output
    {
        public string Name { get; set; }
        public string Action { get; set; }
        public IEnumerable<File> Files { get; set; }
    }
}
