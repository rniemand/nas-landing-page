using NasLandingPage.Models;
using Octokit.Internal;
using Octokit;
using RnCore.Abstractions;

namespace NasLandingPage.Factories;

public interface IGitHubClientFactory
{
  GitHubClient GetGitHubClient();
}

public class GitHubClientFactory : IGitHubClientFactory
{
  private readonly AppConfig _config;
  private readonly IFileAbstraction _file;

  public GitHubClientFactory(AppConfig config, IFileAbstraction file)
  {
    _config = config;
    _file = file;
  }

  public GitHubClient GetGitHubClient()
  {
    // https://octokitnet.readthedocs.io/en/latest/
    var token = _file.ReadAllText(_config.GithubTokenFilePath);
    return new GitHubClient(
      new ProductHeaderValue(_config.GitHubClientName),
      new InMemoryCredentialStore(new Credentials(token)));
  }
}
