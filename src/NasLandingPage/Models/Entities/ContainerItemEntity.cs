namespace NasLandingPage.Models.Entities;

public class ContainerItemEntity : ContainerEntity
{
  public int ItemId { get; set; }
  public int Quantity { get; set; }
  public int OrderMoreMinQty { get; set; } = 0;
  public bool OrderMore { get; set; }
  public bool OrderPlaced { get; set; }
  public bool AutoFlagOrderMore { get; set; }
  public string Category { get; set; } = string.Empty;
  public string SubCategory { get; set; } = string.Empty;
  public string InventoryName { get; set; } = string.Empty;
  public string OrderUrl { get; set; } = string.Empty;
}
