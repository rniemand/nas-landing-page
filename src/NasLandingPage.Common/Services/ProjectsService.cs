using Microsoft.Extensions.DependencyInjection;
using NasLandingPage.Common.Models.Responses;
using NasLandingPage.Common.Providers;

namespace NasLandingPage.Common.Services
{
  public interface IProjectsService
  {
    List<ProjectInfo> GetAll();
    Task<CommandResponse> SyncProject(ProjectInfo project);
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

    public async Task<CommandResponse> SyncProject(ProjectInfo project)
    {
      // TODO: [ProjectsService.SyncProject] (TESTS) Add tests

      return new CommandResponse();
    }
  }
}
