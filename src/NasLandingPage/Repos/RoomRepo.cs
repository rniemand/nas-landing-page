using Dapper;
using NasLandingPage.Models.Dto;

namespace NasLandingPage.Repos;

public interface IRoomRepo
{
  Task<IEnumerable<HomeRoomDto>> GetFloorRoomsAsync(int floorId);
  Task<int> AddRoomAsync(HomeRoomDto room);
  Task<int> UpdateRoomAsync(HomeRoomDto room);
}

class RoomRepo : IRoomRepo
{
  private readonly IConnectionHelper _connectionHelper;

  public RoomRepo(IConnectionHelper connectionHelper)
  {
    _connectionHelper = connectionHelper;
  }

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
