using Microsoft.AspNetCore.Mvc;
using NasLandingPage.Models;
using NasLandingPage.Services;

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
    return _projectsService.GetAll();
  }
}
