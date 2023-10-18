using NasLandingPage.Models;
using NasLandingPage.Models.Dto;
using NasLandingPage.Models.Responses;
using NasLandingPage.Repos;

namespace NasLandingPage.Services;

public interface ICoreService
{
  // Floors
  Task<IEnumerable<HomeFloorDto>> GetFloorsAsync(NlpUserContext userContext, int homeId);
  Task<BoolResponse> AddFloorAsync(NlpUserContext userContext, HomeFloorDto floor);
  Task<BoolResponse> UpdateFloorAsync(NlpUserContext userContext, HomeFloorDto floor);

  // Rooms
  Task<IEnumerable<HomeRoomDto>> GetFloorRoomsAsync(NlpUserContext userContext, int floorId);
  Task<int> ResolveFloorIdFromRoomIsAsync(NlpUserContext userContext, int roomId);
  Task<BoolResponse> AddRoomAsync(NlpUserContext userContext, HomeRoomDto room);
  Task<BoolResponse> UpdateRoomAsync(NlpUserContext userContext, HomeRoomDto room);

  // Users
  Task<IEnumerable<UserDto>> GetAllUsersAsync();
}

internal class CoreService : ICoreService
{
  private readonly IUserRepo _userRepo;
  private readonly IFloorRepo _floorRepo;
  private readonly IRoomRepo _roomRepo;

  public CoreService(IUserRepo userRepo, IFloorRepo floorRepo, IRoomRepo roomRepo)
  {
    _userRepo = userRepo;
    _floorRepo = floorRepo;
    _roomRepo = roomRepo;
  }

  // Floors
  public async Task<IEnumerable<HomeFloorDto>> GetFloorsAsync(NlpUserContext userContext, int homeId) =>
    await _floorRepo.GetFloorsAsync(homeId);

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

  // Rooms
  public async Task<IEnumerable<HomeRoomDto>> GetFloorRoomsAsync(NlpUserContext userContext, int floorId) =>
    await _roomRepo.GetFloorRoomsAsync(floorId);

  public async Task<int> ResolveFloorIdFromRoomIsAsync(NlpUserContext userContext, int roomId) =>
    await _floorRepo.ResolveFloorIdFromRoomIdAsync(roomId);

  public async Task<BoolResponse> AddRoomAsync(NlpUserContext userContext, HomeRoomDto room)
  {
    var response = new BoolResponse();
    var rowCount = await _roomRepo.AddRoomAsync(room);
    return rowCount == 1 ? response : response.AsError("Failed to add room");
  }

  public async Task<BoolResponse> UpdateRoomAsync(NlpUserContext userContext, HomeRoomDto room)
  {
    var response = new BoolResponse();
    var rowCount = await _roomRepo.UpdateRoomAsync(room);
    return rowCount == 1 ? response : response.AsError("Failed to update room");
  }

  // Users
  public async Task<IEnumerable<UserDto>> GetAllUsersAsync() =>
    (await _userRepo.GetAllUsersAsync()).Select(UserDto.FromEntity);
}
