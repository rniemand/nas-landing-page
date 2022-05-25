using Microsoft.AspNetCore.Mvc;
using NasLandingPage.Common.Models.Requests;
using NasLandingPage.Common.Models.Responses;
using NasLandingPage.Common.Models.Responses.Projects;
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
  public List<ProjectInfo> GetAll() =>
    _projectsService.GetAll();

  [HttpPost, Route("sync")]
  public async Task<RunCommandResponse> SyncProject(RunCommandRequest request) =>
    await _projectsService.SyncProject(request);
}
