using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using FluentAssertions;
using Microsoft.Extensions.Hosting;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace RealestateScraper.Integration
{
    [Binding]
    public class ZillowSteps : Steps, IDisposable
    {
        private readonly ScenarioContext _context;
        private readonly IHostedService _service;

        public ZillowSteps(ScenarioContext context, IHostedService service)
        {
            _context = context;
            _service = service;
        }

        [Given(@"I want to get the following data from zillow")]
        public void GivenIWantToGetTheFollowingDataFromZillow(Table table)
        {
            var record = table.CreateSet<ZillowRecord>().ToArray();
            _context.Set(record);
        }

        [When(@"I run the application")]
        public async Task WhenIRunTheApplication()
        {
            await _service.StartAsync(CancellationToken.None);
        }

        [Then(@"I will get the price back in the CSV")]
        public void ThenIWillGetThePriceBackInTheCsv()
        {
            using var reader = new StreamReader(@"artifacts\results.csv");
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);

            var result = csvReader.GetRecords<ZillowRecord>();

            var zillowRecords = _context.Get<ZillowRecord[]>();

            result.Should().BeEquivalentTo(zillowRecords);
        }

        [Given(@"I want the output path to be ""(.*)""")]
        public void GivenIWantTheOutputPathToBe(string path)
        {
            _context.Set(path, "OUTPUT_PATH");
        }

        [Then(@"a file is created in the output path")]
        public void ThenAFileIsCreatedInTheOutputPath()
        {
            File.Exists(_context.Get<string>("OUTPUT_PATH")).Should().BeTrue();
        }


        public void Dispose()
        {
            _service.StopAsync(CancellationToken.None);
        }
    }
}
