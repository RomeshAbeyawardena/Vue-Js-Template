using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageManager.Shared.Domain.Models
{
    public class File
    {
        public string Filter { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}
