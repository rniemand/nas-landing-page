using NasLandingPage.Models.Entities;

namespace NasLandingPage.Models.Dto;

public class UserTaskDto
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

  public static UserTaskDto FromEntity(UserTaskEntity entity) => new()
  {
    UserID = entity.UserID,
    TaskID = entity.TaskID,
    TaskPriority = entity.TaskPriority,
    DateAddedUtc = entity.DateAddedUtc,
    DateCompletedUtc = entity.DateCompletedUtc,
    TaskName = entity.TaskName,
    TaskCategory = entity.TaskCategory,
    TaskDescription = entity.TaskDescription,
    TaskSubCategory = entity.TaskSubCategory,
    DateDeletedUtc = entity.DateDeletedUtc,
  };

  public UserTaskEntity ToEntity() => new()
  {
    TaskID = TaskID,
    TaskPriority = TaskPriority,
    DateAddedUtc = DateAddedUtc,
    DateCompletedUtc = DateCompletedUtc,
    TaskName = TaskName,
    TaskCategory = TaskCategory,
    TaskDescription = TaskDescription,
    TaskSubCategory = TaskSubCategory,
    UserID = UserID,
    DateDeletedUtc = DateDeletedUtc,
  };
}
