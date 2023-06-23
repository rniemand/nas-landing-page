using Dapper;
using NasLandingPage.Models.Entities;

namespace NasLandingPage.Repos;

public interface IGameImageRepo
{
  Task<ImageEntity?> GetGameCoverImageAsync(long gameId);
  Task<List<ImageEntity>> GetGameImagesAsync(long gameId);
}

public class GameImageRepo : IGameImageRepo
{
  public const string TableName = "GameImages";
  private readonly IConnectionHelper _connectionHelper;

  public GameImageRepo(IConnectionHelper connectionHelper)
  {
    _connectionHelper = connectionHelper;
  }

  public async Task<ImageEntity?> GetGameCoverImageAsync(long gameId)
  {
    const string query = @$"SELECT
	    gi.GameID,
	    gi.ImageType,
	    gi.ImageOrder,
	    gi.ImagePath
    FROM `{TableName}` gi
    WHERE gi.GameID = @GameID
	    AND gi.ImageType = 'cover'
    LIMIT 1";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QuerySingleOrDefaultAsync<ImageEntity>(query, new { GameID = gameId });
  }

  public async Task<List<ImageEntity>> GetGameImagesAsync(long gameId)
  {
    const string query = $@"SELECT
	    gi.GameID,
	    gi.ImageType,
	    gi.ImageOrder,
	    gi.ImagePath
    FROM `{TableName}` gi
    WHERE gi.GameID = @GameID";
    await using var connection = _connectionHelper.GetCoreConnection();
    return (await connection.QueryAsync<ImageEntity>(query, new { GameID = gameId })).AsList();
  }
}
