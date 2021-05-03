using MediatR;
using PackageManager.Shared.Abstractions;
using PackageManager.Shared.Domain.Models;
using PackageManager.Shared.Queries.GetConfigurationFilePaths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PackageManager.Core.Feature.GetConfigurationFilePaths
{
    public class Handler : IRequestHandler<Query, IEnumerable<File>>
    {
        private readonly IConfiguration configuration;

        public Handler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Task<IEnumerable<File>> Handle(Query request, CancellationToken cancellationToken)
        {
            return Task.FromResult(configuration
                .Outputs.FirstOrDefault(a => a.Name.Equals(request.Name, 
                    StringComparison.InvariantCultureIgnoreCase))
                        .Files.Where(a => !request.RequiresPackageManager.HasValue 
                            || a.RequiresPackageManager == request.RequiresPackageManager.Value));
        }
    }
}
