using NasLandingPage.Models;
using NasLandingPage.Models.Dto;
using NasLandingPage.Models.Responses;
using NasLandingPage.Repos;

namespace NasLandingPage.Services;

public interface IShoppingListService
{
  Task<ShoppingListItemDto[]> GetShoppingListAsync(NlpUserContext userContext);
  Task<BoolResponse> AddShoppingListItemAsync(NlpUserContext userContext, ShoppingListItemDto item);
  Task<string[]> GetStoreNameSuggestionsAsync(NlpUserContext userContext, string? filter);
  Task<string[]> GetCategorySuggestionsAsync(NlpUserContext userContext, string? filter);
  Task<string[]> GetItemNameSuggestionsAsync(NlpUserContext userContext, string? filter);
}

public class ShoppingListService : IShoppingListService
{
  private readonly IShoppingListRepo _shoppingListRepo;

  public ShoppingListService(IShoppingListRepo shoppingListRepo)
  {
    _shoppingListRepo = shoppingListRepo;
  }

  public async Task<ShoppingListItemDto[]> GetShoppingListAsync(NlpUserContext userContext) =>
    (await _shoppingListRepo.GetShoppingListItemsAsync(userContext)).ToArray();

  public async Task<BoolResponse> AddShoppingListItemAsync(NlpUserContext userContext, ShoppingListItemDto item)
  {
    var response = new BoolResponse();
    item.AddedByUserId = userContext.UserId;
    var rowCount = await _shoppingListRepo.AddItemAsync(item);
    return rowCount == 1 ? response : response.AsError("Failed to add shopping list item");
  }

  public async Task<string[]> GetStoreNameSuggestionsAsync(NlpUserContext userContext, string? filter) =>
    (await _shoppingListRepo.GetStoreNameSuggestionsAsync(userContext, filter)).ToArray();

  public async Task<string[]> GetCategorySuggestionsAsync(NlpUserContext userContext, string? filter) =>
    (await _shoppingListRepo.GetCategorySuggestionsAsync(userContext, filter)).ToArray();

  public async Task<string[]> GetItemNameSuggestionsAsync(NlpUserContext userContext, string? filter) =>
    (await _shoppingListRepo.GetItemNameSuggestionsAsync(userContext, filter)).ToArray();
}
