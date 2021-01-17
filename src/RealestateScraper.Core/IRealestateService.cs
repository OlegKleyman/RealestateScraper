using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealestateScraper.Core
{
    public interface IRealestateService
    {
        Task<IReadOnlyCollection<RealestateResult>> GetAllAsync();
    }
}