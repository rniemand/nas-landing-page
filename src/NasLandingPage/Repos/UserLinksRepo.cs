using Dapper;
using NasLandingPage.Models.Entities;

namespace NasLandingPage.Repos;

public interface IUserLinksRepo
{
  Task<List<UserLinkEntity>> GetAllAsync();
}

public class UserUserLinksRepo : IUserLinksRepo
{
  public const string TableName = "UserLinks";
  private readonly IConnectionHelper _connectionHelper;

  public UserUserLinksRepo(IConnectionHelper connectionHelper)
  {
    _connectionHelper = connectionHelper;
  }

  public async Task<List<UserLinkEntity>> GetAllAsync()
  {
    const string query = $@"SELECT *
    FROM `{TableName}`
    WHERE
	    `Deleted` = 0";
    await using var connection = _connectionHelper.GetCoreConnection();
    return (await connection.QueryAsync<UserLinkEntity>(query)).AsList();
  }
}
