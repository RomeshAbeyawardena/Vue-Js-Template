using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PackageManager.Shared.Abstractions
{
    public interface IConsoleHostDispatcher
    {
        Task Dispatch(IConsoleHost consoleHost, string arguments, CancellationToken cancellationToken);
        IConsoleHost DefaultConsoleHost { get; }
    }
}
