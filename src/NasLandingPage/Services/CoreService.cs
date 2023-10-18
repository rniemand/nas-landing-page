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

  // Users
  Task<IEnumerable<UserDto>> GetAllUsersAsync();
}

internal class CoreService : ICoreService
{
  private readonly ICoreRepo _coreRepo;
  private readonly IUserRepo _userRepo;

  public CoreService(ICoreRepo coreRepo, IUserRepo userRepo)
  {
    _coreRepo = coreRepo;
    _userRepo = userRepo;
  }

  // Floors
  public async Task<IEnumerable<HomeFloorDto>> GetFloorsAsync(NlpUserContext userContext, int homeId) =>
    await _coreRepo.GetFloorsAsync(homeId);

  // Rooms
  public async Task<IEnumerable<HomeRoomDto>> GetFloorRoomsAsync(NlpUserContext userContext, int floorId) =>
    await _coreRepo.GetFloorRoomsAsync(floorId);

  public async Task<int> ResolveFloorIdFromRoomIsAsync(NlpUserContext userContext, int roomId) =>
    await _coreRepo.ResolveFloorIdFromRoomIdAsync(roomId);

  // Users
  public async Task<IEnumerable<UserDto>> GetAllUsersAsync() =>
    (await _userRepo.GetAllUsersAsync()).Select(UserDto.FromEntity);
}
