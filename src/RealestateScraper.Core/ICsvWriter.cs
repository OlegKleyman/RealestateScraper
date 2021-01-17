using System.Threading.Tasks;

namespace RealestateScraper.Core
{
    public interface ICsvWriter
    {
        Task WriteAsync(object target);
    }
}