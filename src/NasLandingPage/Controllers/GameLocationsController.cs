using Microsoft.AspNetCore.Mvc;
using NasLandingPage.Models.Dto;
using NasLandingPage.Services;

namespace NasLandingPage.Controllers;

[ApiController]
[Route("[controller]")]
public class GameLocationsController : ControllerBase
{
  private readonly IGameLocationService _gameLocationService;

  public GameLocationsController(IGameLocationService gameLocationService)
  {
    _gameLocationService = gameLocationService;
  }

  [HttpGet("list/platform-id/{platformId:int}")]
  public async Task<List<LocationDto>> GetPlatformLocations([FromRoute] int platformId) =>
    await _gameLocationService.GetLocationsAsync(platformId);

  [HttpPut("set-location/game-id/{gameId:long}/location-id/{locationId:int}")]
  public async Task<int> SetGameLocation([FromRoute] long gameId, [FromRoute] int locationId) =>
    await _gameLocationService.SetGameLocationAsync(gameId, locationId);

  [HttpPost("add")]
  public async Task<LocationDto> AddLocation([FromBody] LocationDto location) =>
    await _gameLocationService.AddLocationAsync(location);
}
