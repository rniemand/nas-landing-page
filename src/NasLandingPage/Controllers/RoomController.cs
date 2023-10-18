using Microsoft.AspNetCore.Mvc;
using NasLandingPage.Extensions;
using NasLandingPage.Models.Dto;
using NasLandingPage.Models.Responses;
using NasLandingPage.Services;

namespace NasLandingPage.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
  private readonly IRoomService _roomService;

  public RoomController(IRoomService roomService)
  {
    _roomService = roomService;
  }

  [HttpGet("floor/{floorId:int}/rooms")]
  public async Task<HomeRoomDto[]> GetFloorRooms([FromRoute] int floorId) =>
    (await _roomService.GetFloorRoomsAsync(User.GetNlpUserContext(), floorId)).ToArray();

  [HttpPost("add-room")]
  public async Task<BoolResponse> AddRoom([FromBody] HomeRoomDto room) =>
    await _roomService.AddRoomAsync(User.GetNlpUserContext(), room);

  [HttpPatch("update-room")]
  public async Task<BoolResponse> UpdateRoom([FromBody] HomeRoomDto room) =>
    await _roomService.UpdateRoomAsync(User.GetNlpUserContext(), room);
}
