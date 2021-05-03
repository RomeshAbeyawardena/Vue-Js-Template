using PackageManager.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageManager.Shared.Domain.Models
{
    public class Command
    {
        public string Action { get; set; }

        public bool Enabled { get; set; }

        public string Key { get; set; }
        
        public string Value { get; set; }

        public string WorkingDirectory { get; set; }
    }
}
