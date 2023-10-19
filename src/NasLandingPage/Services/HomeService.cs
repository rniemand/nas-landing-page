using NasLandingPage.Models;
using NasLandingPage.Models.Dto;
using NasLandingPage.Models.Responses;
using NasLandingPage.Repos;

namespace NasLandingPage.Services;

public interface IHomeService
{
  Task<HomeDto[]> ListHomesAsync(NlpUserContext userContext);
  Task<BoolResponse> AddHomeAsync(NlpUserContext userContext, HomeDto home);
  Task<BoolResponse> UpdateHomeAsync(NlpUserContext userContext, HomeDto home);
}

public class HomeService : IHomeService
{
  private readonly IHomeRepo _homeRepo;

  public HomeService(IHomeRepo homeRepo)
  {
    _homeRepo = homeRepo;
  }

  public async Task<HomeDto[]> ListHomesAsync(NlpUserContext userContext) =>
    (await _homeRepo.ListHomesAsync(userContext)).ToArray();

  public async Task<BoolResponse> AddHomeAsync(NlpUserContext userContext, HomeDto home)
  {
    var response = new BoolResponse();

    var rowCount = await _homeRepo.AddHomeAsync(home);
    if (rowCount == 0) return response.AsError("Failed to add home");

    var dbHome = await _homeRepo.ResolveHomeExactAsync(home);
    if (dbHome is null) return response.AsError("Failed to resolve added home");

    if (await _homeRepo.UserHomeMappingExistsAsync(userContext.UserId, dbHome.HomeId))
      return response;

    // ReSharper disable once ConvertIfStatementToReturnStatement
    if ((await _homeRepo.AddUserHomeMappingAsync(userContext.UserId, dbHome.HomeId)) == 1)
      return response;

    return response.AsError("Failed to associate user with home");
  }

  public async Task<BoolResponse> UpdateHomeAsync(NlpUserContext userContext, HomeDto home)
  {
    var response = new BoolResponse();

    if (!await _homeRepo.UserHomeMappingExistsAsync(userContext.UserId, home.HomeId))
      return response.AsError("You do not have access to this home");

    var rowCount = await _homeRepo.UpdateHomeAsync(home);
    return rowCount == 0 ? response.AsError("Failed to update home") : response;
  }
}
