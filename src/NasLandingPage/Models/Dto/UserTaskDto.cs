using NasLandingPage.Models.Entities;

namespace NasLandingPage.Models.Dto;

public class UserTaskDto
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

  public static UserTaskDto FromEntity(UserTaskEntity entity) => new()
  {
    Completed = entity.Completed,
    TaskPriority = entity.TaskPriority,
    DateAddedUtc = entity.DateAddedUtc,
    DateCompletedUtc = entity.DateCompletedUtc,
    TaskName = entity.TaskName,
    TaskCategory = entity.TaskCategory,
    TaskDescription = entity.TaskDescription,
    Deleted = entity.Deleted,
    TaskID = entity.TaskID,
  };

  public UserTaskEntity AsEntity() => new()
  {
    Completed = Completed,
    TaskPriority = TaskPriority,
    DateAddedUtc = DateAddedUtc,
    DateCompletedUtc = DateCompletedUtc,
    TaskName = TaskName,
    TaskCategory = TaskCategory,
    TaskDescription = TaskDescription,
    Deleted = Deleted,
    TaskID = TaskID,
  };
}
