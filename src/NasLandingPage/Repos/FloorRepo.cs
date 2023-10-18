using Dapper;
using NasLandingPage.Models.Dto;

namespace NasLandingPage.Repos;

public interface IFloorRepo
{
  Task<IEnumerable<HomeFloorDto>> GetFloorsAsync(int homeId);
  Task<int> ResolveFloorIdFromRoomIdAsync(int roomId);
  Task<int> AddFloorAsync(HomeFloorDto floor);
  Task<int> UpdateFloorAsync(HomeFloorDto floor);
}

class FloorRepo : IFloorRepo
{
  private readonly IConnectionHelper _connectionHelper;

  public FloorRepo(IConnectionHelper connectionHelper)
  {
    _connectionHelper = connectionHelper;
  }

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
}
