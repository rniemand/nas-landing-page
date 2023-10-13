using Dapper;
using NasLandingPage.Models.Dto;

namespace NasLandingPage.Repos;

public interface ICoreRepo
{
  // Floors
  Task<IEnumerable<HomeFloorDto>> GetFloorsAsync(int homeId);

  // Rooms
  Task<IEnumerable<HomeRoomDto>> GetFloorRoomsAsync(int floorId);
}

internal class CoreRepo : ICoreRepo
{
  private readonly IConnectionHelper _connectionHelper;

  public CoreRepo(IConnectionHelper connectionHelper)
  {
    _connectionHelper = connectionHelper;
  }

  // Floors
  public async Task<IEnumerable<HomeFloorDto>> GetFloorsAsync(int homeId)
  {
    const string query = @"
    SELECT *
    FROM `HomeFloors` hf
    WHERE
      hf.`HomeId` = @HomeId
      AND hf.`DateDeletedUtc` IS NULL
    ORDER BY hf.`FloorName`";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QueryAsync<HomeFloorDto>(query, new
    {
      HomeId = homeId,
    });
  }


  // Rooms
  public async Task<IEnumerable<HomeRoomDto>> GetFloorRoomsAsync(int floorId)
  {
    const string query = @"
    SELECT *
    FROM `HomeRooms` hr
    WHERE
      hr.`DateDeletedUtc` IS NULL
      AND hr.`FloorId` = @FloorId
    ORDER BY hr.`RoomName`";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QueryAsync<HomeRoomDto>(query, new
    {
      FloorId = floorId,
    });
  }
}
