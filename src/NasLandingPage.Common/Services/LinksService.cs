using NasLandingPage.Common.Database;
using NasLandingPage.Common.Extensions;
using NasLandingPage.Common.Models.Responses;

namespace NasLandingPage.Common.Services;

public interface IUserLinkService
{
  Task<List<UserLink>> GetAll();
  Task RegisterFollow(int linkId);
  Task<List<string>> GetCategories();
}

public class UserLinkService : IUserLinkService
{
  private readonly IUserLinkRepo _linkRepo;

  public UserLinkService(IUserLinkRepo linkRepo)
  {
    _linkRepo = linkRepo;
  }

  public async Task<List<UserLink>> GetAll() =>
    (await _linkRepo.GetAllAsync()).ToDtoList();

  public async Task RegisterFollow(int linkId) =>
    await _linkRepo.UpdateFollowedAsync(linkId);

  public async Task<List<string>> GetCategories() =>
    (await _linkRepo.GetCategoriesAsync())
    .Where(x => !string.IsNullOrWhiteSpace(x.Value))
    .Select(x => x.Value)
    .ToList();
}
