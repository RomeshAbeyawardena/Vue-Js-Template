using MediatR;
using Microsoft.Extensions.Logging;
using PackageManager.Shared.Abstractions;
using PackageManager.Shared.Base;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PackageManager.Shared.Extensions;

namespace PackageManager.DotNetCliModule
{
    public class TestModule : ModuleBase
    {
        private readonly ILogger<TestModule> logger;

        public TestModule(IConfiguration configuration,
            IMediator mediator,
            ILogger<TestModule> logger) : base(logger, configuration, mediator)
        {
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
                logger.LogInformation("Getting files for {0}", output.Source);
                //var extensions = string.Join(',', output.FileExtensions.Select(a => a.Value));
                var files = await Mediator.Send(new Shared.Queries.GetFiles.Query
                {
                    ExtensionDelimiter = ',',
                    Extensions = output.FileExtensions,
                    FilePath = output.Source
                });
                
                foreach (var file in files.Files)
                {
                    logger.LogInformation("Full Name:\t{0}\r\nDirectory Name:\t{1}", 
                        output.To.Concat(file.FullName.Replace(files.Directory.FullName, string.Empty)), 
                        files.Directory.FullName);
                }
            }
            

            return true;
        }
    }
}
