using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvSite.Service
{
    public interface IGitHubService
    {
        Task<IEnumerable<RepositoryInfo>> GetPortfolioAsync();
        Task<IEnumerable<RepositoryInfo>> SearchRepositoriesAsync(string name, string language, string username);
    }

}
