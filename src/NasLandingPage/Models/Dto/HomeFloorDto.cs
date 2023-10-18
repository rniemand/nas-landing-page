namespace NasLandingPage.Models.Dto;

public class HomeFloorDto
{
  public int FloorId { get; set; }
  public int HomeId { get; set; }
  public DateTimeOffset DateAddedUtc { get; set; } = DateTimeOffset.MinValue;
  public DateTimeOffset? DateDeletedUtc { get; set; }
  public string FloorName { get; set; } = null!;
}
