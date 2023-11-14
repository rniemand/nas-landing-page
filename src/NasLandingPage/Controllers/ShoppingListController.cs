using Microsoft.AspNetCore.Mvc;
using NasLandingPage.Extensions;
using NasLandingPage.Models.Dto;
using NasLandingPage.Models.Requests;
using NasLandingPage.Models.Responses;
using NasLandingPage.Services;

namespace NasLandingPage.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShoppingListController : ControllerBase
{
  private readonly IShoppingListService _shoppingListService;

  public ShoppingListController(IShoppingListService shoppingListService)
  {
    _shoppingListService = shoppingListService;
  }

  [HttpGet("")]
  public async Task<ShoppingListItemDto[]> GetShoppingList() =>
    await _shoppingListService.GetShoppingListAsync(User.GetNlpUserContext());

  [HttpPost("add-item")]
  public async Task<BoolResponse> AddShoppingListItem([FromBody] ShoppingListItemDto item) =>
    await _shoppingListService.AddShoppingListItemAsync(User.GetNlpUserContext(), item);

  [HttpPatch("update-item")]
  public async Task<BoolResponse> UpdateShoppingListItem([FromBody] ShoppingListItemDto item) =>
    await _shoppingListService.UpdateShoppingListItemAsync(User.GetNlpUserContext(), item);

  [HttpPost("store-name-suggestions")]
  public async Task<string[]> GetStoreNameSuggestions([FromBody] BasicSearchRequest request) =>
    await _shoppingListService.GetStoreNameSuggestionsAsync(User.GetNlpUserContext(), request);

  [HttpPost("category-suggestions")]
  public async Task<string[]> GetCategorySuggestions([FromBody] BasicSearchRequest request) =>
    await _shoppingListService.GetCategorySuggestionsAsync(User.GetNlpUserContext(), request);

  [HttpPost("item-name-suggestions")]
  public async Task<string[]> GetItemNameSuggestions([FromBody] BasicSearchRequest request) =>
    await _shoppingListService.GetItemNameSuggestionsAsync(User.GetNlpUserContext(), request);
}
