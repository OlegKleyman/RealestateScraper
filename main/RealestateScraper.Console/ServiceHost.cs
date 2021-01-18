using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using RealestateScraper.Core;

namespace RealestateScraper.Console
{
    public class ServiceHost : IHostedService
    {
        private readonly IHostApplicationLifetime _lifetime;
        private readonly IRealestateService _realestateService;
        private readonly IOutputWriter _outputWriter;

        public ServiceHost(IHostApplicationLifetime lifetime, IRealestateService realestateService, IOutputWriter outputWriter, string outputPath)
        {
            _lifetime = lifetime;
            _realestateService = realestateService;
            _outputWriter = outputWriter;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var results = await _realestateService.GetAllAsync();
            await _outputWriter.WriteAsync("output", results);
            _lifetime.StopApplication();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}