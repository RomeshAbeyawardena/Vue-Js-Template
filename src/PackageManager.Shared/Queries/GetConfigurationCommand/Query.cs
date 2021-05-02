using MediatR;
using PackageManager.Shared.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageManager.Shared.Queries.GetConfigurationCommand
{
    public class Query : IRequest<Command>
    {
        public string Key { get; set; }
    }
}
