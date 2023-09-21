using NasLandingPage.Models;
using NasLandingPage.Models.Entities;

namespace NasLandingPage.Repos;

public interface IUserLinksRepo
{
  Task<IEnumerable<UserLinkEntity>> GetUserLinksAsync(NlpUserContext userContext);
}

internal class UserLinksRepo : IUserLinksRepo
{
  public async Task<IEnumerable<UserLinkEntity>> GetUserLinksAsync(NlpUserContext userContext)
  {
    await Task.CompletedTask;
    var query = @"SELECT *
    FROM `UserLinks` ul
    WHERE ul.UserID = 1
    AND ul.Deleted = 0
    ORDER BY ul.LinkCategory, ul.LinkOrder";
    return new List<UserLinkEntity>();
  }
}
