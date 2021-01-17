using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;

namespace RealestateScraper.Core
{
    public class RealestateService : IRealestateService
    {
        private readonly HttpClient _client;

        public RealestateService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IReadOnlyCollection<RealestateResult>> GetAllAsync()
        {
            var response = await _client.GetStreamAsync("/");
            var document = new HtmlDocument();
            document.Load(response);

            var results = document.DocumentNode.QuerySelectorAll("div div")
                .Select(node => new RealestateResult(decimal.Parse(node.InnerText, CultureInfo.InvariantCulture)))
                .ToArray();

            return results;
        }
    }
}