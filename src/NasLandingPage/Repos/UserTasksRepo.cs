using Dapper;
using NasLandingPage.Models;
using NasLandingPage.Models.Entities;

namespace NasLandingPage.Repos;

public interface IUserTasksRepo
{
  Task<int> AddTaskAsync(UserTaskEntity task);
  Task<IEnumerable<string>> GetTaskCategoriesAsync(NlpUserContext userContext);
  Task<IEnumerable<string>> GetTaskSubCategoriesAsync(NlpUserContext userContext, string category);
  Task<IEnumerable<UserTaskEntity>> GetUserTasksAsync(NlpUserContext userContext);
}

internal class UserTasksRepo : IUserTasksRepo
{
  private readonly IConnectionHelper _connectionHelper;

  public UserTasksRepo(IConnectionHelper connectionHelper)
  {
    _connectionHelper = connectionHelper;
  }

  public async Task<int> AddTaskAsync(UserTaskEntity task)
  {
    const string query = @"INSERT INTO `UserTasks`
	    (`UserID`,`TaskPriority`,`TaskName`,`TaskCategory`,`TaskSubCategory`,`TaskDescription`)
    VALUES
	    (@UserID,@TaskPriority,@TaskName,@TaskCategory,@TaskSubCategory,@TaskDescription)";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, task);
  }

  public async Task<IEnumerable<string>> GetTaskCategoriesAsync(NlpUserContext userContext)
  {
    const string query = @"SELECT DISTINCT ut.TaskCategory
    FROM `UserTasks` ut
    WHERE ut.UserID = @UserID
	    AND ut.DateDeletedUtc IS NULL
    ORDER BY ut.TaskCategory";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QueryAsync<string>(query, new { UserID = userContext.UserId });
  }

  public async Task<IEnumerable<string>> GetTaskSubCategoriesAsync(NlpUserContext userContext, string category)
  {
    const string query = @"SELECT DISTINCT ut.TaskSubCategory
    FROM `UserTasks` ut
    WHERE ut.UserID = @UserID
	    AND ut.DateDeletedUtc IS NULL
	    AND ut.TaskCategory = @TaskCategory
    ORDER BY ut.TaskSubCategory";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QueryAsync<string>(query, new
    {
      UserID = userContext.UserId,
      TaskCategory = category
    });
  }

  public async Task<IEnumerable<UserTaskEntity>> GetUserTasksAsync(NlpUserContext userContext)
  {
    const string query = @"SELECT *
    FROM `UserTasks` ut
    WHERE
	    ut.UserID = @UserID
	    AND ut.DateDeletedUtc IS NULL
	    AND ut.DateCompletedUtc IS NULL
    ORDER BY ut.TaskPriority";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QueryAsync<UserTaskEntity>(query, new { UserID = userContext.UserId });
  }
}
