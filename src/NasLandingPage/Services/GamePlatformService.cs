using NasLandingPage.Models.Dto;
using NasLandingPage.Repos;

namespace NasLandingPage.Services;

public interface IGamePlatformService
{
  Task<List<GamePlatformDto>> GetAllAsync();
}

public class GamePlatformService : IGamePlatformService
{
  private readonly IGamePlatformRepo _gamePlatformRepo;

  public GamePlatformService(IGamePlatformRepo gamePlatformRepo)
  {
    _gamePlatformRepo = gamePlatformRepo;
  }

  public async Task<List<GamePlatformDto>> GetAllAsync()
  {
    var dbPlatforms = await _gamePlatformRepo.GetAllPlatformsAsync();
    return dbPlatforms.Select(GamePlatformDto.FromEntity).ToList();
  }
}
