using Microsoft.AspNetCore.Mvc;
using NasLandingPage.Common.Models.Responses;
using NasLandingPage.Common.Services;

namespace NasLandingPage.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserLinksController : ControllerBase
{
  private readonly IUserLinkService _linkService;

  public UserLinksController(IUserLinkService linkService)
  {
    _linkService = linkService;
  }

  [HttpGet]
  public async Task<List<UserLink>> GetAll()
  {
    return await _linkService.GetAll();
  }
}
