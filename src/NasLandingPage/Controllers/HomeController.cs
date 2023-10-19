using Microsoft.AspNetCore.Mvc;
using NasLandingPage.Extensions;
using NasLandingPage.Models.Dto;
using NasLandingPage.Models.Responses;
using NasLandingPage.Services;

namespace NasLandingPage.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase
{
  private readonly IHomeService _homeService;

  public HomeController(IHomeService homeService)
  {
    _homeService = homeService;
  }

  [HttpGet("list")]
  public async Task<HomeDto[]> ListHomes() =>
    await _homeService.ListHomesAsync(User.GetNlpUserContext());

  [HttpPost("add-home")]
  public async Task<BoolResponse> AddHome([FromBody] HomeDto home) =>
    await _homeService.AddHomeAsync(User.GetNlpUserContext(), home);

  [HttpPatch("update-home")]
  public async Task<BoolResponse> UpdateHome([FromBody] HomeDto home) =>
    await _homeService.UpdateHomeAsync(User.GetNlpUserContext(), home);
}
