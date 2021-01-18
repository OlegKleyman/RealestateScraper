using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Threading.Tasks;
using CsvHelper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace RealestateScraper.Core.Tests.Unit
{
    public class ZillowCsvWriterTests
    {
        [Fact]
        public async Task WriteAsyncCreateTheOutputFile()
        {
            var fileSystem = new MockFileSystem();
            var csvWriter = Substitute.For<IWriter>();

            var writer = CreateZillowCsvWriter(fileSystem, csvWriter);
            var zillowRecords = new[]
            {
                new RealestateResult(10)
            };
            
            await writer.WriteAsync("output", zillowRecords);

            fileSystem.File.Exists(@"output\output.csv").Should().BeTrue();
        }

        private ZillowOutputWriter CreateZillowCsvWriter(IFileSystem fileSystem, IWriter writer)
        {
            return new ZillowOutputWriter(fileSystem, writer);
        }
    }
}
