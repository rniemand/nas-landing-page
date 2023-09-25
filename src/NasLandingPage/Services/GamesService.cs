using NasLandingPage.Models;
using NasLandingPage.Models.Dto;
using NasLandingPage.Repos;

namespace NasLandingPage.Services;

public interface IGamesService
{
  Task<GamePlatformDto[]> GetPlatformsAsync();
  Task<GameDto[]> GetPlatformGamesAsync(int platformId);
  Task<string> GetGameCoverImagePathAsync(string platform, int gameId);
}

public class GamesService : IGamesService
{
  private readonly IGamesRepo _gamesRepo;
  private readonly NlpConfig _config;

  public GamesService(IGamesRepo gamesRepo, NlpConfig config)
  {
    _gamesRepo = gamesRepo;
    _config = config;
  }

  public async Task<GamePlatformDto[]> GetPlatformsAsync() =>
    (await _gamesRepo.GetPlatformsAsync()).ToArray();

  public async Task<GameDto[]> GetPlatformGamesAsync(int platformId) =>
    (await _gamesRepo.GetPlatformGamesAsync(platformId)).ToArray();

  public async Task<string> GetGameCoverImagePathAsync(string platform, int gameId)
  {
    var dbLink = await _gamesRepo.GetGameCoverByGameIdAsync(gameId);
    var fallbackPath = Path.Join(_config.GamesImageRootDir, "covers", platform, _config.GamesImageFallback);
    if (dbLink is null) return fallbackPath;
    var dbFilePath = Path.Join(_config.GamesImageRootDir, dbLink);
    return !File.Exists(dbFilePath) ? fallbackPath : dbFilePath;
  }
}
