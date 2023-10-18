using Dapper;
using NasLandingPage.Models;
using NasLandingPage.Models.Dto;

namespace NasLandingPage.Repos;

public interface IFloorRepo
{
  Task<IEnumerable<HomeFloorDto>> GetFloorsAsync(NlpUserContext userContext);
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

  public async Task<IEnumerable<HomeFloorDto>> GetFloorsAsync(NlpUserContext userContext)
  {
    const string query = @"
    SELECT hf.*
    FROM `Users` u
    INNER JOIN `UserHomeMappings` uhm
	    ON uhm.`UserID` = u.`UserID`
	    AND u.`UserID` = @UserID
	    AND uhm.`DateDeleted` IS NULL
	    AND uhm.`HomeID` = u.`CurrentHomeID`
    INNER JOIN `HomeFloors` hf
	    ON hf.`HomeId` = uhm.`HomeID`
	    AND hf.`DateDeleted` IS NULL
    ORDER BY hf.`FloorName`";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QueryAsync<HomeFloorDto>(query, new
    {
      UserID = userContext.UserId,
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
