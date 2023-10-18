using NasLandingPage.Models.Dto;
using NasLandingPage.Models;
using Dapper;

namespace NasLandingPage.Repos;

public interface IHomeRepo
{
  Task<IEnumerable<HomeDto>> GetHomesAsync(NlpUserContext userContext);
}

class HomeRepo : IHomeRepo
{
  private readonly IConnectionHelper _connectionHelper;

  public HomeRepo(IConnectionHelper connectionHelper)
  {
    _connectionHelper = connectionHelper;
  }

  public async Task<IEnumerable<HomeDto>> GetHomesAsync(NlpUserContext userContext)
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
}
