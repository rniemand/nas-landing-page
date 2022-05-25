using NasLandingPage.Common.Builders;
using NasLandingPage.Common.Clients;
using NasLandingPage.Common.Models.Responses.Projects;
using Octokit;

namespace NasLandingPage.Common.Sync;

public interface ICoreRepositoryInfoSync
{
  Task SyncAsync(RunCommandResponseBuilder responseBuilder, ProjectInfo projectInfo);
}

public class CoreRepositoryInfoSync : ICoreRepositoryInfoSync
{
  private readonly INlpGitHubClient _gitHubClient;

  public CoreRepositoryInfoSync(INlpGitHubClient gitHubClient)
  {
    _gitHubClient = gitHubClient;
  }

  public async Task SyncAsync(RunCommandResponseBuilder responseBuilder, ProjectInfo projectInfo)
  {
    var repository = await _gitHubClient.GetRepositoryAsync(projectInfo.Repo.RepoId);
    var messages = new List<string>();

    SyncDefaultBranch(messages, projectInfo, repository);
    SyncIsPublic(messages, projectInfo, repository);
    SyncForkCount(messages, projectInfo, repository);
    SyncHtmlUrl(messages, projectInfo, repository);
    SyncFullName(messages, projectInfo, repository);
    SyncGitUrl(messages, projectInfo, repository);
    SyncOpenIssueCount(messages, projectInfo, repository);
    SyncSshUrl(messages, projectInfo, repository);
    SyncApiUrl(messages, projectInfo, repository);
    SyncRepoSize(messages, projectInfo, repository);
    SyncDescription(messages, projectInfo, repository);

    responseBuilder.WithMessages(messages);
  }

  private static void SyncDefaultBranch(ICollection<string> messages, ProjectInfo projectInfo, Repository repository)
  {
    if (projectInfo.Repo.DefaultBranch == repository.DefaultBranch)
      return;

    messages.Add($"Set default branch to: {repository.DefaultBranch}");
    projectInfo.Repo.DefaultBranch = repository.DefaultBranch;
  }

  private static void SyncIsPublic(ICollection<string> messages, ProjectInfo projectInfo, Repository repository)
  {
    var repoIsPublic = repository.Visibility == RepositoryVisibility.Public;
    if (projectInfo.Repo.IsPublic == repoIsPublic)
      return;

    messages.Add("Set visibility to: " + (repoIsPublic ? "public" : "private"));
    projectInfo.Repo.IsPublic = repoIsPublic;
  }

  private static void SyncForkCount(ICollection<string> messages, ProjectInfo projectInfo, Repository repository)
  {
    if (projectInfo.Repo.ForksCount == repository.ForksCount)
      return;

    messages.Add($"Updated forked count to: {repository.ForksCount}");
    projectInfo.Repo.ForksCount = repository.ForksCount;
  }

  private static void SyncHtmlUrl(ICollection<string> messages, ProjectInfo projectInfo, Repository repository)
  {
    if (projectInfo.Repo.HtmlUrl == repository.HtmlUrl)
      return;

    messages.Add($"Set Html URL to: {repository.HtmlUrl}");
    projectInfo.Repo.HtmlUrl = repository.HtmlUrl;
  }

  private static void SyncFullName(ICollection<string> messages, ProjectInfo projectInfo, Repository repository)
  {
    if (projectInfo.Repo.FullName == repository.FullName)
      return;

    messages.Add($"Set full name to: {repository.FullName}");
    projectInfo.Repo.FullName = repository.FullName;
  }

  private static void SyncGitUrl(ICollection<string> messages, ProjectInfo projectInfo, Repository repository)
  {
    if (projectInfo.Repo.GitUrl == repository.GitUrl)
      return;

    messages.Add($"Updated git URL to: {repository.GitUrl}");
    projectInfo.Repo.GitUrl = repository.GitUrl;
  }

  private static void SyncOpenIssueCount(ICollection<string> messages, ProjectInfo projectInfo, Repository repository)
  {
    if (projectInfo.Repo.OpenIssuesCount == repository.OpenIssuesCount)
      return;

    messages.Add($"Updated open issue count to: {repository.OpenIssuesCount}");
    projectInfo.Repo.OpenIssuesCount = repository.OpenIssuesCount;
  }

  private static void SyncSshUrl(ICollection<string> messages, ProjectInfo projectInfo, Repository repository)
  {
    if (projectInfo.Repo.SshUrl == repository.SshUrl)
      return;

    messages.Add($"Updated SSH URL to: {repository.SshUrl}");
    projectInfo.Repo.SshUrl = repository.SshUrl;
  }

  private static void SyncApiUrl(ICollection<string> messages, ProjectInfo projectInfo, Repository repository)
  {
    if (projectInfo.Repo.ApiUrl == repository.Url)
      return;

    messages.Add($"Updated API url to: {repository.Url}");
    projectInfo.Repo.ApiUrl = repository.Url;
  }

  private static void SyncRepoSize(ICollection<string> messages, ProjectInfo projectInfo, Repository repository)
  {
    if (projectInfo.Repo.Size == repository.Size)
      return;

    messages.Add($"Updated repo size to: {repository.Size}");
    projectInfo.Repo.Size = repository.Size;
  }

  private static void SyncDescription(ICollection<string> messages, ProjectInfo projectInfo, Repository repository)
  {
    if (projectInfo.Description == repository.Description)
      return;

    messages.Add($"Updated description to: {repository.Description}");
    projectInfo.Description = repository.Description;
  }
}
