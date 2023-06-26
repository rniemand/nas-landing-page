using Dapper;
using NasLandingPage.Models.Entities;

namespace NasLandingPage.Repos;

public interface IGameReceiptRepo
{
  Task<GameReceiptEntity?> GetByIDAsync(int receiptId);
  Task<int> UpdateAsync(GameReceiptEntity gameReceipt);
  Task<GameReceiptEntity?> GetByGameIDAsync(long gameId);
  Task<int> CreateNewReceiptAsync();
  Task<int> AssociateNewReceiptWithGameAsync(long gameId);
  Task<List<GameReceiptEntity>> SearchReceiptsAsync(string term);
  Task<int> AssociateGameReceiptAsync(long gameId, int receiptId);
}

public class GameReceiptRepo : IGameReceiptRepo
{
  public const string TableName = "GameReceipts";
  private readonly IConnectionHelper _connectionHelper;

  public GameReceiptRepo(IConnectionHelper connectionHelper)
  {
    _connectionHelper = connectionHelper;
  }

  public async Task<GameReceiptEntity?> GetByIDAsync(int receiptId)
  {
    const string query = @$"SELECT
	    o.ReceiptID,
	    o.Store,
	    o.ReceiptNumber,
	    o.ReceiptDate,
      o.ReceiptName,
      o.ReceiptUrl,
      o.ReceiptScanned
    FROM `{TableName}` o
    WHERE o.ReceiptID = @ReceiptID
    LIMIT 1";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QuerySingleOrDefaultAsync<GameReceiptEntity>(query, new { ReceiptID = receiptId });
  }

  public async Task<int> UpdateAsync(GameReceiptEntity gameReceipt)
  {
    const string query = @$"UPDATE `{TableName}`
    SET
      `Store` = @Store,
      `ReceiptNumber` = @ReceiptNumber,
      `ReceiptDate` = @ReceiptDate,
      `ReceiptName` = @ReceiptName,
      `ReceiptUrl` = @ReceiptUrl,
      `ReceiptScanned` = @ReceiptScanned
    WHERE
      `ReceiptID` = @ReceiptID";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, gameReceipt);
  }

  public async Task<GameReceiptEntity?> GetByGameIDAsync(long gameId)
  {
    const string query = @$"SELECT r.*
    FROM `{GamesRepo.TableName}` g
    INNER JOIN `{TableName}` r ON g.ReceiptID = r.ReceiptID
    WHERE g.GameID = @GameID";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QuerySingleOrDefaultAsync<GameReceiptEntity>(query, new { GameID = gameId });
  }

  public async Task<int> CreateNewReceiptAsync()
  {
    const string query = @$"INSERT INTO {TableName}
      (`Store`, `ReceiptNumber`, `ReceiptDate`, `ReceiptName`, `ReceiptUrl`, `ReceiptAssociated`)
    VALUES
      (NULL, NULL, NULL, NULL, NULL, 0)";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query);
  }

  public async Task<int> AssociateNewReceiptWithGameAsync(long gameId)
  {
    const string query = @$"UPDATE `{GamesRepo.TableName}`
    SET `ReceiptID` = (
	    SELECT r.ReceiptID
	    FROM `{TableName}` r
	    WHERE r.Store IS NULL
		    AND r.ReceiptNumber IS NULL
		    AND r.ReceiptDate IS NULL
		    AND r.ReceiptName IS NULL
		    AND r.ReceiptUrl IS NULL
		    AND r.ReceiptScanned = 0
        AND r.ReceiptAssociated = 0
	    LIMIT 1
    )
    WHERE GameID = @GameID;
    UPDATE {TableName}
    SET
      `ReceiptAssociated` = 1
    WHERE Store IS NULL
		  AND ReceiptNumber IS NULL
		  AND ReceiptDate IS NULL
		  AND ReceiptName IS NULL
		  AND ReceiptUrl IS NULL
		  AND ReceiptScanned = 0
      AND ReceiptAssociated = 0";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, new { GameID = gameId });
  }

  public async Task<List<GameReceiptEntity>> SearchReceiptsAsync(string term)
  {
    var query = @$"SELECT *
    FROM `{TableName}` r
    WHERE
	    r.Store LIKE '%{term}%'
	    OR r.ReceiptNumber LIKE '%{term}%'
	    OR r.ReceiptName LIKE '%{term}%'
	    OR r.ReceiptUrl LIKE '%{term}%'
    ORDER BY r.ReceiptID
    LIMIT 10";
    await using var connection = _connectionHelper.GetCoreConnection();
    return (await connection.QueryAsync<GameReceiptEntity>(query)).ToList();
  }

  public async Task<int> AssociateGameReceiptAsync(long gameId, int receiptId)
  {
    const string query = $@"UPDATE `{GamesRepo.TableName}`
    SET
	    `ReceiptID` = @ReceiptID
    WHERE
	    `GameID` = @GameID";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, new
    {
      ReceiptID = receiptId,
      GameID = gameId
    });
  }
}
