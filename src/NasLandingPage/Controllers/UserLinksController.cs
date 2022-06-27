using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using NasLandingPage.Common.Models.Responses;
using NasLandingPage.Common.Providers;
using NasLandingPage.Common.Services;

namespace NasLandingPage.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserLinksController : ControllerBase
{
  private readonly IUserLinkService _linkService;
  private readonly ILinkImageProvider _imageProvider;

  public UserLinksController(IUserLinkService linkService,
    ILinkImageProvider imageProvider)
  {
    _linkService = linkService;
    _imageProvider = imageProvider;
  }

  [HttpGet]
  public async Task<List<UserLink>> GetAll()
  {
    return await _linkService.GetAll();
  }

  [HttpPut, Route("followed/{linkId:int}")]
  public async Task RegisterLinkFollow([FromRoute] int linkId)
  {
    await _linkService.RegisterFollow(linkId);
  }

  [HttpGet, Route("categories")]
  public async Task<List<string>> GetCategories()
  {
    return await _linkService.GetCategories();
  }

  [HttpGet, Route("image/{image}")]
  public async Task<IActionResult> GetImage([FromRoute] string image)
  {
    var resolveImagePath = _imageProvider.ResolveImagePath(image);
    var fileStream = System.IO.File.OpenRead(resolveImagePath);

    var provider = new FileExtensionContentTypeProvider();
    var contentType = "image/png";

    if (provider.TryGetContentType(resolveImagePath, out var resolved))
    {
      contentType = resolved;
    }

    return File(fileStream, contentType);
  }
}
