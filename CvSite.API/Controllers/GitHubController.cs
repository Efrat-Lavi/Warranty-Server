using CvSite.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CvSite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GitHubController : ControllerBase
    {
        private readonly IGitHubService _service;

        public GitHubController(IGitHubService service)
        {
            _service = service;
        }

        [HttpGet("portfolio")]
        public async Task<IActionResult> GetPortfolio()
        {
            var result = await _service.GetPortfolioAsync();
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string? name, string? language, string? username)
        {
            var result = await _service.SearchRepositoriesAsync(name, language, username);
            return Ok(result);
        }
    }

}
