using Microsoft.Extensions.DependencyInjection;
using NasLandingPage.Common.Builders;
using NasLandingPage.Common.Clients;
using NasLandingPage.Common.Extensions;
using NasLandingPage.Common.Models.Requests;
using NasLandingPage.Common.Models.Responses;
using NasLandingPage.Common.Providers;
using Octokit;

namespace NasLandingPage.Common.Services;

public interface IProjectsService
{
  List<ProjectInfo> GetAll();
  Task<RunCommandResponse> SyncProject(RunCommandRequest request);
}

public class ProjectsService : IProjectsService
{
  private readonly IProjectInfoProvider _projectInfoProvider;
  private readonly INlpGitHubClient _gitHubClient;

  public ProjectsService(IServiceProvider serviceProvider)
  {
    // TODO: [ProjectsService.ProjectsService] (TESTS) Add tests
    _projectInfoProvider = serviceProvider.GetRequiredService<IProjectInfoProvider>();
    _gitHubClient = serviceProvider.GetRequiredService<INlpGitHubClient>();
  }


  public List<ProjectInfo> GetAll()
  {
    // TODO: [ProjectsService.GetAll] (TESTS) Add tests
    return _projectInfoProvider.GetAll();
  }

  public async Task<RunCommandResponse> SyncProject(RunCommandRequest request)
  {
    // TODO: [ProjectsService.SyncProject] (TESTS) Add tests
    var responseBuilder = new RunCommandResponseBuilder(request);

    var projectInfo = _projectInfoProvider.GetByName(request.Arguments);
    if (projectInfo is null)
      return responseBuilder.Failed("Project not found");

    var repositoryId = projectInfo.Repo.RepoId;

    //// Sync core repo information
    //var repository = await _gitHubClient.GetRepositoryAsync(repositoryId);
    //responseBuilder.WithMessages(SyncCoreInformation(projectInfo, repository));

    //// Sync directory contents
    //var directoryContents = await _gitHubClient.GetAllContentsAsync(repositoryId);
    //SyncScmCoreFileInfo(projectInfo, directoryContents);

    //_projectInfoProvider.UpdateProjectInfo(projectInfo);

    return responseBuilder.Success();
  }


  private static List<string> SyncCoreInformation(ProjectInfo projectInfo, Repository repository)
  {
    // TODO: [ProjectsService.SyncCoreInformation] (TESTS) Add tests
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

  private static List<string> SyncScmCoreFileInfo(ProjectInfo projectInfo, IReadOnlyList<RepositoryContent> rootContent)
  {
    // TODO: [ProjectsService.SyncScmCoreFileInfo] (TESTS) Add tests
    var editorConfigPath = rootContent.GetFilePath(".editorconfig");
    var hasEditorConfig = false;

      


    return new List<string>();
  }
}
