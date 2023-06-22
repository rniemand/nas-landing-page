using Dapper;
using NasLandingPage.Models.Entities;

namespace NasLandingPage.Repos;

public interface IGamePlatformRepo
{
  Task<List<PlatformEntity>> GetAllPlatformsAsync();
}

public class GamePlatformRepo : IGamePlatformRepo
{
  public const string TableName = "GamePlatforms";
  private readonly IConnectionHelper _connectionHelper;

  public GamePlatformRepo(IConnectionHelper connectionHelper)
  {
    _connectionHelper = connectionHelper;
  }

  public async Task<List<PlatformEntity>> GetAllPlatformsAsync()
  {
    const string query = $@"SELECT
	    p.PlatformID,
	    p.PlatformName
    FROM `{TableName}` p";
    await using var connection = _connectionHelper.GetCoreConnection();
    return (await connection.QueryAsync<PlatformEntity>(query)).AsList();
  }
}
