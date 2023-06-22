using NasLandingPage.Models.Dto;
using NasLandingPage.Repos;

namespace NasLandingPage.Services;

public interface IPlatformService
{
  Task<List<PlatformDto>> GetAllAsync();
}

public class PlatformService : IPlatformService
{
  private readonly IGamePlatformRepo _gamePlatformRepo;

  public PlatformService(IGamePlatformRepo gamePlatformRepo)
  {
    _gamePlatformRepo = gamePlatformRepo;
  }

  public async Task<List<PlatformDto>> GetAllAsync()
  {
    var dbPlatforms = await _gamePlatformRepo.GetAllPlatformsAsync();
    return dbPlatforms.Select(PlatformDto.FromEntity).ToList();
  }
}
