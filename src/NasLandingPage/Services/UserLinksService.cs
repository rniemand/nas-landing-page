using NasLandingPage.Models;
using NasLandingPage.Models.Dto;
using NasLandingPage.Repos;

namespace NasLandingPage.Services;

public interface IUserLinksService
{
  Task<UserLinkDto[]> GetUserLinksAsync(NlpUserContext userContext);
  Task<string> GetUserLinkImagePathAsync(int linkId);
  Task IncrementLinkFollowCountAsync(NlpUserContext userContext, int linkId);
}

internal class UserLinksService : IUserLinksService
{
  private readonly IUserLinksRepo _userLinksRepo;
  private readonly NlpConfig _config;

  public UserLinksService(IUserLinksRepo userLinksRepo, NlpConfig config)
  {
    _userLinksRepo = userLinksRepo;
    _config = config;
  }

  public async Task<UserLinkDto[]> GetUserLinksAsync(NlpUserContext userContext)
  {
    var dbLinks = await _userLinksRepo.GetUserLinksAsync(userContext);
    return dbLinks.Select(UserLinkDto.FromEntity).ToArray();
  }

  public async Task<string> GetUserLinkImagePathAsync(int linkId)
  {
    var dbLink = await _userLinksRepo.GetUserLinkByIdAsync(linkId);
    var fallbackPath = Path.Join(_config.LinkImageRootDir, _config.LinkImageFallback);
    if (dbLink is null) return fallbackPath;
    var dbFilePath = Path.Join(_config.LinkImageRootDir, dbLink.LinkImage);
    return !File.Exists(dbFilePath) ? fallbackPath : dbFilePath;
  }

  public async Task IncrementLinkFollowCountAsync(NlpUserContext userContext, int linkId) =>
    await _userLinksRepo.IncrementLinkFollowCountAsync(userContext, linkId);
}
