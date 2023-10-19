using Microsoft.AspNetCore.Mvc;
using NasLandingPage.Extensions;
using NasLandingPage.Models.Dto;
using NasLandingPage.Models.Responses;
using NasLandingPage.Services;

namespace NasLandingPage.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FloorController : ControllerBase
{
  private readonly IFloorService _floorService;

  public FloorController(IFloorService floorService)
  {
    _floorService = floorService;
  }

  [HttpGet("list")]
  public async Task<HomeFloorDto[]> ListFloors() => await _floorService.GetFloorsAsync(User.GetNlpUserContext());

  [HttpPost("add")]
  public async Task<BoolResponse> AddFloor([FromBody] HomeFloorDto floor) =>
    await _floorService.AddFloorAsync(User.GetNlpUserContext(), floor);

  [HttpPatch("update")]
  public async Task<BoolResponse> UpdateFloor([FromBody] HomeFloorDto floor) =>
    await _floorService.UpdateFloorAsync(User.GetNlpUserContext(), floor);

  [HttpGet("resolve-from-room-id/{roomId:int}")]
  public async Task<int> ResolveFromRoomId([FromRoute] int roomId) =>
    await _floorService.ResolveFloorIdFromRoomIsAsync(User.GetNlpUserContext(), roomId);
}
