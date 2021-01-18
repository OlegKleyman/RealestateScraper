using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Abstractions;
using System.Threading.Tasks;
using CsvHelper;

namespace RealestateScraper.Core
{
    public class ZillowOutputWriter : IOutputWriter
    {
        private readonly IFileSystem _fileSystem;
        private readonly IWriter _writer;

        public ZillowOutputWriter(IFileSystem fileSystem, IWriter writer)
        {
            _fileSystem = fileSystem;
            _writer = writer;
        }

        public Task WriteAsync(string outputPath, IEnumerable<object> targets)
        {
            _fileSystem.Directory.CreateDirectory(outputPath);
            var stream = _fileSystem.File.Create(Path.Combine(outputPath, "output.csv"));
            using var writer = new StreamWriter(stream);
            using var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csvWriter.WriteRecords(targets);
            return Task.CompletedTask;
        }
    }
}