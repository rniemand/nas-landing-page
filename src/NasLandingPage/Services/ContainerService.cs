using NasLandingPage.Models.Dto;
using NasLandingPage.Models.Requests;
using NasLandingPage.Models.Responses;
using NasLandingPage.Repos;

namespace NasLandingPage.Services;

public interface IContainerService
{
  Task<BoolResponse> AddContainerAsync(ContainerDto containerDto);
  Task<List<ContainerDto>> GetAllContainersAsync();
  Task<BoolResponse> ContainerExistsAsync(ContainerDto containerDto);
  Task<ContainerDto> GetContainerAsync(int containerId);
  Task<BoolResponse> AddContainerItemAsync(ContainerItemDto itemDto);
  Task<string[]> GetItemCategoriesAsync(CategoryRequest request);
  Task<string[]> GetItemSubCategoriesAsync(CategoryRequest request);
  Task<List<ContainerItemDto>> GetContainerItemsAsync(int containerId);
}

public class ContainerService : IContainerService
{
  private readonly IContainerRepo _containerRepo;

  public ContainerService(IContainerRepo containerRepo)
  {
    _containerRepo = containerRepo;
  }

  public async Task<BoolResponse> AddContainerAsync(ContainerDto containerDto)
  {
    var response = new BoolResponse();
    var rowCount = await _containerRepo.AddContainerAsync(containerDto.AsEntity());
    return rowCount == 1 ? response : response.AsError("Failed to add container");
  }

  public async Task<List<ContainerDto>> GetAllContainersAsync()
  {
    var dbContainers = await _containerRepo.GetAllContainersAsync();
    return dbContainers.Select(ContainerDto.FromEntity).ToList();
  }

  public async Task<BoolResponse> ContainerExistsAsync(ContainerDto containerDto)
  {
    var containerCount = await _containerRepo.ContainerExistsAsync(containerDto.AsEntity());
    return new BoolResponse(containerCount != 0);
  }

  public async Task<ContainerDto> GetContainerAsync(int containerId) =>
    ContainerDto.FromEntity(await _containerRepo.GetContainerAsync(containerId));

  public async Task<BoolResponse> AddContainerItemAsync(ContainerItemDto itemDto)
  {
    var response = new BoolResponse();
    var rowCount = await _containerRepo.AddContainerItemAsync(itemDto.ToEntity());
    return rowCount == 1 ? response : response.AsError("Failed to add container item");
  }

  public async Task<string[]> GetItemCategoriesAsync(CategoryRequest request) =>
    await _containerRepo.GetItemCategoriesAsync(request.Category);

  public async Task<string[]> GetItemSubCategoriesAsync(CategoryRequest request) =>
    await _containerRepo.GetItemSubCategoriesAsync(request.Category, request.SubCategory);

  public async Task<List<ContainerItemDto>> GetContainerItemsAsync(int containerId)
  {
    var dbItems = await _containerRepo.GetContainerItemsAsync(containerId);
    return dbItems.Select(ContainerItemDto.FromEntity).ToList();
  }
}
