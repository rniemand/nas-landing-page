using Microsoft.AspNetCore.Mvc;
using NasLandingPage.Extensions;
using NasLandingPage.Models.Dto;
using NasLandingPage.Services;

namespace NasLandingPage.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoreController : ControllerBase
{
  private readonly ICoreService _coreService;

  public CoreController(ICoreService coreService)
  {
    _coreService = coreService;
  }

  [HttpGet("home/{homeId:int}/floors")]
  public async Task<HomeFloorDto[]> GetFloors([FromRoute] int homeId) =>
    (await _coreService.GetFloorsAsync(User.GetNlpUserContext(), homeId)).ToArray();

  [HttpGet("home/{homeId:int}/floor/{floorId:int}/rooms")]
  public async Task<HomeRoomDto[]> GetFloorRooms([FromRoute] int floorId) =>
    (await _coreService.GetFloorRoomsAsync(User.GetNlpUserContext(), floorId)).ToArray();
}
