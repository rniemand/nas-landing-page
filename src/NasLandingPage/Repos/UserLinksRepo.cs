using Dapper;
using NasLandingPage.Models;
using NasLandingPage.Models.Entities;

namespace NasLandingPage.Repos;

public interface IUserLinksRepo
{
  Task<IEnumerable<UserLinkEntity>> GetUserLinksAsync(NlpUserContext userContext);
  Task<UserLinkEntity?> GetUserLinkByIdAsync(int linkId);
}

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
    return await _connectionHelper.GetCoreConnection()
      .QueryAsync<UserLinkEntity>(query, new { UserID = userContext.UserId });
  }

  public async Task<UserLinkEntity?> GetUserLinkByIdAsync(int linkId)
  {
    const string query = @"SELECT *
    FROM `UserLinks` ul
    WHERE ul.LinkId = @LinkID
    LIMIT 1";
    return await _connectionHelper.GetCoreConnection()
      .QuerySingleOrDefaultAsync<UserLinkEntity>(query, new { LinkID = linkId });
  }
}
