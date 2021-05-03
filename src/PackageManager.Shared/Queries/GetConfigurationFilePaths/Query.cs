using MediatR;
using PackageManager.Shared.Domain.Models;
using System.Collections.Generic;

namespace PackageManager.Shared.Queries.GetConfigurationFilePaths
{
    public class Query : IRequest<IEnumerable<File>>
    {
        public string Name { get; set; }
        public bool? RequiresPackageManager { get; set; }
    }
}
