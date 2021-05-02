using MediatR;
using PackageManager.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageManager.Shared.Queries.GetConsoleHost
{
    public class Query : IRequest<IConsoleHost>
    {
        public string Key { get; set; }
    }
}
