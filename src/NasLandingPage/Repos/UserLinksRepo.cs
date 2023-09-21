using Dapper;
using NasLandingPage.Models;
using NasLandingPage.Models.Entities;

namespace NasLandingPage.Repos;

public interface IUserLinksRepo
{
  Task<IEnumerable<UserLinkEntity>> GetUserLinksAsync(NlpUserContext userContext);
  Task<UserLinkEntity?> GetUserLinkByIdAsync(int linkId);
  Task<int> IncrementLinkFollowCountAsync(NlpUserContext userContext, int linkId);
};

internal class UserLinksRepo : IUserLinksRepo
{
  private readonly IConnectionHelper _connectionHelper;

  public UserLinksRepo(IConnectionHelper connectionHelper)
  {
    _connectionHelper = connectionHelper;
  }

  public async Task<IEnumerable<UserLinkEntity>> GetUserLinksAsync(NlpUserContext userContext)
  {
    const string query = @"SELECT *
    FROM `UserLinks` ul
    WHERE ul.UserID = @UserID
    AND ul.Deleted = 0
    ORDER BY ul.LinkCategory, ul.LinkOrder";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QueryAsync<UserLinkEntity>(query, new
    {
      UserID = userContext.UserId
    });
  }

  public async Task<UserLinkEntity?> GetUserLinkByIdAsync(int linkId)
  {
    const string query = @"SELECT *
    FROM `UserLinks` ul
    WHERE ul.LinkId = @LinkID
    LIMIT 1";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QuerySingleOrDefaultAsync<UserLinkEntity>(query, new
    {
      LinkID = linkId
    });
  }

  public async Task<int> IncrementLinkFollowCountAsync(NlpUserContext userContext, int linkId)
  {
    const string query = @"UPDATE `UserLinks`
    SET
	    FollowCount = FollowCount + 1,
	    DateLastFollowedUtc = utc_timestamp(6)
    WHERE
	    UserID = @UserID
	    AND LinkId = @LinkID";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, new
    {
      UserID = userContext.UserId,
      LinkID = linkId,
    });
  }
}
