using NasLandingPage.Factories;
using Octokit;
using RnCore.Logging;

namespace NasLandingPage.Helpers;

public class RepoFileIndexer
{
  private readonly IServiceProvider _serviceProvider;
  private readonly ILoggerAdapter<RepoFileIndexer> _logger;
  private readonly GitHubClient _gitHubClient;
  private readonly long _repoId;

  public RepoFileIndexer(IServiceProvider serviceProvider, long repoId)
  {
    _serviceProvider = serviceProvider;
    _repoId = repoId;
    _logger = serviceProvider.GetRequiredService<ILoggerAdapter<RepoFileIndexer>>();
    _gitHubClient= serviceProvider.GetRequiredService<IGitHubClientFactory>().GetGitHubClient();
  }

  public async Task IndexAsync()
  {
    // await TryIndexDirectoryAsync("");
    // await TryIndexDirectoryAsync(".github", true);
    // await TryIndexDirectoryAsync("docker");
    // await TryIndexDirectoryAsync("docs");
    await TryIndexDirectoryAsync("src", true, 1);
    await TryIndexDirectoryAsync("tests", true, 1);


    Console.WriteLine();
    Console.WriteLine();
  }

  private async Task TryIndexDirectoryAsync(string path, bool recurse = false, int maxDepth = 0, int currentDepth = 0)
  {
    IReadOnlyList<RepositoryContent>? files;

    if (string.IsNullOrWhiteSpace(path))
      files = await _gitHubClient.Repository.Content.GetAllContents(_repoId);
    else
      files = await _gitHubClient.Repository.Content.GetAllContents(_repoId, path);

    if (recurse)
    {
      if(maxDepth > 0 && currentDepth >= maxDepth) return;
      foreach (var dir in files.Where(x => x.Type == ContentType.Dir))
      {
        await TryIndexDirectoryAsync(dir.Path, true, maxDepth, currentDepth++);
      }
    }

    var file = files[0];

    Console.WriteLine();
    Console.WriteLine();
  }
}
