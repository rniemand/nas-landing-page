using NasLandingPage.Factories;
using NasLandingPage.Helpers;
using NasLandingPage.Models.Dto;
using NasLandingPage.Models.Entities;
using NasLandingPage.Repos;
using Octokit;
using RnCore.Logging;

namespace NasLandingPage.Services;

public interface IGitHubService
{
  Task SyncCoreRepoInformationAsync();
  Task<List<GitHubRepoDto>> ListReposAsync();
  Task<bool> SyncRepoFilesAsync(GitHubRepoEntity repo);
}

public class GitHubService : IGitHubService
{
  private readonly ILoggerAdapter<GitHubService> _logger;
  private readonly IGitHubClientFactory _ghClientFactory;
  private readonly IGitHubRepoRepo _ghRepoRepo;
  private readonly IServiceProvider _serviceProvider;

  public GitHubService(ILoggerAdapter<GitHubService> logger,
    IGitHubClientFactory ghClientFactory,
    IGitHubRepoRepo ghRepoRepo,
    IServiceProvider serviceProvider)
  {
    _logger = logger;
    _ghClientFactory = ghClientFactory;
    _ghRepoRepo = ghRepoRepo;
    _serviceProvider = serviceProvider;
  }

  public async Task SyncCoreRepoInformationAsync()
  {
    var dbRepos = await _ghRepoRepo.GetReposAsync();
    var repos = (await _ghClientFactory.GetGitHubClient().Repository.GetAllForCurrent())
      .Where(x => x.Owner.Login.Equals("rniemand", StringComparison.InvariantCultureIgnoreCase))
      .ToList();
    
    foreach (var repo in repos)
    {
      await AddOrUpdateRepoAsync(dbRepos, MapEntity(repo));
    }
  }

  public async Task<List<GitHubRepoDto>> ListReposAsync()
  {
    var dbRepos = await _ghRepoRepo.GetReposAsync();
    return dbRepos.Select(GitHubRepoDto.FromEntity).ToList();
  }

  public async Task<bool> SyncRepoFilesAsync(GitHubRepoEntity repo)
  {
    await new RepoFileIndexer(_serviceProvider, repo.RepoID).IndexAsync();

    return true;
  }

  // Internal methods
  private async Task AddOrUpdateRepoAsync(IEnumerable<GitHubRepoEntity> dbRepos, GitHubRepoEntity repo)
  {
    var exists = dbRepos.Any(x => x.RepoID == repo.RepoID);

    if (exists)
    {
      _logger.LogInformation("Updating existing repo: ({id}) {name}", repo.RepoID, repo.FullName);
      await _ghRepoRepo.UpdateRepoAsync(repo);
      return;
    }

    _logger.LogInformation("Indexing new repo: ({id}) {name}", repo.RepoID, repo.FullName);
    await _ghRepoRepo.AddRepoAsync(repo);
  }

  private static GitHubRepoEntity MapEntity(Repository repo) =>
    new()
    {
      CreatedAt = repo.CreatedAt,
      DefaultBranch = repo.DefaultBranch,
      Description = repo.Description,
      ForksCount = repo.ForksCount,
      FullName = repo.FullName,
      GitUrl = repo.GitUrl,
      HasDownloads = repo.HasDownloads,
      HasIssues = repo.HasIssues,
      HasPages = repo.HasPages,
      HasWiki = repo.HasWiki,
      IsArchived = repo.Archived,
      IsFork = repo.Fork,
      IsPrivate = repo.Private,
      IsTemplate = repo.IsTemplate,
      License = repo.License?.Name ?? "",
      Name = repo.Name,
      OpenIssuesCount = repo.OpenIssuesCount,
      PushedAt = repo.PushedAt,
      RepoID = repo.Id,
      RepoSize = repo.Size,
      SshUrl = repo.SshUrl,
      StargazersCount = repo.StargazersCount,
      SubscribersCount = repo.SubscribersCount,
      UpdatedAt = repo.UpdatedAt,
    };
}
