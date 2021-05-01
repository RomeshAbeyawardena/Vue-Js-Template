using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageManager.Shared.Domain.Models
{
    public class File
    {
        public bool Enabled { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }
}
