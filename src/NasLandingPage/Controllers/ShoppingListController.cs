using Microsoft.AspNetCore.Mvc;
using NasLandingPage.Extensions;
using NasLandingPage.Models.Dto;
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

  [HttpGet("store-name-suggestions/{filter}")]
  public async Task<string[]> GetStoreNameSuggestions([FromRoute] string? filter) =>
    await _shoppingListService.GetStoreNameSuggestionsAsync(User.GetNlpUserContext(), filter);

  [HttpGet("category-suggestions/{filter}")]
  public async Task<string[]> GetCategorySuggestions([FromRoute] string? filter) =>
    await _shoppingListService.GetCategorySuggestionsAsync(User.GetNlpUserContext(), filter);

  [HttpGet("item-name-suggestions/{filter}")]
  public async Task<string[]> GetItemNameSuggestions([FromRoute] string? filter) =>
    await _shoppingListService.GetItemNameSuggestionsAsync(User.GetNlpUserContext(), filter);
}
