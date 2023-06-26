using Dapper;
using NasLandingPage.Models.Entities;

namespace NasLandingPage.Repos;

public interface IGamesRepo
{
  Task<List<BasicGameInfoEntity>> GetAllAsync(int platformId);
  Task<int> UpdateAsync(BasicGameInfoEntity game);
  Task<BasicGameInfoEntity?> GetByIDAsync(long gameId);
  Task<int> AddGameAsync(BasicGameInfoEntity gameInfo);
}

public class GamesRepo : IGamesRepo
{
  public const string TableName = "Games";
  private readonly IConnectionHelper _connectionHelper;

  public GamesRepo(IConnectionHelper connectionHelper)
  {
    _connectionHelper = connectionHelper;
  }

  public async Task<List<BasicGameInfoEntity>> GetAllAsync(int platformId) =>
    (await _connectionHelper.GetCoreConnection()
      .QueryAsync<BasicGameInfoEntity>(@$"SELECT
	        g.GameID,
	        g.GameName,
	        g.PlatformID,
	        g.LocationID,
	        g.GameCaseLocation,
	        g.HasGameBox,
	        g.GameRating,
	        l.LocationName,
	        p.PlatformName,
	        i.ImagePath,
          g.HasProtection,
	        r.Store,
	        r.ReceiptNumber,
	        g.GamePrice,
	        r.ReceiptDate,
          CASE WHEN g.SaleReceiptID IS NOT NULL THEN TRUE ELSE FALSE END AS `GameSold`,
          CASE WHEN g.ReceiptID IS NOT NULL THEN TRUE ELSE FALSE END AS `HasReceipt`,
          r.ReceiptName,
          r.ReceiptScanned,
          r.ReceiptID
        FROM `{TableName}` g
	        INNER JOIN `{GamePlatformRepo.TableName}` p ON p.PlatformID = g.PlatformID
	        INNER JOIN `{GameLocationRepo.TableName}` l ON l.LocationID = g.LocationID
	        LEFT JOIN `{GameImageRepo.TableName}` i ON i.GameID = g.GameID AND i.ImageType = 'cover'
          LEFT JOIN `{GameReceiptRepo.TableName}` r ON r.ReceiptID = g.ReceiptID
        WHERE g.PlatformID = @PlatformID
        ORDER BY g.GameName",
        new { PlatformID = platformId })
    ).ToList();

  public async Task<int> UpdateAsync(BasicGameInfoEntity game) =>
    await _connectionHelper.GetCoreConnection()
      .ExecuteAsync(@$"UPDATE `{TableName}`
      SET
	      `GameName` = @GameName,
	      `HasGameBox` = @HasGameBox,
        `HasProtection` = @HasProtection,
        `GameRating` = @GameRating,
        `GamePrice` = @GamePrice,
        `GameCaseLocation` = @GameCaseLocation
      WHERE
	      `GameID` = @GameID", game);

  public async Task<BasicGameInfoEntity?> GetByIDAsync(long gameId) =>
    await _connectionHelper.GetCoreConnection()
      .QuerySingleOrDefaultAsync<BasicGameInfoEntity>(@$"SELECT *
        FROM `{TableName}`
        WHERE
	        `GameID` = @GameID", new { GameID = gameId }
      );

  public async Task<int> AddGameAsync(BasicGameInfoEntity gameInfo) =>
    await _connectionHelper.GetCoreConnection()
      .ExecuteAsync(@$"INSERT INTO `{TableName}`
          (`PlatformID`, `LocationID`, `HasGameBox`, `HasProtection`, `GameRating`, `GamePrice`, `GameName`, `GameCaseLocation`)
        VALUES
          (@PlatformID, @LocationID, @HasGameBox, @HasProtection, @GameRating, @GamePrice, @GameName, @GameCaseLocation)",
        gameInfo
      );
}