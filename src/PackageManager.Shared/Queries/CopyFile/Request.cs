using MediatR;

namespace PackageManager.Shared.Queries.CopyFile
{
    public class Request : IRequest
    {
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        public bool OverWriteFile { get; set; }
        public bool CreateSubDirectories { get; set; }
    }
}
