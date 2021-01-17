using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RealestateScraper.Console
{
    public class Program
    {
        public static async Task Main()
        {
            await Host.CreateDefaultBuilder()
                .ConfigureServices(collection =>
                {
                    collection.AddHostedService<ServiceHost>();
                })
                .RunConsoleAsync();
        }
    }
}
