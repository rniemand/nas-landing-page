using Microsoft.AspNetCore.Mvc;
using NasLandingPage.Models.Dto;
using NasLandingPage.Models.Requests;
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

  [HttpPatch("update")]
  public async Task<BoolResponse> UpdateContainer([FromBody] ContainerDto container) =>
    await _containerService.UpdateContainerAsync(container);

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

  [HttpPatch("items/update")]
  public async Task<BoolResponse> UpdateContainerItem([FromBody] ContainerItemDto item) =>
    await _containerService.UpdateContainerItemAsync(item);

  [HttpPost("item/categories/list")]
  public async Task<string[]> GetItemCategories([FromBody] CategoryRequest request) =>
    await _containerService.GetItemCategoriesAsync(request);

  [HttpPost("item/sub-categories/list")]
  public async Task<string[]> GetItemSubCategories([FromBody] CategoryRequest request) =>
    await _containerService.GetItemSubCategoriesAsync(request);

  [HttpGet("items/container-id/{containerId:int}")]
  public async Task<List<ContainerItemDto>> GetContainerItems([FromRoute] int containerId) =>
    await _containerService.GetContainerItemsAsync(containerId);
}
