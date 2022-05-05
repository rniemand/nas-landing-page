using NasLandingPage.Common.Builders;
using NasLandingPage.Common.Clients;
using NasLandingPage.Common.Extensions;
using NasLandingPage.Common.Models.Responses.Projects;
using Octokit;
using Rn.NetCore.Common.Extensions;

namespace NasLandingPage.Common.Sync;

public interface IBuildScriptInfoSync
{
  Task SyncAsync(RunCommandResponseBuilder responseBuilder, ProjectInfo projectInfo);
}

public class BuildScriptInfoSync : IBuildScriptInfoSync
{
  private readonly INlpGitHubClient _gitHubClient;

  public BuildScriptInfoSync(INlpGitHubClient gitHubClient)
  {
    _gitHubClient = gitHubClient;
  }


  public async Task SyncAsync(RunCommandResponseBuilder responseBuilder, ProjectInfo projectInfo)
  {
    // TODO: [BuildScriptInfoSync.SyncAsync] (TESTS) Add tests
    if (string.IsNullOrWhiteSpace(projectInfo.Scm.BuildDirectory))
      return;

    var messages = new List<string>();
    var repositoryId = projectInfo.Repo.RepoId;
    const string path = ".github";
    
    var contents = await _gitHubClient.GetAllContentsAsync(repositoryId, path);

    SyncPrTemplate(messages, projectInfo, contents);
    SyncBuildScript(messages, projectInfo, contents);
    SyncTestScript(messages, projectInfo, contents);
    SyncCiInfo(messages, projectInfo, contents);

    responseBuilder.WithMessages(messages);
  }


  // Build script files
  private static void SyncPrTemplate(ICollection<string> messages, ProjectInfo projectInfo, IReadOnlyList<RepositoryContent> contents)
  {
    // TODO: [BuildScriptInfoSync.SyncPrTemplate] (TESTS) Add tests
    var filePath = contents.GetHtmlFilePath("pull_request_template.md");
    if (projectInfo.Scm.PrTemplate.IgnoreCaseEquals(filePath))
      return;

    projectInfo.Scm.PrTemplate = filePath;
    messages.Add($"Updated 'scm.prTemplate' to: {filePath}");
  }

  private static void SyncBuildScript(ICollection<string> messages, ProjectInfo projectInfo, IReadOnlyList<RepositoryContent> contents)
  {
    // TODO: [RootRepositoryContentInfoSync.SyncEditorConfig] (TESTS) Add tests
    var filePath = contents.GetHtmlFilePath("ci-build.ps1");
    if (projectInfo.Scm.BuildScript.IgnoreCaseEquals(filePath))
      return;

    projectInfo.Scm.BuildScript = filePath;
    messages.Add($"Updated 'scm.buildScript' to: {filePath}");
  }

  private static void SyncTestScript(ICollection<string> messages, ProjectInfo projectInfo, IReadOnlyList<RepositoryContent> contents)
  {
    // TODO: [BuildScriptInfoSync.SyncTestScript] (TESTS) Add tests
    var filePath = contents.GetHtmlFilePath("ci-test.ps1");
    if (projectInfo.Scm.TestScript.IgnoreCaseEquals(filePath))
      return;

    projectInfo.Scm.TestScript = filePath;
    messages.Add($"Updated 'scm.testScript' to: {filePath}");
  }

  private static void SyncCiInfo(ICollection<string> messages, ProjectInfo projectInfo, IReadOnlyList<RepositoryContent> contents)
  {
    // TODO: [BuildScriptInfoSync.SyncCiInfo] (TESTS) Add tests
    var filePath = contents.GetHtmlFilePath("ci.info.json");
    if (projectInfo.Scm.CiInfo.IgnoreCaseEquals(filePath))
      return;

    projectInfo.Scm.CiInfo = filePath;
    messages.Add($"Updated 'scm.ciInfo' to: {filePath}");
  }
}
