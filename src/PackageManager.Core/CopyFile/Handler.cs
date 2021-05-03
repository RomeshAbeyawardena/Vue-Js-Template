using MediatR;
using PackageManager.Shared.Queries.CopyFile;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PackageManager.Core.CopyFile
{
    public class Handler : IRequestHandler<Request>
    {
        public Task<Unit> Handle(Request request, CancellationToken cancellationToken)
        {
            if (request.CreateSubDirectories)
            {
                var fileInfo = new FileInfo(request.DestinationPath);
                Directory.CreateDirectory(fileInfo.Directory.FullName);
            }

            File.Copy(request.SourcePath, 
                request.DestinationPath, 
                request.OverWriteFile);

            return Unit.Task;
        }
    }
}
