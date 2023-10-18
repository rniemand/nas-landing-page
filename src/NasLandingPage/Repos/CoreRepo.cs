using Dapper;
using NasLandingPage.Models;
using NasLandingPage.Models.Dto;

namespace NasLandingPage.Repos;

public interface ICoreRepo
{
  // Homes
  Task<IEnumerable<HomeDto>> GetHomesAsync(NlpUserContext userContext);

  // Floors
  Task<IEnumerable<HomeFloorDto>> GetFloorsAsync(int homeId);
  Task<int> ResolveFloorIdFromRoomIdAsync(int roomId);
  Task<int> AddFloorAsync(HomeFloorDto floor);
  Task<int> UpdateFloorAsync(HomeFloorDto floor);

  // Rooms
  Task<IEnumerable<HomeRoomDto>> GetFloorRoomsAsync(int floorId);
  Task<int> AddRoomAsync(HomeRoomDto room);
  Task<int> UpdateRoomAsync(HomeRoomDto room);
}

internal class CoreRepo : ICoreRepo
{
  private readonly IConnectionHelper _connectionHelper;

  public CoreRepo(IConnectionHelper connectionHelper)
  {
    _connectionHelper = connectionHelper;
  }


  // Homes
  public async Task<IEnumerable<HomeDto>> GetHomesAsync(NlpUserContext userContext)
  {
    await Task.CompletedTask;
    return new List<HomeDto>();
  }

  // Floors
  public async Task<IEnumerable<HomeFloorDto>> GetFloorsAsync(int homeId)
  {
    const string query = @"
    SELECT *
    FROM `HomeFloors` hf
    WHERE
      hf.`HomeId` = @HomeId
      AND hf.`DateDeleted` IS NULL
    ORDER BY hf.`FloorName`";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QueryAsync<HomeFloorDto>(query, new
    {
      HomeId = homeId,
    });
  }

  public async Task<int> ResolveFloorIdFromRoomIdAsync(int roomId)
  {
    const string query = @"
    SELECT hr.`FloorId`
    FROM `HomeRooms` hr
    WHERE hr.`RoomId` = @RoomId";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QuerySingleOrDefaultAsync<int>(query, new
    {
      RoomId = roomId
    });
  }

  public async Task<int> AddFloorAsync(HomeFloorDto floor)
  {
    const string query = @"
    INSERT INTO `HomeFloors`
	    (`HomeId`, `FloorName`)
    VALUES
	    (@HomeId, @FloorName)";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, floor);
  }

  public async Task<int> UpdateFloorAsync(HomeFloorDto floor)
  {
    const string query = @"
    UPDATE `HomeFloors`
    SET
	    `FloorName` = @FloorName
    WHERE
	    `FloorId` = @FloorId";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, floor);
  }

  // Rooms
  public async Task<IEnumerable<HomeRoomDto>> GetFloorRoomsAsync(int floorId)
  {
    const string query = @"
    SELECT *
    FROM `HomeRooms` hr
    WHERE
      hr.`DateDeleted` IS NULL
      AND hr.`FloorId` = @FloorId
    ORDER BY hr.`RoomName`";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QueryAsync<HomeRoomDto>(query, new
    {
      FloorId = floorId,
    });
  }

  public async Task<int> AddRoomAsync(HomeRoomDto room)
  {
    const string query = @"
    INSERT INTO `HomeRooms`
	    (`FloorId`,`RoomName`)
    VALUES
	    (@FloorId,@RoomName)";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, room);
  }

  public async Task<int> UpdateRoomAsync(HomeRoomDto room)
  {
    const string query = @"
    UPDATE `HomeRooms`
    SET
	    `RoomName` = @RoomName
    WHERE
	    `RoomId` = @RoomId";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, room);
  }
}
