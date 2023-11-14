namespace NasLandingPage.Models.Dto;

public class ShoppingListItemDto
{
  public long ItemId { get; set; }
  public int HomeId { get; set; }
  public int AddedByUserId { get; set; }
  public int Quantity { get; set; } = 1;
  public DateOnly DateAdded { get; set; } = DateOnly.MinValue;
  public DateOnly? DateDeleted { get; set; }
  public DateOnly? DatePurchased { get; set; }
  public decimal? LastKnownPrice { get; set; }
  public string StoreName { get; set; } = null!;
  public string? Category { get; set; }
  public string ItemName { get; set; } = null!;
}
