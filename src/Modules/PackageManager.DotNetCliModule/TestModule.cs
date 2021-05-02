using MediatR;
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
            IMediator mediator,
            ILogger<TestModule> logger) : base(logger, configuration, mediator)
        {
            this.fileProvider = fileProvider;
            this.logger = logger;
        }

        public override Task<bool> CleanUpAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override async Task<bool> RunAsync(CancellationToken cancellationToken)
        {
            var outputs = await Mediator.Send(new Shared.Queries.GetConfigurationFilePaths.Query { Name = "Web" }, cancellationToken);

            foreach(var output in outputs)
            {
                var extensions = string.Join(',', output.FileExtensions.Select(a => a.Value));
                var files = fileProvider.GetFiles(output.Source, extensions, ',');

                foreach (var file in files)
                {
                    logger.LogInformation(file.FullName);
                }
            }
            

            return true;
        }
    }
}
