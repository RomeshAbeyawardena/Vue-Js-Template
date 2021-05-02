using MediatR;
using PackageManager.Shared.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageManager.Shared.Queries.GetConfigurationFilePaths
{
    public class Query : IRequest<IEnumerable<File>>
    {
        public string Name { get; set; }
    }
}
