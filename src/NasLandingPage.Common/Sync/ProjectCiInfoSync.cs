using NasLandingPage.Common.Builders;
using NasLandingPage.Common.Clients;
using NasLandingPage.Common.Models.External;
using NasLandingPage.Common.Models.Responses.Projects;
using Rn.NetCore.Common.Helpers;

namespace NasLandingPage.Common.Sync;

public interface IProjectCiInfoSync
{
  Task SyncAsync(RunCommandResponseBuilder responseBuilder, ProjectInfo projectInfo);
}

public class ProjectCiInfoSync : IProjectCiInfoSync
{
  private readonly INlpGitHubClient _gitHubClient;
  private readonly IJsonHelper _json;

  public ProjectCiInfoSync(INlpGitHubClient gitHubClient, IJsonHelper json)
  {
    _gitHubClient = gitHubClient;
    _json = json;
  }

  public async Task SyncAsync(RunCommandResponseBuilder responseBuilder, ProjectInfo projectInfo)
  {
    // TODO: [ProjectCiInfoSync.SyncAsync] (TESTS) Add tests
    if(!projectInfo.Scm.HasCiInfo)
      return;

    var repositoryId = projectInfo.Repo.RepoId;
    var filePath = projectInfo.Scm.CiInfoPath;

    var contents = await _gitHubClient.GetAllContentsAsync(repositoryId, filePath);
    var ciFile = contents.FirstOrDefault();

    if (ciFile is null)
    {
      HandleNoFileFound(responseBuilder, projectInfo);
      return;
    }

    if (!TryExtractRepoCiInfo(ciFile.Content, out var ciInfo))
    {
      HandleUnableToParseCiInfo(responseBuilder, projectInfo);
      return;
    }

    responseBuilder.WithMessage("Updating ci.info.json");
    projectInfo.CiInfo = ciInfo;

    SyncHasBuildScripVersion(responseBuilder, projectInfo, ciInfo);
  }

  private static void HandleNoFileFound(RunCommandResponseBuilder responseBuilder, ProjectInfo projectInfo)
  {
    // TODO: [ProjectCiInfoSync.HandleNoFileFound] (TESTS) Add tests
    responseBuilder.WithMessage("No ci.info.json file found, updating project information");
    projectInfo.Scm.HasCiInfo = false;
    projectInfo.Scm.CiInfoPath = string.Empty;
  }

  private static void HandleUnableToParseCiInfo(RunCommandResponseBuilder responseBuilder, ProjectInfo projectInfo)
  {
    // TODO: [ProjectCiInfoSync.HandleUnableToParseCiInfo] (TESTS) Add tests
    responseBuilder.WithMessage("Unable to parse ci.info.json");
    projectInfo.Scm.HasCiInfo = false;
    projectInfo.Scm.CiInfoPath = string.Empty;
  }

  private static void SyncHasBuildScripVersion(RunCommandResponseBuilder responseBuilder, ProjectInfo projectInfo, RepoCiInfo ciInfo)
  {
    // TODO: [ProjectCiInfoSync.SyncHasBuildScripVersion] (TESTS) Add tests
    if (projectInfo.Scm.BuildScriptVersion == ciInfo.BuildScriptVersion)
      return;

    responseBuilder.WithMessage($"Updating build script version to: {ciInfo.BuildScriptVersion}");
    projectInfo.Scm.BuildScriptVersion = ciInfo.BuildScriptVersion;
  }

  private bool TryExtractRepoCiInfo(string rawJson, out RepoCiInfo parsed) =>
    // TODO: [ProjectCiInfoSync.TryExtractRepoCiInfo] (TESTS) Add tests
    _json.TryDeserializeObject(rawJson, out parsed);
}
