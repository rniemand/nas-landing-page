using Microsoft.AspNetCore.Mvc;
using NasLandingPage.Extensions;
using NasLandingPage.Models.Dto;
using NasLandingPage.Services;

namespace NasLandingPage.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserLinksController : ControllerBase
{
  private readonly IUserLinksService _userLinksService;

  public UserLinksController(IUserLinksService userLinksService)
  {
    _userLinksService = userLinksService;
  }

  [HttpGet]
  public async Task<UserLinkDto[]> GetUserLinks() =>
    await _userLinksService.GetUserLinksAsync(User.GetNlpUserContext());

  [HttpGet("follow/{linkId:int}")]
  public async Task FollowLink([FromRoute] int linkId) =>
    await _userLinksService.IncrementLinkFollowCountAsync(User.GetNlpUserContext(), linkId);
}
