namespace NasLandingPage.Models.Entities;

public class UserTaskEntity
{
  public int TaskID { get; set; }
  public int UserID { get; set; }
  public bool Deleted { get; set; }
  public bool Completed { get; set; }
  public int TaskPriority { get; set; }
  public DateTimeOffset DateAddedUtc { get; set; } = DateTimeOffset.MinValue;
  public DateTimeOffset DateCompletedUtc { get; set; } = DateTimeOffset.MinValue;
  public string TaskName { get; set; } = null!;
  public string TaskCategory { get; set; } = null!;
  public string TaskSubCategory { get; set; } = null!;
  public string TaskDescription { get; set; } = null!;
}
