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
        private readonly ICsvWriter _csvWriter;

        public ServiceHost(IHostApplicationLifetime lifetime, IRealestateService realestateService, ICsvWriter csvWriter)
        {
            _lifetime = lifetime;
            _realestateService = realestateService;
            _csvWriter = csvWriter;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var results = _realestateService.GetAllAsync();
            _csvWriter.WriteAsync(results);
            _lifetime.StopApplication();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}