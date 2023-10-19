using NasLandingPage.Models.Dto;
using NasLandingPage.Models.Responses;
using NasLandingPage.Models;
using NasLandingPage.Repos;

namespace NasLandingPage.Services;

public interface IRoomService
{
  Task<IEnumerable<HomeRoomDto>> GetFloorRoomsAsync(NlpUserContext userContext, int floorId);
  Task<BoolResponse> AddRoomAsync(NlpUserContext userContext, HomeRoomDto room);
  Task<BoolResponse> UpdateRoomAsync(NlpUserContext userContext, HomeRoomDto room);
}

class RoomService : IRoomService
{
  private readonly IRoomRepo _roomRepo;

  public RoomService(IRoomRepo roomRepo)
  {
    _roomRepo = roomRepo;
  }

  public async Task<IEnumerable<HomeRoomDto>> GetFloorRoomsAsync(NlpUserContext userContext, int floorId) =>
    await _roomRepo.GetFloorRoomsAsync(floorId);

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
}
