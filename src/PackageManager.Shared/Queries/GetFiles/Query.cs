using MediatR;
using PackageManager.Shared.Domain.Models;
using System.Collections.Generic;

namespace PackageManager.Shared.Queries.GetFiles
{
    public class Query : IRequest<Responses.GetFiles.Response>
    {
        public string FilePath { get; set; }
        public IEnumerable<FileExtension> Extensions { get; set; }
        public char ExtensionDelimiter { get; set; }
    }
}
