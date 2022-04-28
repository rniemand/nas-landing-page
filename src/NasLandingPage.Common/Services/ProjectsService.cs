using Microsoft.Extensions.DependencyInjection;
using NasLandingPage.Common.Models;
using NasLandingPage.Common.Providers;

namespace NasLandingPage.Common.Services
{
  public interface IProjectsService
  {
    List<ProjectInfo> GetAll();
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
  }
}
