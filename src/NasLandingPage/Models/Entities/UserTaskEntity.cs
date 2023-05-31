namespace NasLandingPage.Models.Entities;

public class UserTaskEntity
{
  public int TaskID { get; set; }
  public bool Deleted { get; set; }
  public bool Completed { get; set; }
  public int TaskPriority { get; set; }
  public DateTime DateAddedUtc { get; set; }
  public DateTime DateCompletedUtc { get; set; }
  public string TaskName { get; set; } = string.Empty;
  public string TaskCategory { get; set; } = string.Empty;
  public string TaskDescription { get; set; } = string.Empty;
}
