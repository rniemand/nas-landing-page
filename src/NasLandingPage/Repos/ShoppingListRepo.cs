using Dapper;
using NasLandingPage.Models;
using NasLandingPage.Models.Dto;
using NasLandingPage.Models.Requests;

namespace NasLandingPage.Repos;

public interface IShoppingListRepo
{
  Task<IEnumerable<ShoppingListItemDto>> GetShoppingListItemsAsync(NlpUserContext userContext, BasicSearchRequest request);
  Task<int> AddItemAsync(ShoppingListItemDto item);
  Task<int> UpdateItemAsync(ShoppingListItemDto item);
  Task<int> MarkBoughtAsync(ShoppingListItemDto item);
  Task<int> DeleteItemAsync(ShoppingListItemDto item);
  Task<decimal> GetItemLastKnownPriceAsync(ShoppingListItemDto item);
  Task<IEnumerable<string>> GetStoreNameSuggestionsAsync(NlpUserContext userContext, string? filter, bool includeBoughtEntries);
  Task<IEnumerable<string>> GetCategorySuggestionsAsync(NlpUserContext userContext, string? filter, bool includeBoughtEntries);
  Task<IEnumerable<string>> GetItemNameSuggestionsAsync(NlpUserContext userContext, string? filter, bool includeBoughtEntries);
}

public class ShoppingListRepo : IShoppingListRepo
{
  private readonly IConnectionHelper _connectionHelper;

  public ShoppingListRepo(IConnectionHelper connectionHelper)
  {
    _connectionHelper = connectionHelper;
  }

  public async Task<IEnumerable<ShoppingListItemDto>> GetShoppingListItemsAsync(NlpUserContext userContext, BasicSearchRequest request)
  {
    var query = @$"
      SELECT shop.*
      FROM `ShoppingList` shop
      INNER JOIN `Users` u
	      ON u.CurrentHomeID = shop.HomeId
	      AND shop.DateDeleted IS NULL
	      AND shop.DatePurchased IS NULL
	      AND u.UserID = @UserID
        {(string.IsNullOrWhiteSpace(request.Filter) ? "" : $"AND shop.StoreName = '{request.Filter}'")}
        {(string.IsNullOrWhiteSpace(request.SubFilter) ? "" : $"AND shop.Category = '{request.SubFilter}'")}
      ORDER BY shop.StoreName, shop.Category, shop.ItemName
    ";
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
      (`HomeId`, `AddedByUserId`, `StoreName`, `Category`, `ItemName`, `Quantity`, `LastKnownPrice`)
    VALUES
      (@HomeId, @AddedByUserId, @StoreName, @Category, @ItemName, @Quantity, @LastKnownPrice)";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, item);
  }

  public async Task<int> UpdateItemAsync(ShoppingListItemDto item)
  {
    const string query = @"
      UPDATE `ShoppingList`
      SET
        `LastKnownPrice` = @LastKnownPrice,
        `Quantity` = @Quantity,
        `StoreName` = @StoreName,
        `Category` = @Category,
        `ItemName` = @ItemName
      WHERE
        `ItemId` = @ItemId
    ";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, item);
  }

  public async Task<int> MarkBoughtAsync(ShoppingListItemDto item)
  {
    const string query = @"
      UPDATE `ShoppingList`
      SET
        `LastKnownPrice` = @LastKnownPrice,
        `Quantity` = @Quantity,
        `DatePurchased` = curdate()
      WHERE
        `ItemId` = @ItemId
    ";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, item);
  }

  public async Task<int> DeleteItemAsync(ShoppingListItemDto item)
  {
    const string query = @"
      UPDATE `ShoppingList`
      SET
        `DateDeleted` = curdate()
      WHERE
        `ItemId` = @ItemId
    ";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, item);
  }

  public async Task<decimal> GetItemLastKnownPriceAsync(ShoppingListItemDto item)
  {
    const string query = @"
      SELECT `LastKnownPrice`
      FROM `ShoppingList`
      WHERE
        `HomeId` = @HomeId
        AND `DateDeleted` IS NULL
        AND `DatePurchased` IS NOT NULL
        AND `StoreName` = @StoreName
        AND `ItemName` = @ItemName
        AND `Category` = @Category
      ORDER BY `DatePurchased` DESC
      LIMIT 1
    ";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QueryFirstOrDefaultAsync<decimal>(query, item);
  }

  public async Task<IEnumerable<string>> GetStoreNameSuggestionsAsync(NlpUserContext userContext, string? filter, bool includeBoughtEntries)
  {
    var query = @$"
    SELECT DISTINCT shop.StoreName
    FROM `ShoppingList` shop
    INNER JOIN `Users` u
	    ON u.CurrentHomeID = shop.HomeId
	    AND shop.DateDeleted IS NULL
	    AND u.UserID = @UserID
      {(includeBoughtEntries ? "" : "AND shop.`DatePurchased` IS NULL")}
      {(filter is null ? "" : $"AND shop.StoreName LIKE '%{filter}%'")}
    ORDER BY shop.StoreName";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QueryAsync<string>(query, new
    {
      UserID = userContext.UserId,
    });
  }

  public async Task<IEnumerable<string>> GetCategorySuggestionsAsync(NlpUserContext userContext, string? filter, bool includeBoughtEntries)
  {
    var query = @$"
    SELECT DISTINCT shop.Category
    FROM `ShoppingList` shop
    INNER JOIN `Users` u
	    ON u.CurrentHomeID = shop.HomeId
	    AND shop.DateDeleted IS NULL
      {(includeBoughtEntries ? "" : "AND shop.`DatePurchased` IS NULL")}
	    AND u.UserID = @UserID
      {(filter is null ? "" : $"AND shop.Category LIKE '%{filter}%'")}
    ORDER BY shop.Category";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QueryAsync<string>(query, new
    {
      UserID = userContext.UserId,
    });
  }

  public async Task<IEnumerable<string>> GetItemNameSuggestionsAsync(NlpUserContext userContext, string? filter, bool includeBoughtEntries)
  {
    var query = @$"
    SELECT DISTINCT shop.ItemName
    FROM `ShoppingList` shop
    INNER JOIN `Users` u
	    ON u.CurrentHomeID = shop.HomeId
	    AND shop.DateDeleted IS NULL
      {(includeBoughtEntries ? "" : "AND shop.`DatePurchased` IS NULL")}
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
