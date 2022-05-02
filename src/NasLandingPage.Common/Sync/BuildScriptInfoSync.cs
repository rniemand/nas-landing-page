using NasLandingPage.Common.Builders;
using NasLandingPage.Common.Clients;
using NasLandingPage.Common.Models.Responses;

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


    Console.WriteLine();
  }
}
