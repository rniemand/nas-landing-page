using NasLandingPage.Models;
using NasLandingPage.Models.Dto;
using NasLandingPage.Models.Requests;
using NasLandingPage.Models.Responses;
using NasLandingPage.Repos;

namespace NasLandingPage.Services;

public interface IShoppingListService
{
  Task<ShoppingListItemDto[]> GetShoppingListAsync(NlpUserContext userContext, BasicSearchRequest request);
  Task<BoolResponse> AddShoppingListItemAsync(NlpUserContext userContext, ShoppingListItemDto item);
  Task<BoolResponse> UpdateShoppingListItemAsync(NlpUserContext userContext, ShoppingListItemDto item);
  Task<BoolResponse> MarkItemBoughtAsync(NlpUserContext userContext, ShoppingListItemDto item);
  Task<BoolResponse> DeleteItemAsync(NlpUserContext userContext, ShoppingListItemDto item);
  Task<decimal> GetItemLastKnownPriceAsync(NlpUserContext userContext, ShoppingListItemDto item);
  Task<string[]> GetStoreNameSuggestionsAsync(NlpUserContext userContext, BasicSearchRequest request);
  Task<string[]> GetCategorySuggestionsAsync(NlpUserContext userContext, BasicSearchRequest request);
  Task<string[]> GetItemNameSuggestionsAsync(NlpUserContext userContext, BasicSearchRequest request);
}

public class ShoppingListService : IShoppingListService
{
  private readonly IShoppingListRepo _shoppingListRepo;

  public ShoppingListService(IShoppingListRepo shoppingListRepo)
  {
    _shoppingListRepo = shoppingListRepo;
  }

  public async Task<ShoppingListItemDto[]> GetShoppingListAsync(NlpUserContext userContext, BasicSearchRequest request) =>
    (await _shoppingListRepo.GetShoppingListItemsAsync(userContext, request)).ToArray();

  public async Task<BoolResponse> AddShoppingListItemAsync(NlpUserContext userContext, ShoppingListItemDto item)
  {
    var response = new BoolResponse();
    item.AddedByUserId = userContext.UserId;
    var rowCount = await _shoppingListRepo.AddItemAsync(item);
    return rowCount == 1 ? response : response.AsError("Failed to add shopping list item");
  }

  public async Task<BoolResponse> UpdateShoppingListItemAsync(NlpUserContext userContext, ShoppingListItemDto item)
  {
    // TODO: [ACCESS] (ShoppingListService.UpdateShoppingListItemAsync) Check user has access to this item
    var response = new BoolResponse();
    var rowCount = await _shoppingListRepo.UpdateItemAsync(item);
    return rowCount == 1 ? response : response.AsError("Failed to update item");
  }

  public async Task<BoolResponse> MarkItemBoughtAsync(NlpUserContext userContext, ShoppingListItemDto item)
  {
    // TODO: [VALIDATION] (ShoppingListService.MarkItemBoughtAsync) Ensure user has access to this item
    var response = new BoolResponse();
    var rowCount = await _shoppingListRepo.MarkBoughtAsync(item);
    return rowCount == 1 ? response : response.AsError("Failed to mark item bought");
  }

  public async Task<BoolResponse> DeleteItemAsync(NlpUserContext userContext, ShoppingListItemDto item)
  {
    // TODO: [VALIDATION] (ShoppingListService.DeleteItemAsync) Ensure user has access
    var response = new BoolResponse();
    var rowCount = await _shoppingListRepo.DeleteItemAsync(item);
    return rowCount == 1 ? response : response.AsError("Failed to delete item");
  }

  public async Task<decimal> GetItemLastKnownPriceAsync(NlpUserContext userContext, ShoppingListItemDto item)
  {
    return await _shoppingListRepo.GetItemLastKnownPriceAsync(item);
  }

  public async Task<string[]> GetStoreNameSuggestionsAsync(NlpUserContext userContext, BasicSearchRequest request) =>
    (await _shoppingListRepo.GetStoreNameSuggestionsAsync(userContext, request.Filter, request.IncludeCompletedEntries)).ToArray();

  public async Task<string[]> GetCategorySuggestionsAsync(NlpUserContext userContext, BasicSearchRequest request) =>
    (await _shoppingListRepo.GetCategorySuggestionsAsync(userContext, request.Filter, request.IncludeCompletedEntries)).ToArray();

  public async Task<string[]> GetItemNameSuggestionsAsync(NlpUserContext userContext, BasicSearchRequest request) =>
    (await _shoppingListRepo.GetItemNameSuggestionsAsync(userContext, request.Filter, request.IncludeCompletedEntries)).ToArray();
}
