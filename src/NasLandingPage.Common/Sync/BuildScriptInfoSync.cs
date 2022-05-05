using NasLandingPage.Common.Builders;
using NasLandingPage.Common.Clients;
using NasLandingPage.Common.Extensions;
using NasLandingPage.Common.Models.Responses.Projects;
using Octokit;

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
    if (!projectInfo.Directories.DotGithub)
      return;

    var messages = new List<string>();
    var repositoryId = projectInfo.Repo.RepoId;
    const string path = ".github";

    projectInfo.Scm.BuildScripts = new List<string>();
    projectInfo.Scm.TestScripts = new List<string>();

    var contents = await _gitHubClient.GetAllContentsAsync(repositoryId, path);

    SyncBuildScript(messages, projectInfo, contents);
    SyncTestScript(messages, projectInfo, contents);
    SyncCiInfo(messages, projectInfo, contents);

    responseBuilder.WithMessages(messages);
  }

  // Build script files
  private static void SetHasBuildScript(ICollection<string> messages, ProjectInfo projectInfo, bool hasBuildScript)
  {
    // TODO: [BuildScriptInfoSync.SetHasBuildScript] (TESTS) Add tests
    if (projectInfo.Scm.HasBuildScript == hasBuildScript)
      return;

    messages.Add("Updating HasBuildScript to " + (hasBuildScript ? "true" : "false"));
    projectInfo.Scm.HasBuildScript = hasBuildScript;
  }

  private static void SetHasTestScript(ICollection<string> messages, ProjectInfo projectInfo, bool hasTestScript)
  {
    // TODO: [BuildScriptInfoSync.SetHasTestScript] (TESTS) Add tests
    if (projectInfo.Scm.HasTestScript == hasTestScript)
      return;

    messages.Add("Updating HasTestScript to " + (hasTestScript ? "true" : "false"));
    projectInfo.Scm.HasBuildScript = hasTestScript;
  }

  private static void SetHasCiInfo(ICollection<string> messages, ProjectInfo projectInfo, bool hasCiInfo)
  {
    // TODO: [BuildScriptInfoSync.SetHasCiInfo] (TESTS) Add tests
    if (projectInfo.Scm.HasCiInfo == hasCiInfo)
      return;

    messages.Add("Updating HasCiInfo to " + (hasCiInfo ? "true" : "false"));
    projectInfo.Scm.HasCiInfo = hasCiInfo;
  }

  private static void SyncBuildScript(ICollection<string> messages, ProjectInfo projectInfo, IReadOnlyList<RepositoryContent> contents)
  {
    // TODO: [RootRepositoryContentInfoSync.SyncEditorConfig] (TESTS) Add tests
    var filePath = contents.GetHtmlFilePath("ci-build.ps1");
    var fileExists = !string.IsNullOrWhiteSpace(filePath);

    SetHasBuildScript(messages, projectInfo, fileExists);

    if (!fileExists)
      return;

    var repoFilePath = contents.GetRepoFilePath("ci-build.ps1");
    messages.Add($"Adding build script: {repoFilePath}");
    projectInfo.Scm.BuildScripts.Add(repoFilePath);
  }

  private static void SyncTestScript(ICollection<string> messages, ProjectInfo projectInfo, IReadOnlyList<RepositoryContent> contents)
  {
    // TODO: [BuildScriptInfoSync.SyncTestScript] (TESTS) Add tests
    var filePath = contents.GetHtmlFilePath("ci-test.ps1");
    var fileExists = !string.IsNullOrWhiteSpace(filePath);

    SetHasTestScript(messages, projectInfo, fileExists);

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
    var fileExists = !string.IsNullOrWhiteSpace(filePath);

    SetHasCiInfo(messages, projectInfo, fileExists);

    if (!fileExists)
      return;

    var repoFilePath = contents.GetRepoFilePath("ci.info.json");
    messages.Add($"Adding ci.info file: {repoFilePath}");
    projectInfo.Scm.CiInfoPath = repoFilePath;
  }
}
