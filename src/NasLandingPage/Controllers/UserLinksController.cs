using Microsoft.AspNetCore.Mvc;
using NasLandingPage.Models.Dto;
using NasLandingPage.Services;

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

  [HttpGet("all")]
  public async Task<List<UserLinkDto>> GetAllLinks() => await _linkService.GetAllLinksAsync();

  [HttpPut("follow/{linkId:int}")]
  public async Task<bool> RecordLinkFollow([FromRoute] int linkId) => await _linkService.RecordLinkFollow(linkId);
}
