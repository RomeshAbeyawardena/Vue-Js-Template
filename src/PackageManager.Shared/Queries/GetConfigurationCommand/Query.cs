using MediatR;
using PackageManager.Shared.Domain.Models;

namespace PackageManager.Shared.Queries.GetConfigurationCommand
{
    public class Query : IRequest<Command>
    {
        public string Key { get; set; }
    }
}
