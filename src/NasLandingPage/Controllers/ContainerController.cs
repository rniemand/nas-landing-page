using Microsoft.AspNetCore.Mvc;
using NasLandingPage.Models.Dto;
using NasLandingPage.Models.Responses;
using NasLandingPage.Services;

namespace NasLandingPage.Controllers;

[ApiController]
[Route("[controller]")]
public class ContainerController : ControllerBase
{
  private readonly IContainerService _containerService;

  public ContainerController(IContainerService containerService)
  {
    _containerService = containerService;
  }

  [HttpPost("add")]
  public async Task<BoolResponse> AddContainer([FromBody] ContainerDto container) =>
    await _containerService.AddContainerAsync(container);

  [HttpGet("list")]
  public async Task<List<ContainerDto>> GetAllContainers() =>
    await _containerService.GetAllContainersAsync();

  [HttpPost("exists")]
  public async Task<BoolResponse> CheckContainerExists([FromBody] ContainerDto container) =>
    await _containerService.ContainerExistsAsync(container);
}
