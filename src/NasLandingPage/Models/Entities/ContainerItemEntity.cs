namespace NasLandingPage.Models.Entities;

public class ContainerItemEntity
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
}
