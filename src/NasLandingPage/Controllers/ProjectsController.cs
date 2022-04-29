using Microsoft.AspNetCore.Mvc;
using NasLandingPage.Common.Models.Requests;
using NasLandingPage.Common.Models.Responses;
using NasLandingPage.Common.Services;

namespace NasLandingPage.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
  private readonly IProjectsService _projectsService;

  public ProjectsController(IProjectsService projectsService)
  {
    _projectsService = projectsService;
  }

  [HttpGet]
  public List<ProjectInfo> GetAll()
  {
    // TODO: [ProjectsController.GetAll] (TESTS) Add tests
    return _projectsService.GetAll();
  }

  [HttpPost, Route("sync")]
  public async Task<RunCommandResponse> SyncProject(RunCommandRequest request)
  {
    // TODO: [ProjectsController.SyncProject] (TESTS) Add tests
    return await _projectsService.SyncProject(request);
  }
}
