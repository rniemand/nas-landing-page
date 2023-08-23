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

  [HttpGet("id/{containerId:int}")]
  public async Task<ContainerDto> GetContainer([FromRoute] int containerId) =>
    await _containerService.GetContainerAsync(containerId);

  [HttpPost("items/add")]
  public async Task<BoolResponse> AddContainerItem([FromBody] ContainerItemDto item) =>
    await _containerService.AddContainerItemAsync(item);

  [HttpPost("item/categories/list")]
  public async Task<string[]> GetItemCategories([FromBody] string term) =>
    await _containerService.GetItemCategoriesAsync(term);
}
