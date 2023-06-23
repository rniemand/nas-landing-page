using Microsoft.AspNetCore.Mvc;
using NasLandingPage.Models.Dto;
using NasLandingPage.Services;

namespace NasLandingPage.Controllers;

[ApiController]
[Route("[controller]")]
public class GamePlatformsController : ControllerBase
{
  private readonly IGamePlatformService _gamePlatformService;

  public GamePlatformsController(IGamePlatformService gamePlatformService)
  {
    _gamePlatformService = gamePlatformService;
  }

  [Route("list")]
  public async Task<List<PlatformDto>> GetAll() =>
    await _gamePlatformService.GetAllAsync();
}
