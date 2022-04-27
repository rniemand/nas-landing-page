using Microsoft.AspNetCore.Mvc;
using NasLandingPage.Models;
using NasLandingPage.Services;

namespace NasLandingPage.Controllers;

[ApiController]
[Route("[controller]")]
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
    // http://localhost:5296/projects
    return _projectsService.GetAll();
  }
}
