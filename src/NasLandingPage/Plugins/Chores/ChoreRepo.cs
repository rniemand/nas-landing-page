using Dapper;
using NasLandingPage.Repos;

namespace NasLandingPage.Plugins.Chores;

interface IChoreRepo
{
  Task<int> AddChoreAsync(HomeChoreDto chore);
  Task<int> UpdateChoreAsync(HomeChoreDto chore);
  Task<int> RescheduleChoreAsync(HomeChoreDto chore);
  Task<HomeChoreDto?> GetChoreByIdAsync(int choreId);
  Task<int> AddChoreCompletedEntryAsync(HomeChoreHistoryDto entry);
  Task<IEnumerable<HomeChoreDto>> GetChoresAsync();
}

public class ChoreRepo : IChoreRepo
{
  private readonly IConnectionHelper _connectionHelper;

  public ChoreRepo(IConnectionHelper connectionHelper)
  {
    _connectionHelper = connectionHelper;
  }

  public async Task<int> AddChoreAsync(HomeChoreDto chore)
  {
    const string query = @"
    INSERT INTO `HomeChores`
      (`RoomId`, `ChorePoints`, `DateScheduled`, `Priority`, `IntervalModifier`, `Interval`, `ChoreName`, `ChoreDescription`)
    VALUES
      (@RoomId, @ChorePoints, @DateScheduled, @Priority, @IntervalModifier, @Interval, @ChoreName, @ChoreDescription)";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, chore);
  }

  public async Task<int> UpdateChoreAsync(HomeChoreDto chore)
  {
    const string query = @"
    UPDATE `HomeChores`
    SET
      `RoomId` = @RoomId,
      `ChorePoints` = @ChorePoints,
	    `Priority` = @Priority,
	    `IntervalModifier` = @IntervalModifier,
	    `Interval` = @Interval,
	    `ChoreName` = @ChoreName,
	    `ChoreDescription` = @ChoreDescription,
      `DateScheduled` = @DateScheduled
    WHERE
      `ChoreId` = @ChoreId";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, chore);
  }

  public async Task<int> RescheduleChoreAsync(HomeChoreDto chore)
  {
    const string query = @"
    UPDATE `HomeChores`
    SET
      `CompletedCount` = `CompletedCount` + 1,
      `DateScheduled` = @DateScheduled,
      `DateLastCompleted` = curdate()
    WHERE
      `ChoreId` = @ChoreId";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, chore);
  }

  public async Task<HomeChoreDto?> GetChoreByIdAsync(int choreId)
  {
    const string query = @"
    SELECT *
    FROM `HomeChores` hc
    WHERE
      hc.`ChoreId` = @ChoreId
      AND hc.`DateDeleted` IS NULL";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QueryFirstOrDefaultAsync<HomeChoreDto>(query, new
    {
      ChoreId = choreId,
    });
  }

  public async Task<int> AddChoreCompletedEntryAsync(HomeChoreHistoryDto entry)
  {
    const string query = @"
    INSERT INTO `HomeChoreHistory`
      (`ChoreId`, `UserId`, `Points`)
    VALUES
      (@ChoreId, @UserId, @Points)";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, entry);
  }

  public async Task<IEnumerable<HomeChoreDto>> GetChoresAsync()
  {
    const string query = @"
    SELECT
	    hc.*,
	    hr.`RoomName`,
	    hf.`FloorName`
    FROM `HomeChores` hc
    INNER JOIN `HomeRooms` hr
	    ON hr.`RoomId` = hc.`RoomId`
    INNER JOIN `HomeFloors` hf
	    ON hf.`FloorId` = hr.`FloorId`
    WHERE
      hc.`DateDeleted` IS NULL
	    AND hr.`DateDeletedUtc` IS NULL
	    AND hf.`DateDeletedUtc` IS NULL
    ORDER BY hc.`DateScheduled` ASC";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QueryAsync<HomeChoreDto>(query);
  }
}
