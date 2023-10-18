using Microsoft.AspNetCore.Mvc;
using NasLandingPage.Extensions;
using NasLandingPage.Models.Dto;
using NasLandingPage.Models.Requests;
using NasLandingPage.Models.Responses;
using NasLandingPage.Services;

namespace NasLandingPage.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChoreController : ControllerBase
{
  private readonly IChoreService _choreService;

  public ChoreController(IChoreService choreService)
  {
    _choreService = choreService;
  }

  [HttpGet("chores")]
  public async Task<HomeChoreDto[]> GetChores() =>
    await _choreService.GetChoresAsync(User.GetNlpUserContext());

  [HttpPost("add-chore")]
  public async Task<BoolResponse> AddChore([FromBody] HomeChoreDto chore) =>
    await _choreService.AddChoreAsync(User.GetNlpUserContext(), chore);

  [HttpPatch("update-chore")]
  public async Task<BoolResponse> UpdateChore([FromBody] HomeChoreDto chore) =>
    await _choreService.UpdateChoreAsync(User.GetNlpUserContext(), chore);

  [HttpPatch("complete-chore")]
  public async Task<BoolResponse> CompleteChore([FromBody] CompleteChoreRequest request) =>
    await _choreService.CompleteChoreAsync(User.GetNlpUserContext(), request);
}
