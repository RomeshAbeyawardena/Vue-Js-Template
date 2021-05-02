using Microsoft.Extensions.Logging;
using PackageManager.Shared.Abstractions;
using PackageManager.Shared.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PackageManager.DotNetCliModule
{
    public class TestModule : ModuleBase
    {
        private readonly IFileProvider fileProvider;
        private readonly ILogger<TestModule> logger;

        public TestModule(IConfiguration configuration,
            IFileProvider fileProvider,
            ILogger<TestModule> logger) : base(configuration)
        {
            this.fileProvider = fileProvider;
            this.logger = logger;
        }

        public override Task<bool> CleanUpAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> RunAsync(CancellationToken cancellationToken)
        {
            var outputs = Configuration.Outputs;
            var files = fileProvider.GetFiles("Templates");
            foreach (var file in files)
            {
                logger.LogInformation(file.FullName);
            }
            return Task.FromResult(true);
        }
    }
}
