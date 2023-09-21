namespace NasLandingPage.Models.Entities;

public class UserTaskEntity
{
  public int TaskID { get; set; }
  public int UserID { get; set; }
  public int TaskPriority { get; set; }
  public DateTimeOffset DateAddedUtc { get; set; } = DateTimeOffset.MinValue;
  public DateTimeOffset? DateCompletedUtc { get; set; }
  public DateTimeOffset? DateDeletedUtc { get; set; }
  public string TaskName { get; set; } = null!;
  public string TaskCategory { get; set; } = null!;
  public string TaskSubCategory { get; set; } = null!;
  public string TaskDescription { get; set; } = null!;
}
