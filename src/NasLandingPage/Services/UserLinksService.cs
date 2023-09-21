using NasLandingPage.Models;
using NasLandingPage.Models.Dto;
using NasLandingPage.Repos;

namespace NasLandingPage.Services;

public interface IUserLinksService
{
  Task<UserLinkDto[]> GetUserLinksAsync(NlpUserContext userContext);
}

internal class UserLinksService : IUserLinksService
{
  private readonly IUserLinksRepo _userLinksRepo;

  public UserLinksService(IUserLinksRepo userLinksRepo)
  {
    _userLinksRepo = userLinksRepo;
  }

  public async Task<UserLinkDto[]> GetUserLinksAsync(NlpUserContext userContext)
  {
    var dbLinks = await _userLinksRepo.GetUserLinksAsync(userContext);
    return dbLinks.Select(UserLinkDto.FromEntity).ToArray();
  }
}
