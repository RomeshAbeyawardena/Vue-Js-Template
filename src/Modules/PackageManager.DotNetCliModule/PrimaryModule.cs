using PackageManager.Shared.Abstractions;
using PackageManager.Shared.Base;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PackageManager.DotNetCliModule
{
    public class PrimaryModule : ModuleBase
    {
        private readonly IConfiguration configuration;

        public PrimaryModule(IConfiguration configuration)
            : base(configuration)
        {
            this.configuration = configuration;
        }

        public override Task<bool> CleanUpAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> RunAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
