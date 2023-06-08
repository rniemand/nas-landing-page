using Microsoft.Extensions.DependencyInjection;
using NasLandingPage.Models;
using NasLandingPage.Models.Entities;
using NasLandingPage.Repos;
using Octokit;
using Octokit.Internal;

namespace DevConsole;

internal class Program
{
  static async Task Main(string[] args)
  {
    // https://octokitnet.readthedocs.io/en/latest/
    var ghClient = GetGitHubClient();

    var repos = (await ghClient.Repository.GetAllForCurrent())
      .Where(x => x.Owner.Login.Equals("rniemand", StringComparison.InvariantCultureIgnoreCase))
      .ToList();

    var ghRepoRepo = DIContainer.Services.GetRequiredService<IGitHubRepoRepo>();

    foreach (var repo in repos)
    {
      Console.WriteLine($"Adding: {repo.FullName}");
      await ghRepoRepo.AddRepoAsync(MapEntity(repo));
    }


    Console.WriteLine("Hello, World!");
    Console.WriteLine();
  }


  private static GitHubClient GetGitHubClient()
  {
    var config = DIContainer.Services.GetRequiredService<AppConfig>();
    var token = File.ReadAllText(config.GithubTokenFilePath);
    
    return new GitHubClient(
      new ProductHeaderValue("my-cool-app"),
      new InMemoryCredentialStore(new Credentials(token))
    );
  }

  private static GitHubRepoEntity MapEntity(Repository repo) =>
    new()
    {
      CreatedAt = repo.CreatedAt,
      DefaultBranch = repo.DefaultBranch,
      Description = repo.Description ,
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
