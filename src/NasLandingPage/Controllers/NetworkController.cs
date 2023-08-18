using Microsoft.AspNetCore.Mvc;
using NasLandingPage.Models.Dto;
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
}
