using Microsoft.AspNetCore.Mvc;
using NasLandingPage.Models.Dto;
using NasLandingPage.Services;

namespace NasLandingPage.Controllers;

[ApiController]
[Route("[controller]")]
public class PlatformsController : ControllerBase
{
  private readonly IPlatformService _platformService;

  public PlatformsController(IPlatformService platformService)
  {
    _platformService = platformService;
  }

  [Route("list")]
  public async Task<List<PlatformDto>> GetAll() =>
    await _platformService.GetAllAsync();
}
