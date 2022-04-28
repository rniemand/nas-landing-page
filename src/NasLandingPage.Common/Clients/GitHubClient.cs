using Microsoft.Extensions.DependencyInjection;
using NasLandingPage.Common.Models;
using NasLandingPage.Common.Services;
using Octokit;

namespace NasLandingPage.Common.Clients;

public interface INlpGitHubClient
{
  Task<Repository> GetRepositoryAsync(long repositoryId);
}

public class NlpGitHubClient : INlpGitHubClient
{
  private readonly IGitHubClient _gitHubClient;

  public NlpGitHubClient(IServiceProvider serviceProvider)
  {
    // TODO: [NlpGitHubClient.NlpGitHubClient] (TESTS) Add tests
    _gitHubClient = CreateGitHubClient(serviceProvider);
  }


  public async Task<Repository> GetRepositoryAsync(long repositoryId)
  {
    // TODO: [NlpGitHubClient.GetRepositoryAsync] (TESTS) Add tests
    return await _gitHubClient.Repository.Get(repositoryId);
  }


  private IGitHubClient CreateGitHubClient(IServiceProvider serviceProvider)
  {
    // TODO: [NlpGitHubClient.CreateGitHubClient] (TESTS) Add tests
    var credentials = GetCredentials(serviceProvider);
    var github = new GitHubClient(new ProductHeaderValue("NasLandingPage"));

    if (!string.IsNullOrWhiteSpace(credentials.Password))
    {
      var basicAuth = new Credentials(credentials.Username, credentials.Password);
      github.Credentials = basicAuth;
    }

    return github;
  }

  private static BasicCredentials GetCredentials(IServiceProvider serviceProvider)
  {
    // TODO: [NlpGitHubClient.GetCredentials] (TESTS) Add tests
    var credentialsService = serviceProvider.GetRequiredService<ICredentialsService>();
    return credentialsService.GetCredentials("github");
  }
}
