using NasLandingPage.Common.Builders;
using NasLandingPage.Common.Clients;
using NasLandingPage.Common.Models.External;
using NasLandingPage.Common.Models.Responses.Projects;
using Rn.NetCore.Common.Extensions;
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
    if(string.IsNullOrWhiteSpace(projectInfo.Scm.CiInfo))
      return;

    var repositoryId = projectInfo.Repo.RepoId;
    var filePath = ExtractGitFilePath(projectInfo.Scm.CiInfo);

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

    SyncCiVersion(responseBuilder, projectInfo, ciInfo);
  }

  private static void HandleNoFileFound(RunCommandResponseBuilder responseBuilder, ProjectInfo projectInfo)
  {
    responseBuilder.WithMessage("No ci.info.json file found, updating project information");
    projectInfo.Scm.CiInfo = string.Empty;
  }

  private static void HandleUnableToParseCiInfo(RunCommandResponseBuilder responseBuilder, ProjectInfo projectInfo)
  {
    responseBuilder.WithMessage("Unable to parse ci.info.json");
    projectInfo.Scm.CiInfo = string.Empty;
  }

  private static void SyncCiVersion(RunCommandResponseBuilder responseBuilder, ProjectInfo projectInfo, RepoCiInfo ciInfo)
  {
    if (projectInfo.Scm.CiVersion.IgnoreCaseEquals(ciInfo.BuildScriptVersion))
      return;
    
    responseBuilder.WithMessage($"Updated 'scm.ciVersion' to: {ciInfo.BuildScriptVersion}");
    projectInfo.Scm.CiVersion = ciInfo.BuildScriptVersion;
  }

  private bool TryExtractRepoCiInfo(string rawJson, out RepoCiInfo parsed) =>
    _json.TryDeserializeObject(rawJson, out parsed);

  private static string ExtractGitFilePath(string url)
  {
    // http.*?:\/\/.*?\/blob\/([^\/]+)\/(.*)
    const string rxp = @"http.*?:\/\/.*?\/blob\/([^\/]+)\/(.*)";

    if (!url.MatchesRegex(rxp))
      throw new Exception("Unable to extract file path");

    var match = url.GetRegexMatch(rxp);
    return match.Groups[2].Value;
  }
}
