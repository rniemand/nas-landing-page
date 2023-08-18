using NasLandingPage.Models.Dto;
using NasLandingPage.Models.Requests;
using NasLandingPage.Models.Responses;
using NasLandingPage.Repos;

namespace NasLandingPage.Services;

public interface INetworkService
{
  Task<List<NetworkDeviceDto>> GetNetworkDevicesAsync();
  Task<BoolResponse> AddDeviceAsync(AddNetworkDeviceRequest request);
  Task<BoolResponse> ClassifyDeviceAsync(ClassifyNetworkDeviceRequest request);
  Task<BoolResponse> AddIPv4AddressAsync(AddNetworkIPv4Request request);
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
      {
        mappedDevices[dbEntry.DeviceID] = NetworkDeviceDto.FromEntity(dbEntry);
        if (!string.IsNullOrWhiteSpace(dbEntry.Category))
          mappedDevices[dbEntry.DeviceID].Classification = NetworkDeviceClassificationDto.FromEntity(dbEntry);
      }

      if (!string.IsNullOrWhiteSpace(dbEntry.IPv4))
        mappedDevices[dbEntry.DeviceID].IPv4.Add(NetworkDeviceIPv4EntryDto.FromEntity(dbEntry));
    }

    return mappedDevices.Select(device => device.Value).ToList();
  }

  public async Task<BoolResponse> AddDeviceAsync(AddNetworkDeviceRequest request)
  {
    var response = new BoolResponse();
    var rowCount = await _networkRepo.AddDeviceAsync(request);
    return rowCount == 1 ? response : response.AsError("Failed to add device");
  }

  public async Task<BoolResponse> ClassifyDeviceAsync(ClassifyNetworkDeviceRequest request)
  {
    var response = new BoolResponse();
    var rowCount = await _networkRepo.AddDeviceClassificationAsync(request);
    return rowCount == 1 ? response : response.AsError("Failed to classify device");
  }

  public async Task<BoolResponse> AddIPv4AddressAsync(AddNetworkIPv4Request request)
  {
    var response = new BoolResponse();
    var rowCount = await _networkRepo.AddIPv4AddressAsync(request);
    return rowCount == 1 ? response : response.AsError("Failed to add IPv4 address");
  }
}
