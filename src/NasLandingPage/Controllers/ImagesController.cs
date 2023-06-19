using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using NasLandingPage.Models;
using NasLandingPage.Models.Dto;
using NasLandingPage.Services;

namespace NasLandingPage.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImagesController : ControllerBase
{
  private readonly AppConfig _config;
  private readonly FileExtensionContentTypeProvider _provider = new();
  private readonly IImageService _imageService;

  public ImagesController(AppConfig config, IImageService imageService)
  {
    _config = config;
    _imageService = imageService;
  }

  [Route("link/{path}")]
  public ActionResult GetImage([FromRoute] string path)
  {
    var localPath = Path.Combine(_config.LinkImageRootDir, path);
    if (!System.IO.File.Exists(localPath))
      return NotFound();

    return File(System.IO.File.OpenRead(localPath), GetContentType(localPath));
  }

  [Route("game/cover/{platform}/{gameId:long}")]
  public async Task<ActionResult> GetImage([FromRoute] string platform, [FromRoute] long gameId)
  {
    // TODO: (ImagesController.GetImage) [OPTIMIZE] Cache mime type of file to save lookup
    var path = await _imageService.GetGameCoverImagePathAsync(platform, gameId);
    return File(System.IO.File.OpenRead(path), GetContentType(path));
  }

  [HttpGet("game/images/{gameId:long}")]
  public async Task<List<ImageDto>> GetGameImages([FromRoute] long gameId) =>
    await _imageService.GetGameImagesAsync(gameId);

  // Internal methods
  private string GetContentType(string filePath)
  {
    var contentType = "image/png";

    if (_provider.TryGetContentType(filePath, out var resolved))
      contentType = resolved;

    return contentType;
  }
}
