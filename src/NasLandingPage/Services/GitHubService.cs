using RnCore.Logging;

namespace NasLandingPage.Services;

public interface IGitHubService
{
  Task SyncCoreRepoInformationAsync();
}

public class GitHubService : IGitHubService
{
  private readonly ILoggerAdapter<GitHubService> _logger;

  public GitHubService(ILoggerAdapter<GitHubService> logger)
  {
    _logger = logger;
  }

  public async Task SyncCoreRepoInformationAsync()
  {
    _logger.LogInformation("hello world");


    Console.WriteLine();
    Console.WriteLine();
  }
}
