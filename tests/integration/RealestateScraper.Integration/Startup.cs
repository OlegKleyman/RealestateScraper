using System.IO.Abstractions;
using AdCodicem.SpecFlow.MicrosoftDependencyInjection;
using CsvHelper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSubstitute;
using RealestateScraper.Console;
using RealestateScraper.Core;

namespace RealestateScraper.Integration
{
    public class Startup : IServicesConfigurator
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IHostedService, ServiceHost>()
                .AddSingleton(provider => Substitute.For<IHostApplicationLifetime>())
                .AddSingleton(provider => Substitute.For<IRealestateService>())
                .AddSingleton(provider =>
                    new ZillowOutputWriter(new FileSystem(), provider.GetRequiredService<IWriter>()));
        }
    }
}
