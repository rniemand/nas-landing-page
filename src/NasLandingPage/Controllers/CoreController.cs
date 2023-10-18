using Microsoft.AspNetCore.Mvc;
using NasLandingPage.Extensions;
using NasLandingPage.Models.Dto;
using NasLandingPage.Models.Responses;
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

  // Floors
  [HttpGet("home/{homeId:int}/floors")]
  public async Task<HomeFloorDto[]> GetFloors([FromRoute] int homeId) =>
    (await _coreService.GetFloorsAsync(User.GetNlpUserContext(), homeId)).ToArray();

  [HttpPost("add-floor")]
  public async Task<BoolResponse> AddFloor([FromBody] HomeFloorDto floor) =>
    await _coreService.AddFloorAsync(User.GetNlpUserContext(), floor);

  [HttpPatch("update-floor")]
  public async Task<BoolResponse> UpdateFloor([FromBody] HomeFloorDto floor) =>
    await _coreService.UpdateFloorAsync(User.GetNlpUserContext(), floor);

  // Rooms
  [HttpGet("room/{roomId:int}/floor-id")]
  public async Task<int> ResolveFloorIdFromRoomId([FromRoute] int roomId) =>
    await _coreService.ResolveFloorIdFromRoomIsAsync(User.GetNlpUserContext(), roomId);

  [HttpGet("floor/{floorId:int}/rooms")]
  public async Task<HomeRoomDto[]> GetFloorRooms([FromRoute] int floorId) =>
    (await _coreService.GetFloorRoomsAsync(User.GetNlpUserContext(), floorId)).ToArray();

  [HttpPost("add-room")]
  public async Task<BoolResponse> AddRoom([FromBody] HomeRoomDto room) =>
  await _coreService.AddRoomAsync(User.GetNlpUserContext(), room);

  [HttpPatch("update-room")]
  public async Task<BoolResponse> UpdateRoom([FromBody] HomeRoomDto room) =>
  await _coreService.UpdateRoomAsync(User.GetNlpUserContext(), room);

  // Users
  [HttpGet("users/list")]
  public async Task<UserDto[]> GetAllUsers() =>
    (await _coreService.GetAllUsersAsync()).ToArray();
}
