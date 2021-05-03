using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageManager.Shared.Queries.DispatchConsoleHostCommand
{
    public class Query : IRequest
    {
        public Query(Action<IDictionary<string, string>> action = default)
        {
            Parameters = new Dictionary<string, string>();
            action?.Invoke(Parameters);
        }

        public string Key { get; set; }
        public IDictionary<string, string> Parameters { get; }
        public string WorkingDirectory { get; set; }
        public string Arguments { get; set; }
    }
}
