using NasLandingPage.Models.Dto;
using NasLandingPage.Models;
using Dapper;

namespace NasLandingPage.Repos;

public interface IHomeRepo
{
  Task<IEnumerable<HomeDto>> ListHomesAsync(NlpUserContext userContext);
  Task<int> AddHomeAsync(HomeDto home);
  Task<HomeDto?> ResolveHomeExactAsync(HomeDto home);
  Task<bool> UserHomeMappingExistsAsync(int userId, int homeId);
  Task<int> AddUserHomeMappingAsync(int userId, int homeId);
  Task<int> UpdateHomeAsync(HomeDto home);
}

internal class HomeRepo : IHomeRepo
{
  private readonly IConnectionHelper _connectionHelper;

  public HomeRepo(IConnectionHelper connectionHelper)
  {
    _connectionHelper = connectionHelper;
  }

  public async Task<IEnumerable<HomeDto>> ListHomesAsync(NlpUserContext userContext)
  {
    const string query = @"
    SELECT h.*
    FROM `Homes` h
    INNER JOIN `UserHomeMappings` uhm
	    ON uhm.`HomeID` = h.`HomeId`
	    AND uhm.`UserID` = @UserID
	    AND uhm.`DateDeleted` IS NULL
	    AND h.`DateDeleted` IS NULL";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QueryAsync<HomeDto>(query, new
    {
      UserID = userContext.UserId,
    });
  }

  public async Task<int> AddHomeAsync(HomeDto home)
  {
    const string query = @"
    INSERT INTO `Homes`
      (`Longitude`,`Latitude`,`Country`,`PostalCode`,`City`,`Province`,`HomeName`,`AddressLine1`,`AddressLine2`)
    VALUES
      (@Longitude,@Latitude,@Country,@PostalCode,@City,@Province,@HomeName,@AddressLine1,@AddressLine2)";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, home);
  }

  public async Task<HomeDto?> ResolveHomeExactAsync(HomeDto home)
  {
    const string query = @"
    SELECT *
    FROM `Homes`
    WHERE
      `Longitude` = @Longitude
      AND `Latitude` = @Latitude
      AND `Country` = @Country
      AND `PostalCode` = @PostalCode
      AND `City` = @City
      AND `Province` = @Province
      AND `HomeName` = @HomeName
      AND `AddressLine1` = @AddressLine1
      AND `AddressLine2` = @AddressLine2";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QueryFirstOrDefaultAsync<HomeDto>(query, home);
  }

  public async Task<bool> UserHomeMappingExistsAsync(int userId, int homeId)
  {
    const string query = @"
    SELECT COUNT(1)
    FROM `UserHomeMappings`
    WHERE
      `UserID` = @UserID
      AND `HomeID` = @HomeID
      AND `DateDeleted` IS NULL";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteScalarAsync<bool>(query, new
    {
      UserID = userId,
      HomeID = homeId,
    });
  }

  public async Task<int> AddUserHomeMappingAsync(int userId, int homeId)
  {
    const string query = @"
    INSERT INTO `UserHomeMappings`
      (`UserID`,`HomeID`)
    VALUES
      (@UserID,@HomeID)";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, new
    {
      UserID = userId,
      HomeID = homeId,
    });
  }

  public async Task<int> UpdateHomeAsync(HomeDto home)
  {
    const string query = @"
    UPDATE `Homes`
    SET
      `Longitude` = @Longitude,
      `Latitude` = @Latitude,
      `Country` = @Country,
      `PostalCode` = @PostalCode,
      `City` = @City,
      `Province` = @Province,
      `HomeName` = @HomeName,
      `AddressLine1` = @AddressLine1,
      `AddressLine2` = @AddressLine2
    WHERE `HomeId` = @HomeId";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, home);
  }
}
