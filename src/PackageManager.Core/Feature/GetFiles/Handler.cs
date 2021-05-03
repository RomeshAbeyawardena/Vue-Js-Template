using MediatR;
using PackageManager.Shared.Abstractions;
using PackageManager.Shared.Queries.GetFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PackageManager.Core.Feature.GetFiles
{
    public class Handler : IRequestHandler<Query, Shared.Responses.GetFiles.Response>
    {
        private readonly IFileProvider fileProvider;

        public Handler(IFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
        }

        public Task<Shared.Responses.GetFiles.Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var extensions = request.Extensions.Select(a => a.Value.Trim('*'));

            var directoryInfo = new DirectoryInfo(request.FilePath);
            var files = fileProvider.GetFiles(directoryInfo, 
                extensions, request.ExtensionDelimiter);

            return Task.FromResult(new Shared.Responses.GetFiles.Response
            {
                Files = files,
                Directory = directoryInfo
            });
        }
    }
}
