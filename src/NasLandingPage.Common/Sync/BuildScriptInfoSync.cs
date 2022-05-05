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
    if (string.IsNullOrWhiteSpace(projectInfo.Directories.Build))
      return;

    var messages = new List<string>();
    var repositoryId = projectInfo.Repo.RepoId;
    const string path = ".github";

    projectInfo.Scm.TestScripts = new List<string>();

    var contents = await _gitHubClient.GetAllContentsAsync(repositoryId, path);

    SyncBuildScript(messages, projectInfo, contents);
    SyncTestScript(messages, projectInfo, contents);
    SyncCiInfo(messages, projectInfo, contents);

    responseBuilder.WithMessages(messages);
  }


  // Build script files
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
    var fileExists = !string.IsNullOrWhiteSpace(filePath);

    //SetHasTestScript(messages, projectInfo, fileExists);

    if (!fileExists)
      return;

    var repoFilePath = contents.GetRepoFilePath("ci-test.ps1");
    messages.Add($"Adding test script: {repoFilePath}");
    projectInfo.Scm.TestScripts.Add(repoFilePath);
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
