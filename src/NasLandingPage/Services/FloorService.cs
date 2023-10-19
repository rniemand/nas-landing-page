using NasLandingPage.Models.Dto;
using NasLandingPage.Models.Responses;
using NasLandingPage.Models;
using NasLandingPage.Repos;

namespace NasLandingPage.Services;

public interface IFloorService
{
  Task<HomeFloorDto[]> GetFloorsAsync(NlpUserContext userContext);
  Task<BoolResponse> AddFloorAsync(NlpUserContext userContext, HomeFloorDto floor);
  Task<BoolResponse> UpdateFloorAsync(NlpUserContext userContext, HomeFloorDto floor);
  Task<int> ResolveFloorIdFromRoomIsAsync(NlpUserContext userContext, int roomId);
}

class FloorService : IFloorService
{
  private readonly IFloorRepo _floorRepo;

  public FloorService(IFloorRepo floorRepo)
  {
    _floorRepo = floorRepo;
  }

  public async Task<HomeFloorDto[]> GetFloorsAsync(NlpUserContext userContext) =>
    (await _floorRepo.GetFloorsAsync(userContext)).ToArray();

  public async Task<BoolResponse> AddFloorAsync(NlpUserContext userContext, HomeFloorDto floor)
  {
    var response = new BoolResponse();
    var rowCount = await _floorRepo.AddFloorAsync(floor);
    return rowCount == 1 ? response : response.AsError("Failed to add floor");
  }

  public async Task<BoolResponse> UpdateFloorAsync(NlpUserContext userContext, HomeFloorDto floor)
  {
    var response = new BoolResponse();
    var rowCount = await _floorRepo.UpdateFloorAsync(floor);
    return rowCount == 1 ? response : response.AsError("Failed to update floor");
  }

  public async Task<int> ResolveFloorIdFromRoomIsAsync(NlpUserContext userContext, int roomId) =>
    await _floorRepo.ResolveFloorIdFromRoomIdAsync(roomId);
}
