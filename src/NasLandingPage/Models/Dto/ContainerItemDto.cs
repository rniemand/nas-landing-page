using NasLandingPage.Models.Entities;

namespace NasLandingPage.Models.Dto;

public class ContainerItemDto
{
  public int ItemId { get; set; }
  public int ContainerId { get; set; }
  public int Quantity { get; set; }
  public int OrderMoreMinQty { get; set; } = 0;
  public bool OrderMore { get; set; }
  public bool OrderPlaced { get; set; }
  public bool AutoFlagOrderMore { get; set; }
  public DateTimeOffset DateAddedUtc { get; set; } = DateTimeOffset.Now;
  public DateTimeOffset DateUpdatedUtc { get; set; } = DateTimeOffset.Now;
  public string Category { get; set; } = string.Empty;
  public string SubCategory { get; set; } = string.Empty;
  public string InventoryName { get; set; } = string.Empty;
  public string OrderUrl { get; set; } = string.Empty;

  public static ContainerItemDto FromEntity(ContainerItemEntity entity) => new()
  {
    DateAddedUtc = entity.DateAddedUtc,
    DateUpdatedUtc = entity.DateUpdatedUtc,
    ContainerId = entity.ContainerId,
    Quantity = entity.Quantity,
    Category = entity.Category,
    SubCategory = entity.SubCategory,
    InventoryName = entity.InventoryName,
    OrderUrl = entity.OrderUrl,
    OrderMore = entity.OrderMore,
    OrderPlaced = entity.OrderPlaced,
    ItemId = entity.ItemId,
    AutoFlagOrderMore = entity.AutoFlagOrderMore,
    OrderMoreMinQty = entity.OrderMoreMinQty,
  };

  public ContainerItemEntity ToEntity() => new()
  {
    ContainerId = ContainerId,
    DateAddedUtc = DateAddedUtc,
    DateUpdatedUtc = DateUpdatedUtc,
    Quantity = Quantity,
    OrderMoreMinQty = OrderMoreMinQty,
    OrderPlaced = OrderPlaced,
    AutoFlagOrderMore = AutoFlagOrderMore,
    Category = Category,
    SubCategory = SubCategory,
    InventoryName = InventoryName,
    OrderUrl = OrderUrl,
    ItemId = ItemId,
    OrderMore = OrderMore,
  };
}
