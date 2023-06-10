using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using NasLandingPage.Models;

namespace NasLandingPage.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImagesController : ControllerBase
{
  private readonly AppConfig _config;
  private readonly FileExtensionContentTypeProvider _provider = new();

  public ImagesController(AppConfig config)
  {
    _config = config;
  }

  [Route("link/{path}")]
  public ActionResult GetImage([FromRoute] string path)
  {
    var localPath = Path.Combine(_config.LinkImageRootDir, path);
    if (!System.IO.File.Exists(localPath))
      return NotFound();

    return File(System.IO.File.OpenRead(localPath), GetContentType(localPath));
  }

  private string GetContentType(string filePath)
  {
    var contentType = "image/png";

    if (_provider.TryGetContentType(filePath, out var resolved))
      contentType = resolved;

    return contentType;
  }
}
