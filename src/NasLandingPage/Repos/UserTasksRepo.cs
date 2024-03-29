using Dapper;
using NasLandingPage.Models;
using NasLandingPage.Models.Entities;
using NasLandingPage.Models.Requests;
using NLog.Filters;

namespace NasLandingPage.Repos;

public interface IUserTasksRepo
{
  Task<int> AddTaskAsync(UserTaskEntity task);
  Task<IEnumerable<string>> GetTaskCategoriesAsync(NlpUserContext userContext, string filter, bool includeCompletedEntries);
  Task<IEnumerable<string>> GetAllTaskCategoriesAsync(NlpUserContext userContext, bool includeCompletedEntries);
  Task<IEnumerable<string>> GetTaskSubCategoriesAsync(NlpUserContext userContext, string category, string filter, bool includeCompletedEntries);
  Task<IEnumerable<UserTaskEntity>> GetUserTasksAsync(NlpUserContext userContext, BasicSearchRequest request);
  Task<int> CompleteUserTaskAsync(NlpUserContext userContext, int taskId);
  Task<int> UpdateUserTaskAsync(UserTaskEntity task);
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

  public async Task<IEnumerable<string>> GetTaskCategoriesAsync(NlpUserContext userContext, string filter, bool includeCompletedEntries)
  {
    var query = @$"
    SELECT DISTINCT ut.TaskCategory
    FROM `UserTasks` ut
    WHERE ut.UserID = @UserID
	    AND ut.DateDeleted IS NULL
      {(includeCompletedEntries ? "" : "AND ut.DateCompleted IS NULL")}
      AND ut.TaskCategory LIKE '%{filter}%'
    ORDER BY ut.TaskCategory
    ";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QueryAsync<string>(query, new
    {
      UserID = userContext.UserId
    });
  }

  public async Task<IEnumerable<string>> GetAllTaskCategoriesAsync(NlpUserContext userContext, bool includeCompletedEntries)
  {
    var query = @$"
    SELECT DISTINCT ut.TaskCategory
    FROM `UserTasks` ut
    WHERE ut.UserID = @UserID
	    AND ut.DateDeleted IS NULL
      {(includeCompletedEntries ? "" : "AND ut.DateCompleted IS NULL")}
    ORDER BY ut.TaskCategory
    ";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QueryAsync<string>(query, new
    {
      UserID = userContext.UserId
    });
  }

  public async Task<IEnumerable<string>> GetTaskSubCategoriesAsync(NlpUserContext userContext, string category, string filter, bool includeCompletedEntries)
  {
    var query = @$"SELECT DISTINCT ut.TaskSubCategory
    FROM `UserTasks` ut
    WHERE ut.UserID = @UserID
	    AND ut.DateDeleted IS NULL
	    AND ut.TaskCategory = @TaskCategory
      AND ut.TaskSubCategory LIKE '%{filter}%'
      {(includeCompletedEntries ? "" : "AND ut.DateCompleted IS NULL")}
    ORDER BY ut.TaskSubCategory";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QueryAsync<string>(query, new
    {
      UserID = userContext.UserId,
      TaskCategory = category
    });
  }

  public async Task<IEnumerable<UserTaskEntity>> GetUserTasksAsync(NlpUserContext userContext, BasicSearchRequest request)
  {
    var query = @$"
    SELECT *
    FROM `UserTasks` ut
    WHERE
	    ut.UserID = @UserID
	    AND ut.DateDeleted IS NULL
	    AND ut.DateCompleted IS NULL
      {(string.IsNullOrWhiteSpace(request.Filter) ? "" : "AND ut.TaskCategory = @TaskCategory")}
      {(string.IsNullOrWhiteSpace(request.SubFilter) ? "" : "AND ut.TaskSubCategory = @TaskSubCategory")}
    ORDER BY ut.TaskPriority
    ";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QueryAsync<UserTaskEntity>(query, new
    {
      UserID = userContext.UserId,
      TaskCategory = request.Filter,
      TaskSubCategory = request.SubFilter
    });
  }

  public async Task<int> CompleteUserTaskAsync(NlpUserContext userContext, int taskId)
  {
    const string query = @"UPDATE `UserTasks`
    SET
      `DateCompleted` = utc_timestamp(6)
    WHERE
      `TaskID` = @TaskID
      AND `UserID` = @UserID";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, new
    {
      UserID = userContext.UserId,
      TaskID = taskId,
    });
  }

  public async Task<int> UpdateUserTaskAsync(UserTaskEntity task)
  {
    const string query = @"UPDATE `UserTasks`
    SET
      `TaskPriority` = @TaskPriority,
      `TaskName` = @TaskName,
      `TaskCategory` = @TaskCategory,
      `TaskSubCategory` = @TaskSubCategory,
      `TaskDescription` = @TaskDescription
    WHERE
      `TaskID` = @TaskID
      AND `UserID` = @UserID";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, task);
  }
}
