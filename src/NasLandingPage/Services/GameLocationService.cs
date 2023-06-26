using NasLandingPage.Models.Dto;
using NasLandingPage.Models.Entities;
using NasLandingPage.Repos;

namespace NasLandingPage.Services;

public interface IGameLocationService
{
  Task<List<GameLocationDto>> GetLocationsAsync(int platformId);
  Task<int> SetGameLocationAsync(long gameId, int locationId);
  Task<GameLocationDto> AddLocationAsync(GameLocationDto gameLocation);
}

public class GameLocationService : IGameLocationService
{
  private readonly IGameLocationRepo _gameLocationRepo;

  public GameLocationService(IGameLocationRepo gameLocationRepo)
  {
    _gameLocationRepo = gameLocationRepo;
  }

  public async Task<List<GameLocationDto>> GetLocationsAsync(int platformId)
  {
    var dbLocations = await _gameLocationRepo.GetLocationsAsync(platformId);
    return dbLocations.Count == 0 ? new List<GameLocationDto>() : dbLocations.Select(GameLocationDto.FromEntity).ToList();
  }

  public async Task<int> SetGameLocationAsync(long gameId, int locationId) =>
    await _gameLocationRepo.SetGameLocationAsync(gameId, locationId);

  public async Task<GameLocationDto> AddLocationAsync(GameLocationDto gameLocation)
  {
    var dbLocation = await _gameLocationRepo.GetLocationByNameAsync(gameLocation.PlatformID, gameLocation.LocationName);
    if (dbLocation is not null) return GameLocationDto.FromEntity(dbLocation);

    var rowCount = await _gameLocationRepo.AddLocationAsync(new GameLocationEntity
    {
      LocationName = gameLocation.LocationName,
      PlatformID = gameLocation.PlatformID,
    });
    if (rowCount < 0) throw new Exception("Failed to add location");

    dbLocation = await _gameLocationRepo.GetLocationByNameAsync(gameLocation.PlatformID, gameLocation.LocationName);
    if (dbLocation is null) throw new Exception("Failed to add location");
    return GameLocationDto.FromEntity(dbLocation);
  }
}
