using Dapper;
using NasLandingPage.Models.Entities;

namespace NasLandingPage.Repos;
public interface IUserRepo
{
  Task<UserEntity?> GetByEmailAsync(string email);
  Task<bool> UpdatePasswordHash(UserEntity user);
  Task<IEnumerable<UserEntity>> GetAllUsersAsync();
}

public class UserRepo : IUserRepo
{
  public const string TableName = "Users";
  private readonly IConnectionHelper _connectionHelper;

  public UserRepo(IConnectionHelper connectionHelper)
  {
    _connectionHelper = connectionHelper;
  }

  public async Task<UserEntity?> GetByEmailAsync(string email)
  {
    const string query = $@"SELECT
      `UserID`,
      `Email`,
      `PasswordHash`,
      `CanSetPass`
    FROM {TableName}
    WHERE `Email` = @Email";

    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QuerySingleOrDefaultAsync<UserEntity>(query, new { Email = email });
  }

  public async Task<bool> UpdatePasswordHash(UserEntity user)
  {
    const string query = $@"UPDATE {TableName}
    SET
      `PasswordHash` = @PasswordHash,
      `CanSetPass` = 0
    WHERE
      `UserID` = @UserID";
    await using var connection = _connectionHelper.GetCoreConnection();
    return (await connection.ExecuteAsync(query, user)) == 1;
  }

  public async Task<IEnumerable<UserEntity>> GetAllUsersAsync()
  {
    const string query = @"
    SELECT *
    FROM `Users` u
    WHERE u.`DateDeleted` IS NULL
    ORDER BY u.FirstName, u.Surname";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.QueryAsync<UserEntity>(query);
  }
}
