using Microsoft.Extensions.DependencyInjection;
using NasLandingPage.Common.Builders;
using NasLandingPage.Common.Clients;
using NasLandingPage.Common.Factories;
using NasLandingPage.Common.Models.Requests;
using NasLandingPage.Common.Models.Responses;
using NasLandingPage.Common.Providers;

namespace NasLandingPage.Common.Services;

public interface IProjectsService
{
  List<ProjectInfo> GetAll();
  Task<RunCommandResponse> SyncProject(RunCommandRequest request);
}

public class ProjectsService : IProjectsService
{
  private readonly IProjectInfoProvider _projectInfoProvider;
  private readonly IProjectInfoSyncFactory _syncFactory;
  private readonly INlpGitHubClient _gitHubClient;

  public ProjectsService(IServiceProvider serviceProvider)
  {
    // TODO: [ProjectsService.ProjectsService] (TESTS) Add tests
    _projectInfoProvider = serviceProvider.GetRequiredService<IProjectInfoProvider>();
    _syncFactory = serviceProvider.GetRequiredService<IProjectInfoSyncFactory>();
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
    
    // Sync core repo information
    await _syncFactory.CreateCoreRepositoryInfoSync().SyncAsync(responseBuilder, projectInfo);
    await _syncFactory.CreateRootRepositoryContentInfoSync().SyncAsync(responseBuilder, projectInfo);

    // Save and return
    _projectInfoProvider.UpdateProjectInfo(projectInfo);
    return responseBuilder.Success();
  }
}
