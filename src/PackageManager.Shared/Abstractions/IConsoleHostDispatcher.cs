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
        Task Dispatch(IConsoleHost consoleHost, string arguments, 
            string workingDirectory = default, 
            CancellationToken cancellationToken = default);

        IConsoleHost DefaultConsoleHost { get; }
    }
}
