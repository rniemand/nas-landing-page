using NasLandingPage.Models.Dto;
using NasLandingPage.Repos;

namespace NasLandingPage.Services;

public interface IGamesService
{
  Task<GamePlatformDto[]> GetPlatformsAsync();
}

public class GamesService : IGamesService
{
  private readonly IGamesRepo _gamesRepo;

  public GamesService(IGamesRepo gamesRepo)
  {
    _gamesRepo = gamesRepo;
  }

  public async Task<GamePlatformDto[]> GetPlatformsAsync() =>
    (await _gamesRepo.GetPlatformsAsync()).ToArray();
}
