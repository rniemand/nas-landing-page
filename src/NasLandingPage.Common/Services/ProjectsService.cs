using Microsoft.Extensions.DependencyInjection;
using NasLandingPage.Common.Builders;
using NasLandingPage.Common.Models.Requests;
using NasLandingPage.Common.Models.Responses;
using NasLandingPage.Common.Providers;

namespace NasLandingPage.Common.Services
{
  public interface IProjectsService
  {
    List<ProjectInfo> GetAll();
    Task<RunCommandResponse> SyncProject(RunCommandRequest request);
  }

  public class ProjectsService : IProjectsService
  {
    private readonly IProjectInfoProvider _projectInfoProvider;

    public ProjectsService(IServiceProvider serviceProvider)
    {
      // TODO: [ProjectsService.ProjectsService] (TESTS) Add tests
      _projectInfoProvider = serviceProvider.GetRequiredService<IProjectInfoProvider>();
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



      Console.WriteLine();
      return new RunCommandResponse();
    }
  }
}
