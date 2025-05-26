using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Octokit;

namespace CvSite.Service
{
  
    public class GitHubService : IGitHubService
    {
        private readonly GitHubClient _client;
        private readonly IMemoryCache _cache;
        private readonly GitHubSettings _settings;

        public GitHubService(IOptions<GitHubSettings> options, IMemoryCache cache)
        {
            _settings = options.Value;
            _cache = cache;
            _client = new GitHubClient(new ProductHeaderValue("CvSite"))
            {
                Credentials = new Credentials(_settings.Token)
            };
        }

        public async Task<IEnumerable<RepositoryInfo>> GetPortfolioAsync()
        {
            const string cacheKey = "GitHubPortfolio";

            if (_cache.TryGetValue(cacheKey, out IEnumerable<RepositoryInfo> cachedRepos))
            {
                return cachedRepos;
            }

            var repositories = await _client.Repository.GetAllForUser(_settings.Username);

            var result = new List<RepositoryInfo>();

            foreach (var repo in repositories)
            {
                // נשלוף PR-ים פתוחים (נכון לעכשיו)
                var pulls = await _client.PullRequest.GetAllForRepository(_settings.Username, repo.Name);

                result.Add(new RepositoryInfo
                {
                    Name = repo.Name,
                    Url = repo.HtmlUrl,
                    Language = repo.Language,
                    LastCommit = repo.PushedAt?.DateTime ?? repo.UpdatedAt.DateTime,
                    Stars = repo.StargazersCount,
                    PullRequests = pulls.Count
                });
            }

            _cache.Set(cacheKey, result, TimeSpan.FromMinutes(5));
            return result;
        }



        public async Task<IEnumerable<RepositoryInfo>> SearchRepositoriesAsync(string name, string language, string username)
        {
            var repositories = await _client.Repository.GetAllForUser(username);
            var filtered = repositories.Where(repo =>
                (string.IsNullOrEmpty(name) || repo.Name.Contains(name, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(language) || string.Equals(repo.Language, language, StringComparison.OrdinalIgnoreCase))
            );

            var result = new List<RepositoryInfo>();

            foreach (var repo in filtered)
            {
                var pulls = await _client.PullRequest.GetAllForRepository(username, repo.Name);

                result.Add(new RepositoryInfo
                {
                    Name = repo.Name,
                    Url = repo.HtmlUrl,
                    Language = repo.Language,
                    LastCommit = repo.PushedAt?.DateTime ?? repo.UpdatedAt.DateTime,
                    Stars = repo.StargazersCount,
                    PullRequests = pulls.Count
                });
            }

            return result;
        }

    }

}
