using Microsoft.AspNetCore.Mvc;
using NasLandingPage.Extensions;
using NasLandingPage.Models.Responses;
using NasLandingPage.Plugins.Chores;

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

  [HttpPost("add-chore")]
  public async Task<BoolResponse> AddChore([FromBody] HomeChoreDto chore) =>
    await _choreService.AddChoreAsync(User.GetNlpUserContext(), chore);

  [HttpPatch("update-chore")]
  public async Task<BoolResponse> UpdateChore([FromBody] HomeChoreDto chore) =>
    await _choreService.UpdateChoreAsync(User.GetNlpUserContext(), chore);

  [HttpPatch("reschedule-chore")]
  public async Task<BoolResponse> RescheduleChore([FromBody] HomeChoreDto chore) =>
    await _choreService.RescheduleChoreAsync(User.GetNlpUserContext(), chore);
}
