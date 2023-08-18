using NasLandingPage.Models.Dto;
using NasLandingPage.Repos;

namespace NasLandingPage.Services;

public interface INetworkService
{
  Task<List<NetworkDeviceDto>> GetNetworkDevicesAsync();
}

public class NetworkService : INetworkService
{
  private readonly INetworkRepo _networkRepo;

  public NetworkService(INetworkRepo networkRepo)
  {
    _networkRepo = networkRepo;
  }

  public async Task<List<NetworkDeviceDto>> GetNetworkDevicesAsync()
  {
    var dbEntries = await _networkRepo.GetEnabledDevicesAsync();

    var mappedDevices = new Dictionary<int, NetworkDeviceDto>();
    foreach (var dbEntry in dbEntries)
    {
      if (!mappedDevices.ContainsKey(dbEntry.DeviceID))
        mappedDevices[dbEntry.DeviceID] = NetworkDeviceDto.FromEntity(dbEntry);
      mappedDevices[dbEntry.DeviceID].IPv4.Add(NetworkDeviceIPv4EntryDto.FromEntity(dbEntry));
    }

    return mappedDevices.Select(device => device.Value).ToList();
  }
}
