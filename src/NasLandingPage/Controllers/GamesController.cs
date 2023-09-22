using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using NasLandingPage.Models.Dto;
using NasLandingPage.Services;

namespace NasLandingPage.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
  private readonly IGamesService _gamesService;
  private readonly FileExtensionContentTypeProvider _mimeTypeProvider = new();

  public GamesController(IGamesService gamesService)
  {
    _gamesService = gamesService;
  }

  [HttpGet("platforms")]
  public async Task<GamePlatformDto[]> GetPlatforms() =>
    await _gamesService.GetPlatformsAsync();

  [HttpGet("games/platform-id/{platformId:int}")]
  public async Task<GameDto[]> GetPlatformGames([FromRoute] int platformId) =>
    await _gamesService.GetPlatformGamesAsync(platformId);

  [HttpGet("game-cover/{platform}/{gameId:int}")]
  public async Task<IActionResult> GetGameCover([FromRoute] string platform, [FromRoute] int gameId)
  {
    var imagePath = await _gamesService.GetGameCoverImagePathAsync(platform, gameId);
    if (_mimeTypeProvider.TryGetContentType(imagePath, out var contentType))
      return PhysicalFile(imagePath, contentType);
    return PhysicalFile(imagePath, "application/octet-stream");
  }
}
