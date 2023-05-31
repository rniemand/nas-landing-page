using NasLandingPage.Models.Dto;
using NasLandingPage.Repos;

namespace NasLandingPage.Services;

public interface IUserLinkService
{
  Task<List<UserLinkDto>> GetAllLinksAsync();
  Task<bool> RecordLinkFollow(int linkId);
}

public class UserLinkService : IUserLinkService
{
  private readonly IUserLinksRepo _linksRepo;

  public UserLinkService(IUserLinksRepo linksRepo)
  {
    _linksRepo = linksRepo;
  }

  public async Task<List<UserLinkDto>> GetAllLinksAsync()
  {
    var dbLinks = await _linksRepo.GetAllAsync();
    return dbLinks.Select(UserLinkDto.FromEntity).ToList();
  }

  public async Task<bool> RecordLinkFollow(int linkId) => (await _linksRepo.RecordFollowAsync(linkId)) == 1;
}
