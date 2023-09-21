using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using NasLandingPage.Services;

namespace NasLandingPage.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImageController : ControllerBase
{
  private readonly IUserLinksService _userLinksService;
  private readonly FileExtensionContentTypeProvider _mimeTypeProvider = new();

  public ImageController(IUserLinksService userLinksService)
  {
    _userLinksService = userLinksService;
  }

  [HttpGet("user-link/{linkId:int}")]
  public async Task<IActionResult> GetUserLinkImage([FromRoute] int linkId)
  {
    var imagePath = await _userLinksService.GetUserLinkImagePathAsync(linkId);
    if (_mimeTypeProvider.TryGetContentType(imagePath, out var contentType))
      return PhysicalFile(imagePath, contentType);
    return PhysicalFile(imagePath, "application/octet-stream");
  }
}
