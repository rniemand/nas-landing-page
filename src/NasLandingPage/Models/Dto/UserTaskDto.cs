using NasLandingPage.Models.Entities;

namespace NasLandingPage.Models.Dto;

public class UserTaskDto
{
  public int TaskID { get; set; }
  public int UserID { get; set; }
  public int TaskPriority { get; set; }
  public DateTimeOffset DateAdded { get; set; } = DateTimeOffset.MinValue;
  public DateTimeOffset? DateCompleted { get; set; }
  public DateTimeOffset? DateDeleted { get; set; }
  public string TaskName { get; set; } = null!;
  public string TaskCategory { get; set; } = null!;
  public string TaskSubCategory { get; set; } = null!;
  public string TaskDescription { get; set; } = null!;

  public static UserTaskDto FromEntity(UserTaskEntity entity) => new()
  {
    UserID = entity.UserID,
    TaskID = entity.TaskID,
    TaskPriority = entity.TaskPriority,
    DateAdded = entity.DateAdded,
    DateCompleted = entity.DateCompleted,
    TaskName = entity.TaskName,
    TaskCategory = entity.TaskCategory,
    TaskDescription = entity.TaskDescription,
    TaskSubCategory = entity.TaskSubCategory,
    DateDeleted = entity.DateDeleted,
  };

  public UserTaskEntity ToEntity() => new()
  {
    TaskID = TaskID,
    TaskPriority = TaskPriority,
    DateAdded = DateAdded,
    DateCompleted = DateCompleted,
    TaskName = TaskName,
    TaskCategory = TaskCategory,
    TaskDescription = TaskDescription,
    TaskSubCategory = TaskSubCategory,
    UserID = UserID,
    DateDeleted = DateDeleted,
  };
}
