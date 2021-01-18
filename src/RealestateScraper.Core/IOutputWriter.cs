using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealestateScraper.Core
{
    public interface IOutputWriter
    {
        Task WriteAsync(string outputPath, IEnumerable<object> targets);
    }
}