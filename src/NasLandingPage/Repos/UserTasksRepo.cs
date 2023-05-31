using Dapper;
using NasLandingPage.Models.Entities;

namespace NasLandingPage.Repos;

public interface IUserTasksRepo
{
  Task<List<UserTaskEntity>> GetAllAsync();
  Task<int> AddAsync(UserTaskEntity entity);
}

public class UserTasksRepo : IUserTasksRepo
{
  private readonly IConnectionHelper _connectionHelper;
  public const string TableName = "UserTasks";

  public UserTasksRepo(IConnectionHelper connectionHelper)
  {
    _connectionHelper = connectionHelper;
  }

  public async Task<List<UserTaskEntity>> GetAllAsync()
  {
    const string query = $@"SELECT *
    FROM `{TableName}` t
    WHERE
	    t.`Deleted` = 0 AND
	    t.`Completed` = 0";
    await using var connection = _connectionHelper.GetCoreConnection();
    return (await connection.QueryAsync<UserTaskEntity>(query)).AsList();
  }

  public async Task<int> AddAsync(UserTaskEntity entity)
  {
    const string query = @"INSERT INTO `UserTasks`
	    (`TaskPriority`, `TaskName`, `TaskCategory`, `TaskDescription`)
    VALUES
	    (@TaskPriority, @TaskName, @TaskCategory, @TaskDescription)";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, entity);
  }
}
