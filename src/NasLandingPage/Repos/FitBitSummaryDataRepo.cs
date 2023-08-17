using Dapper;
using NasLandingPage.Models.FitBit;

namespace NasLandingPage.Repos;

public interface IFitBitSummaryDataRepo
{
  Task<int> DeleteDatedEntryAsync(int userId, DateOnly date);
  Task<int> AddEntryAsync(FitBitSummaryDataEntity entity);
}

public class FitBitSummaryDataRepo : IFitBitSummaryDataRepo
{
  public const string TableName = "FitBitSummaryData";
  private readonly IConnectionHelper _connectionHelper;

  public FitBitSummaryDataRepo(IConnectionHelper connectionHelper)
  {
    _connectionHelper = connectionHelper;
  }

  public async Task<int> DeleteDatedEntryAsync(int userId, DateOnly date)
  {
    const string query = $@"DELETE FROM `{TableName}`
    WHERE `Date` = @Date
    AND `UserId` = @UserId";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, new
    {
      UserId = userId,
      Date = date.ToString("O")
    });
  }

  public async Task<int> AddEntryAsync(FitBitSummaryDataEntity entity)
  {
    const string query = $@"INSERT INTO `{TableName}`
      (
        `UserId`, `Date`, `CaloriesOut`, `Distance`, `Elevation`, `Floors`, `LightlyActiveMinutes`,
        `MarginalCalories`, `RestingHeartRate`, `SedentaryMinutes`, `Steps`, `VeryActiveMinutes`
      ) VALUES (
        @UserId, @Date, `CaloriesOut`, @Distance, @Elevation, @Floors, @LightlyActiveMinutes,
        @MarginalCalories, @RestingHeartRate, @SedentaryMinutes, @Steps, @VeryActiveMinutes
      )";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, entity);
  }
}
