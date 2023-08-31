using Dapper;
using NasLandingPage.Models.Entities;
using NasLandingPage.Models.Requests;
using System.ComponentModel;

namespace NasLandingPage.Repos;

public interface IContainerRepo
{
  Task<int> AddContainerAsync(ContainerEntity container);
  Task<int> UpdateContainerAsync(ContainerEntity container);
  Task<List<ContainerEntity>> GetAllContainersAsync();
  Task<int> ContainerExistsAsync(ContainerEntity container);
  Task<ContainerEntity> GetContainerAsync(int containerId);
  Task<List<IntSelectOptionEntity>> GetContainerDropdownOptionsAsync();
  Task<int> AddContainerItemAsync(ContainerItemEntity item);
  Task<int> UpdateContainerItemAsync(ContainerItemEntity item);
  Task<string[]> GetItemCategoriesAsync(string term);
  Task<string[]> GetItemSubCategoriesAsync(string category, string term);
  Task<List<ContainerItemEntity>> GetContainerItemsAsync(int containerId);
  Task<List<ContainerItemEntity>> SearchContainerItemsAsync(SearchContainerItemsRequest request);
  Task<int> UpdateContainerItemCountAsync(int containerId);
  Task<int> UpdateContainerItemCountFromItemIdAsync(int itemId);
  Task<int> DecrementItemQuantityAsync(int itemId, int decrementAmount);
  Task<int> IncrementItemQuantityAsync(int itemId, int incrementAmount);
  Task<int> SetItemQuantityAsync(int itemId, int quantity);
}

public class ContainerRepo : IContainerRepo
{
  private readonly IConnectionHelper _connectionHelper;

  public ContainerRepo(IConnectionHelper connectionHelper)
  {
    _connectionHelper = connectionHelper;
  }

  public async Task<int> AddContainerAsync(ContainerEntity container)
  {
    const string query = @"INSERT INTO `Containers`
	    (`ShelfNumber`, `ShelfLevel`, `ShelfRow`, `ShelfRowPosition`, `ItemCount`, `ContainerLabel`, `ContainerName`, `Notes`)
    VALUES
	    (@ShelfNumber, @ShelfLevel, @ShelfRow, @ShelfRowPosition, @ItemCount, @ContainerLabel, @ContainerName, @Notes)";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, container);
  }

  public async Task<int> UpdateContainerAsync(ContainerEntity container)
  {
    const string query = @"UPDATE `Containers`
    SET
	    `DateUpdatedUtc` = utc_timestamp(6),
	    `ContainerName` = @ContainerName,
	    `Notes` = @Notes
    WHERE `ContainerId` = @ContainerId";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, container);
  }

  public async Task<List<ContainerEntity>> GetAllContainersAsync()
  {
    var query = @"SELECT
	    con.`ContainerId`,
	    con.`Deleted`,
	    con.`ShelfNumber`,
	    con.`ShelfLevel`,
	    con.`ShelfRow`,
	    con.`ShelfRowPosition`,
	    con.`ItemCount`,
	    con.`DateAddedUtc`,
	    con.`DateUpdatedUtc`,
	    con.`ContainerLabel`,
	    con.`ContainerName`,
	    con.`Notes`
    FROM `Containers` con
    WHERE con.`Deleted` = 0
    ORDER BY con.`ShelfNumber`, con.`ShelfLevel`, con.`ShelfRow`, con.`ShelfRowPosition`";
    await using var connection = _connectionHelper.GetCoreConnection();
    return (await connection.QueryAsync<ContainerEntity>(query)).ToList();
  }

  public async Task<int> ContainerExistsAsync(ContainerEntity container)
  {
    const string query = @"SELECT COUNT(1)
    FROM `Containers` c
    WHERE c.ShelfNumber = @ShelfNumber
	    AND c.ShelfLevel = @ShelfLevel
	    AND c.ShelfRow = @ShelfRow
	    AND c.ShelfRowPosition = @ShelfRowPosition
	    AND c.Deleted = 0";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteScalarAsync<int>(query, container);
  }

  public async Task<ContainerEntity> GetContainerAsync(int containerId)
  {
    var query = @"SELECT
	    con.`ContainerId`,
	    con.`Deleted`,
	    con.`ShelfNumber`,
	    con.`ShelfLevel`,
	    con.`ShelfRow`,
	    con.`ShelfRowPosition`,
	    con.`ItemCount`,
	    con.`DateAddedUtc`,
	    con.`DateUpdatedUtc`,
	    con.`ContainerLabel`,
	    con.`ContainerName`,
	    con.`Notes`
    FROM `Containers` con
    WHERE con.`ContainerId` = @ContainerId";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QuerySingleAsync<ContainerEntity>(query, new { ContainerId = containerId });
  }

  public async Task<List<IntSelectOptionEntity>> GetContainerDropdownOptionsAsync()
  {
    var query = @"SELECT
	    c.ContainerId AS `Value`,
	    CONCAT('(', c.ContainerLabel, ') ', c.ContainerName) AS `Title`
    FROM `Containers` c
    WHERE c.Deleted = 0
    ORDER BY c.ShelfNumber, c.ShelfLevel, c.ShelfRow, c.ShelfRowPosition";
    await using var connection = _connectionHelper.GetCoreConnection();
    return (await connection.QueryAsync<IntSelectOptionEntity>(query)).ToList();
  }

  public async Task<int> AddContainerItemAsync(ContainerItemEntity item)
  {
    const string query = @"INSERT INTO `ContainerItems`
      (
        `ContainerId`, `Quantity`, `OrderMoreMinQty`, `OrderMore`, `OrderPlaced`,
        `AutoFlagOrderMore`, `Category`, `SubCategory`, `InventoryName`, `OrderUrl`
      )
    VALUES
      (
        @ContainerId, @Quantity, @OrderMoreMinQty, @OrderMore, @OrderPlaced,
        @AutoFlagOrderMore, @Category, @SubCategory, @InventoryName, @OrderUrl
      )";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, item);
  }

  public async Task<int> UpdateContainerItemAsync(ContainerItemEntity item)
  {
    const string query = @"UPDATE `ContainerItems`
    SET
	    `Quantity` = @Quantity,
	    `OrderMoreMinQty` = @OrderMoreMinQty,
	    `AutoFlagOrderMore` = @AutoFlagOrderMore,
	    `DateUpdatedUtc` = utc_timestamp(6),
	    `Category` = @Category,
	    `SubCategory` = @SubCategory,
	    `InventoryName` = @InventoryName,
	    `OrderUrl` = @OrderUrl
    WHERE `ItemId` = @ItemId";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, item);
  }

  public async Task<string[]> GetItemCategoriesAsync(string term)
  {
    var query = @$"SELECT distinct ci.`Category`
    FROM `ContainerItems` ci
    WHERE ci.Deleted = 0
	    AND ci.Category LIKE '%{term}%'
    ORDER BY ci.Category";
    await using var connection = _connectionHelper.GetCoreConnection();
    return (await connection.QueryAsync<string>(query)).ToArray();
  }

  public async Task<string[]> GetItemSubCategoriesAsync(string category, string term)
  {
    var query = @$"SELECT distinct ci.`SubCategory`
    FROM `ContainerItems` ci
    WHERE ci.Deleted = 0
	    AND ci.Category = '{category}'
      AND ci.SubCategory LIKE '%{term}%'
    ORDER BY ci.Category";
    await using var connection = _connectionHelper.GetCoreConnection();
    return (await connection.QueryAsync<string>(query)).ToArray();
  }

  public async Task<List<ContainerItemEntity>> GetContainerItemsAsync(int containerId)
  {
    var query = $@"SELECT
      ci.*
    FROM `Containers` con
    INNER JOIN `ContainerItems` ci
	    ON ci.ContainerId = con.ContainerId
    WHERE con.ContainerId = {containerId}
	    AND ci.Deleted = 0
    ORDER BY ci.Category, ci.SubCategory, ci.InventoryName";
    await using var connection = _connectionHelper.GetCoreConnection();
    return (await connection.QueryAsync<ContainerItemEntity>(query)).ToList();
  }

  public async Task<List<ContainerItemEntity>> SearchContainerItemsAsync(SearchContainerItemsRequest request)
  {
    var query = $@"SELECT *
    FROM `ContainerItems` ci
    INNER JOIN `Containers` c ON c.ContainerId = ci.ContainerId
    WHERE ci.Deleted = 0
	    {(!string.IsNullOrWhiteSpace(request.Category) ? $"AND ci.Category = '{request.Category}'" : "")}
      {(!string.IsNullOrWhiteSpace(request.SubCategory) ? $"AND ci.SubCategory = '{request.SubCategory}'" : "")}
	    AND ci.InventoryName LIKE '%{request.Term}%'";
    await using var connection = _connectionHelper.GetCoreConnection();
    return (await connection.QueryAsync<ContainerItemEntity>(query)).ToList();
  }

  public async Task<int> UpdateContainerItemCountAsync(int containerId)
  {
    var query = $@"UPDATE `Containers` c
    SET c.`ItemCount` = (
	    SELECT SUM(ci.Quantity)
	    FROM `ContainerItems` ci
	    WHERE ci.ContainerId = {containerId}
	    AND ci.Deleted = 0
    )
    WHERE c.ContainerId = {containerId}";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query);
  }

  public async Task<int> UpdateContainerItemCountFromItemIdAsync(int itemId)
  {
    var query = $@"UPDATE `Containers` c
    SET c.`ItemCount` = (
	    SELECT SUM(ci.Quantity)
	    FROM `ContainerItems` ci
	    WHERE ci.ContainerId = (SELECT ContainerId FROM `ContainerItems` WHERE ItemId = {itemId})
	    AND ci.Deleted = 0
    )
    WHERE c.ContainerId = (SELECT ContainerId FROM `ContainerItems` WHERE ItemId = {itemId})";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query);
  }

  public async Task<int> DecrementItemQuantityAsync(int itemId, int decrementAmount)
  {
    var query = $@"UPDATE `ContainerItems`
    SET `Quantity` = `Quantity` - {decrementAmount}
    WHERE `ItemId` = {itemId}";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query);
  }

  public async Task<int> IncrementItemQuantityAsync(int itemId, int incrementAmount)
  {
    var query = $@"UPDATE `ContainerItems`
    SET `Quantity` = `Quantity` + {incrementAmount}
    WHERE `ItemId` = {itemId}";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query);
  }

  public async Task<int> SetItemQuantityAsync(int itemId, int quantity)
  {
    var query = $@"UPDATE `ContainerItems`
    SET `Quantity` = {quantity}
    WHERE `ItemId` = {itemId}";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query);
  }
}