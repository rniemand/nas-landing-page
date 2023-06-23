using NasLandingPage.Models.Dto;
using NasLandingPage.Models.Entities;
using NasLandingPage.Repos;

namespace NasLandingPage.Services;

public interface IGameLocationService
{
  Task<List<LocationDto>> GetLocationsAsync(int platformId);
  Task<int> SetGameLocationAsync(long gameId, int locationId);
  Task<LocationDto> AddLocationAsync(LocationDto location);
}

public class GameLocationService : IGameLocationService
{
  private readonly IGameLocationRepo _gameLocationRepo;

  public GameLocationService(IGameLocationRepo gameLocationRepo)
  {
    _gameLocationRepo = gameLocationRepo;
  }

  public async Task<List<LocationDto>> GetLocationsAsync(int platformId)
  {
    var dbLocations = await _gameLocationRepo.GetLocationsAsync(platformId);
    return dbLocations.Count == 0 ? new List<LocationDto>() : dbLocations.Select(LocationDto.FromEntity).ToList();
  }

  public async Task<int> SetGameLocationAsync(long gameId, int locationId) =>
    await _gameLocationRepo.SetGameLocationAsync(gameId, locationId);

  public async Task<LocationDto> AddLocationAsync(LocationDto location)
  {
    var dbLocation = await _gameLocationRepo.GetLocationByNameAsync(location.PlatformID, location.LocationName);
    if (dbLocation is not null) return LocationDto.FromEntity(dbLocation);

    var rowCount = await _gameLocationRepo.AddLocationAsync(new LocationEntity
    {
      LocationName = location.LocationName,
      PlatformID = location.PlatformID,
    });
    if (rowCount < 0) throw new Exception("Failed to add location");

    dbLocation = await _gameLocationRepo.GetLocationByNameAsync(location.PlatformID, location.LocationName);
    if (dbLocation is null) throw new Exception("Failed to add location");
    return LocationDto.FromEntity(dbLocation);
  }
}
