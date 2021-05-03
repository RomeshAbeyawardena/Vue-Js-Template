using MediatR;
using PackageManager.Shared.Abstractions;

namespace PackageManager.Shared.Queries.GetConsoleHost
{
    public class Query : IRequest<IConsoleHost>
    {
        public string Key { get; set; }
    }
}
