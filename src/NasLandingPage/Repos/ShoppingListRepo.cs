using Dapper;
using NasLandingPage.Models;
using NasLandingPage.Models.Dto;

namespace NasLandingPage.Repos;

public interface IShoppingListRepo
{
  Task<IEnumerable<ShoppingListItemDto>> GetShoppingListItemsAsync(NlpUserContext userContext);
  Task<int> AddItemAsync(ShoppingListItemDto item);
  Task<IEnumerable<string>> GetStoreNameSuggestionsAsync(NlpUserContext userContext, string? filter);
  Task<IEnumerable<string>> GetCategorySuggestionsAsync(NlpUserContext userContext, string? filter);
  Task<IEnumerable<string>> GetItemNameSuggestionsAsync(NlpUserContext userContext, string? filter);
}

public class ShoppingListRepo : IShoppingListRepo
{
  private readonly IConnectionHelper _connectionHelper;

  public ShoppingListRepo(IConnectionHelper connectionHelper)
  {
    _connectionHelper = connectionHelper;
  }

  public async Task<IEnumerable<ShoppingListItemDto>> GetShoppingListItemsAsync(NlpUserContext userContext)
  {
    const string query = @"
    SELECT shop.*
    FROM `ShoppingList` shop
    INNER JOIN `Users` u
	    ON u.CurrentHomeID = shop.HomeId
	    AND shop.DateDeleted IS NULL
	    AND shop.DatePurchased IS NULL
	    AND u.UserID = @UserID
    ORDER BY shop.StoreName, shop.Category, shop.ItemName";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QueryAsync<ShoppingListItemDto>(query, new
    {
      UserID = userContext.UserId,
    });
  }

  public async Task<int> AddItemAsync(ShoppingListItemDto item)
  {
    const string query = @"
    INSERT INTO `ShoppingList`
      (`HomeId`, `AddedByUserId`, `StoreName`, `Category`, `ItemName`, `Quantity`)
    VALUES
      (@HomeId, @AddedByUserId, @StoreName, @Category, @ItemName, @Quantity)";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, item);
  }

  public async Task<IEnumerable<string>> GetStoreNameSuggestionsAsync(NlpUserContext userContext, string? filter)
  {
    var query = @$"
    SELECT DISTINCT shop.StoreName
    FROM `ShoppingList` shop
    INNER JOIN `Users` u
	    ON u.CurrentHomeID = shop.HomeId
	    AND shop.DateDeleted IS NULL
	    AND u.UserID = @UserID
      {(filter is null ? "" : $"AND shop.StoreName LIKE '%{filter}%'")}
    ORDER BY shop.StoreName";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QueryAsync<string>(query, new
    {
      UserID = userContext.UserId,
    });
  }

  public async Task<IEnumerable<string>> GetCategorySuggestionsAsync(NlpUserContext userContext, string? filter)
  {
    var query = @$"
    SELECT DISTINCT shop.Category
    FROM `ShoppingList` shop
    INNER JOIN `Users` u
	    ON u.CurrentHomeID = shop.HomeId
	    AND shop.DateDeleted IS NULL
	    AND u.UserID = @UserID
      {(filter is null ? "" : $"AND shop.Category LIKE '%{filter}%'")}
    ORDER BY shop.Category";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QueryAsync<string>(query, new
    {
      UserID = userContext.UserId,
    });
  }

  public async Task<IEnumerable<string>> GetItemNameSuggestionsAsync(NlpUserContext userContext, string? filter)
  {
    var query = @$"
    SELECT DISTINCT shop.ItemName
    FROM `ShoppingList` shop
    INNER JOIN `Users` u
	    ON u.CurrentHomeID = shop.HomeId
	    AND shop.DateDeleted IS NULL
	    AND u.UserID = @UserID
      {(filter is null ? "" : $"AND shop.ItemName LIKE '%{filter}%'")}
    ORDER BY shop.ItemName";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QueryAsync<string>(query, new
    {
      UserID = userContext.UserId,
    });
  }
}
