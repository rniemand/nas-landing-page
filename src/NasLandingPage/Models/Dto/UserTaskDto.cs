using NasLandingPage.Models.Entities;

namespace NasLandingPage.Models.Dto;

public class UserTaskDto
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

  public static UserTaskDto FromEntity(UserTaskEntity entity) => new()
  {
    UserID = entity.UserID,
    TaskID = entity.TaskID,
    Completed = entity.Completed,
    TaskPriority = entity.TaskPriority,
    DateAddedUtc = entity.DateAddedUtc,
    DateCompletedUtc = entity.DateCompletedUtc,
    TaskName = entity.TaskName,
    TaskCategory = entity.TaskCategory,
    TaskDescription = entity.TaskDescription,
    Deleted = entity.Deleted,
    TaskSubCategory = entity.TaskSubCategory,
  };

  public UserTaskEntity ToEntity() => new()
  {
    Deleted = Deleted,
    TaskID = TaskID,
    TaskPriority = TaskPriority,
    DateAddedUtc = DateAddedUtc,
    DateCompletedUtc = DateCompletedUtc,
    TaskName = TaskName,
    TaskCategory = TaskCategory,
    TaskDescription = TaskDescription,
    Completed = Completed,
    TaskSubCategory = TaskSubCategory,
    UserID = UserID,
  };
}
