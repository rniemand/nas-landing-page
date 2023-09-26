namespace NasLandingPage.Models.Dto;

public class HomeChoreDto
{
  public int ChoreId { get; set; }
  public int RoomId { get; set; }
  public int CompletedCount { get; set; }
  public int Priority { get; set; }
  public int ChorePoints { get; set; }
  public DateTimeOffset DateAddedUtc { get; set; } = DateTimeOffset.MinValue;
  public DateTimeOffset? DateDeletedUtc { get; set; }
  public DateTimeOffset? DateDisabledUtc { get; set; }
  public DateTimeOffset? DateLastCompletedUtc { get; set; }
  public DateTimeOffset DateScheduledUtc { get; set; } = DateTimeOffset.MinValue;
  public string ChoreFrequency { get; set; } = string.Empty;
  public string ChoreName { get; set; } = null!;
  public string ChoreDescription { get; set; } = string.Empty;
}
