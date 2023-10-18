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

  [HttpGet("home/{homeId:int}/floors")]
  public async Task<HomeFloorDto[]> GetFloors([FromRoute] int homeId) =>
    (await _floorService.GetFloorsAsync(User.GetNlpUserContext(), homeId)).ToArray();

  [HttpPost("add-floor")]
  public async Task<BoolResponse> AddFloor([FromBody] HomeFloorDto floor) =>
    await _floorService.AddFloorAsync(User.GetNlpUserContext(), floor);

  [HttpPatch("update-floor")]
  public async Task<BoolResponse> UpdateFloor([FromBody] HomeFloorDto floor) =>
    await _floorService.UpdateFloorAsync(User.GetNlpUserContext(), floor);

  [HttpGet("room/{roomId:int}/floor-id")]
  public async Task<int> ResolveFloorIdFromRoomId([FromRoute] int roomId) =>
    await _floorService.ResolveFloorIdFromRoomIsAsync(User.GetNlpUserContext(), roomId);
}
