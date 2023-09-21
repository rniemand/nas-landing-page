using Dapper;
using NasLandingPage.Models;
using NasLandingPage.Models.Entities;
using Octokit;

namespace NasLandingPage.Repos;

public interface IUserLinksRepo
{
  Task<IEnumerable<UserLinkEntity>> GetUserLinksAsync(NlpUserContext userContext);
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
}
