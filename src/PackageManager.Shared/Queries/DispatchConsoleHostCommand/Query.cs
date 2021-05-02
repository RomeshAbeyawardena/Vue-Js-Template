using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageManager.Shared.Queries.DispatchConsoleHostCommand
{
    public class Query : IRequest
    {
        public string Key { get; set; }
        public string Arguments { get; set; }
    }
}
