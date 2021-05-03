using MediatR;
using PackageManager.Shared.Queries.CopyFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PackageManager.Core.CopyFile
{
    public class Handler : IRequestHandler<Request>
    {
        public Task<Unit> Handle(Request request, CancellationToken cancellationToken)
        {
            File.Copy(request.SourcePath, 
                request.DestinationPath, 
                request.OverWriteFile);

            return Unit.Task;
        }
    }
}
