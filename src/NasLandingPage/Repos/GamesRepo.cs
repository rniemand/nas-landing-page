using Dapper;
using NasLandingPage.Models.Dto;

namespace NasLandingPage.Repos;

public interface IGamesRepo
{
  Task<IEnumerable<GamePlatformDto>> GetPlatformsAsync();
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
}
