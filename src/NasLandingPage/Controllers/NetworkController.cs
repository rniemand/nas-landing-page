using Microsoft.AspNetCore.Mvc;
using NasLandingPage.Models.Dto;
using NasLandingPage.Models.Requests;
using NasLandingPage.Models.Responses;
using NasLandingPage.Services;

namespace NasLandingPage.Controllers;

[ApiController]
[Route("[controller]")]
public class NetworkController : ControllerBase
{
  private readonly INetworkService _networkService;

  public NetworkController(INetworkService networkService)
  {
    _networkService = networkService;
  }

  [HttpGet("devices")]
  public async Task<List<NetworkDeviceDto>> GetAllDevices() =>
    await _networkService.GetNetworkDevicesAsync();

  [HttpPost("add-device")]
  public async Task<BoolResponse> AddDevice([FromBody] AddNetworkDeviceRequest request) =>
    await _networkService.AddDeviceAsync(request);

  [HttpPost("device/classify")]
  public async Task<BoolResponse> ClassifyDevice([FromBody] ClassifyNetworkDeviceRequest request) =>
    await _networkService.ClassifyDeviceAsync(request);
}
