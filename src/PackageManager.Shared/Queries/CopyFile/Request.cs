using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageManager.Shared.Queries.CopyFile
{
    public class Request : IRequest
    {
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        public bool OverWriteFile { get; set; }
    }
}
