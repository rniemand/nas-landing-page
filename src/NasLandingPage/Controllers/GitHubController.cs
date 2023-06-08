using Microsoft.AspNetCore.Mvc;
using NasLandingPage.Models.Dto;
using NasLandingPage.Services;

namespace NasLandingPage.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GitHubController : ControllerBase
{
  private readonly IGitHubService _gitHubService;

  public GitHubController(IGitHubService gitHubService)
  {
    _gitHubService = gitHubService;
  }

  [Route("trigger-index")]
  public async Task<string> TriggerRepoIndex()
  {
    await _gitHubService.SyncCoreRepoInformationAsync();
    return "OK";
  }

  [Route("repo/list")]
  public async Task<List<GitHubRepoDto>> ListRepos() => await _gitHubService.ListReposAsync();
}
