using NasLandingPage.Common.Models.Responses;
using Octokit;

namespace NasLandingPage.Common.Sync;

public static class CoreRepositoryInfoSync
{
  public static List<string> Sync(ProjectInfo projectInfo, Repository repository)
  {
    // TODO: [CoreRepositoryInfoSync.Sync] (TESTS) Add tests
    var messages = new List<string>();

    projectInfo.Repo.LastUpdated = repository.UpdatedAt;
    projectInfo.Languages = new[] { repository.Language };

    if (projectInfo.Repo.DefaultBranch != repository.DefaultBranch)
    {
      messages.Add($"Set default branch to: {repository.DefaultBranch}");
      projectInfo.Repo.DefaultBranch = repository.DefaultBranch;
    }

    var repoIsPublic = repository.Visibility == RepositoryVisibility.Public;
    if (projectInfo.Repo.IsPublic != repoIsPublic)
    {
      messages.Add("Set visibility to: " + (repoIsPublic ? "public" : "private"));
      projectInfo.Repo.IsPublic = repoIsPublic;
    }

    if (projectInfo.Repo.ForksCount != repository.ForksCount)
    {
      messages.Add($"Updated forked count to: {repository.ForksCount}");
      projectInfo.Repo.ForksCount = repository.ForksCount;
    }

    if (projectInfo.Repo.HtmlUrl != repository.HtmlUrl)
    {
      messages.Add($"Set Html URL to: {repository.HtmlUrl}");
      projectInfo.Repo.HtmlUrl = repository.HtmlUrl;
    }

    if (projectInfo.Repo.FullName != repository.FullName)
    {
      messages.Add($"Set full name to: {repository.FullName}");
      projectInfo.Repo.FullName = repository.FullName;
    }

    if (projectInfo.Repo.GitUrl != repository.GitUrl)
    {
      messages.Add($"Updated git URL to: {repository.GitUrl}");
      projectInfo.Repo.GitUrl = repository.GitUrl;
    }

    if (projectInfo.Repo.OpenIssuesCount != repository.OpenIssuesCount)
    {
      messages.Add($"Updated open issue count to: {repository.OpenIssuesCount}");
      projectInfo.Repo.OpenIssuesCount = repository.OpenIssuesCount;
    }

    if (projectInfo.Repo.SshUrl != repository.SshUrl)
    {
      messages.Add($"Updated SSH URL to: {repository.SshUrl}");
      projectInfo.Repo.SshUrl = repository.SshUrl;
    }

    if (projectInfo.Repo.ApiUrl != repository.Url)
    {
      messages.Add($"Updated API url to: {repository.Url}");
      projectInfo.Repo.ApiUrl = repository.Url;
    }

    if (projectInfo.Repo.Size != repository.Size)
    {
      messages.Add($"Updated repo size to: {repository.Size}");
      projectInfo.Repo.Size = repository.Size;
    }

    if (projectInfo.Description != repository.Description)
    {
      messages.Add($"Updated description to: {repository.Description}");
      projectInfo.Description = repository.Description;
    }

    return messages;
  }
}
