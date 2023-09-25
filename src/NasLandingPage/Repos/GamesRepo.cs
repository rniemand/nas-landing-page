using Dapper;
using NasLandingPage.Models.Dto;

namespace NasLandingPage.Repos;

public interface IGamesRepo
{
  Task<IEnumerable<GamePlatformDto>> GetPlatformsAsync();
  Task<IEnumerable<GameDto>> GetPlatformGamesAsync(int platformId);
  Task<string?> GetGameCoverByGameIdAsync(int gameId);
}

public class GamesRepo : IGamesRepo
{
  private readonly IConnectionHelper _connectionHelper;

  public GamesRepo(IConnectionHelper connectionHelper)
  {
    _connectionHelper = connectionHelper;
  }

  public async Task<IEnumerable<GamePlatformDto>> GetPlatformsAsync()
  {
    const string query = @"
    SELECT *
    FROM `GamePlatforms`
    ORDER BY `PlatformName`";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QueryAsync<GamePlatformDto>(query);
  }

  public async Task<IEnumerable<GameDto>> GetPlatformGamesAsync(int platformId)
  {
    const string query = @"
    SELECT
	    g.*,
	    gp.PlatformName,
	    gl.LocationName,
	    gi.ImagePath
    FROM `Games` g
	    INNER JOIN `GamePlatforms` gp ON gp.PlatformID = g.PlatformID
	    LEFT JOIN `GameLocations` gl ON gl.LocationID = g.LocationID
	    LEFT JOIN `GameImages` gi ON gi.GameID = g.GameID AND gi.ImageType = 'cover' AND gi.ImageOrder = 1
    WHERE g.PlatformID = @PlatformID
    ORDER BY g.GameName
    ";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QueryAsync<GameDto>(query, new { PlatformID = platformId });
  }

  public async Task<string?> GetGameCoverByGameIdAsync(int gameId)
  {
    const string query = @"
    SELECT gi.ImagePath
    FROM `GameImages` gi
    WHERE
	    gi.GameID = @GameID
	    AND gi.ImageType = 'cover'
	    AND gi.ImageOrder = 1
    ";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QuerySingleOrDefaultAsync<string>(query, new
    {
      GameID = gameId
    });
  }
}
