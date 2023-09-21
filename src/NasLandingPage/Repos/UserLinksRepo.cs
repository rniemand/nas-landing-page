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
    return new List<UserLinkEntity>();
  }
}
