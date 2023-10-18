namespace NasLandingPage.Plugins.Chores;

public class HomeChoreDto
{
  public int ChoreId { get; set; }
  public int RoomId { get; set; }
  public int CompletedCount { get; set; }
  public string Priority { get; set; } = "low";
  public int ChorePoints { get; set; }
  public DateOnly DateAdded { get; set; } = DateOnly.MinValue;
  public DateOnly? DateDeleted { get; set; }
  public DateOnly? DateDisabled { get; set; }
  public DateOnly? DateLastCompleted { get; set; }
  public DateOnly DateScheduled { get; set; } = DateOnly.MinValue;
  public string IntervalModifier { get; set; } = string.Empty;
  public string Interval { get; set; } = string.Empty;
  public string ChoreName { get; set; } = null!;
  public string ChoreDescription { get; set; } = string.Empty;
  // Additional properties
  public string? RoomName { get; set; }
  public string? FloorName { get; set; }
}
