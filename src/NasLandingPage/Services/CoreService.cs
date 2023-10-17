using NasLandingPage.Models;
using NasLandingPage.Models.Dto;
using NasLandingPage.Repos;

namespace NasLandingPage.Services;

public interface ICoreService
{
  // Floors
  Task<IEnumerable<HomeFloorDto>> GetFloorsAsync(NlpUserContext userContext, int homeId);

  // Rooms
  Task<IEnumerable<HomeRoomDto>> GetFloorRoomsAsync(NlpUserContext userContext, int floorId);
  Task<int> ResolveFloorIdFromRoomIsAsync(NlpUserContext userContext, int roomId);
}

internal class CoreService : ICoreService
{
  private readonly ICoreRepo _coreRepo;

  public CoreService(ICoreRepo coreRepo)
  {
    _coreRepo = coreRepo;
  }

  // Floors
  public async Task<IEnumerable<HomeFloorDto>> GetFloorsAsync(NlpUserContext userContext, int homeId) =>
    await _coreRepo.GetFloorsAsync(homeId);

  // Rooms
  public async Task<IEnumerable<HomeRoomDto>> GetFloorRoomsAsync(NlpUserContext userContext, int floorId) =>
    await _coreRepo.GetFloorRoomsAsync(floorId);

  public async Task<int> ResolveFloorIdFromRoomIsAsync(NlpUserContext userContext, int roomId) =>
    await _coreRepo.ResolveFloorIdFromRoomIdAsync(roomId);
}
