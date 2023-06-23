using Dapper;
using NasLandingPage.Models.Entities;

namespace NasLandingPage.Repos;

public interface IGameLocationRepo
{
  Task<List<LocationEntity>> GetLocationsAsync(int platformId);
  Task<int> SetGameLocationAsync(long gameId, int locationID);
  Task<int> AddLocationAsync(LocationEntity entity);
  Task<LocationEntity?> GetLocationByNameAsync(int platformId, string name);
}

public class GameLocationRepo : IGameLocationRepo
{
  public const string TableName = "GameLocations";
  private readonly IConnectionHelper _connectionHelper;

  public GameLocationRepo(IConnectionHelper connectionHelper)
  {
    _connectionHelper = connectionHelper;
  }

  public async Task<List<LocationEntity>> GetLocationsAsync(int platformId)
  {
    const string query = $@"SELECT
	    l.LocationID,
	    l.PlatformID,
	    l.LocationName
    FROM `{TableName}` l
    WHERE l.PlatformID = @PlatformID";
    await using var connection = _connectionHelper.GetCoreConnection();
    return (await connection.QueryAsync<LocationEntity>(query, new { PlatformID = platformId })).AsList();
  }

  public async Task<int> SetGameLocationAsync(long gameId, int locationID)
  {
    const string query = $@"UPDATE `{GamesRepo.TableName}`
    SET `LocationID` = (
	    SELECT `LocationID`
	    FROM `{TableName}`
	    WHERE `PlatformID` = (
		    SELECT `PlatformID`
		    FROM `{GamesRepo.TableName}`
		    WHERE `GameID` = @GameID
	    )
	    AND `LocationName` = 'Home'
    )
    WHERE `LocationID` = @LocationID;
    UPDATE `{GamesRepo.TableName}`
    SET `LocationID` = @LocationID
    WHERE `GameID` = @GameID";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, new
    {
      GameID = gameId,
      LocationID = locationID,
    });
  }

  public async Task<int> AddLocationAsync(LocationEntity entity)
  {
    const string query = $@"INSERT INTO `{TableName}`
      (`PlatformID`, `LocationName`)
    VALUES
      (@PlatformID, @LocationName)";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, entity);
  }

  public async Task<LocationEntity?> GetLocationByNameAsync(int platformId, string name)
  {
    const string query = $@"SELECT *
    FROM `{TableName}`
    WHERE
      `PlatformID` = @PlatformID
      AND `LocationName` = @LocationName";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QueryFirstOrDefaultAsync<LocationEntity>(query, new
    {
      PlatformID = platformId,
      LocationName = name
    });
  }
}
