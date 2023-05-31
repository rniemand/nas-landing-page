using Dapper;
using NasLandingPage.Models.Entities;

namespace NasLandingPage.Repos;

public interface IUserLinksRepo
{
  Task<List<UserLinkEntity>> GetAllAsync();
  Task<int> RecordFollowAsync(int linkId);
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

  public async Task<int> RecordFollowAsync(int linkId)
  {
    const string query = $@"UPDATE `{TableName}`
    SET
	    `FollowCount` = `FollowCount` + 1,
	    `DateLastFollowedUtc` = utc_timestamp(6)
    WHERE `LinkId` = @LinkId";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, new { LinkId = linkId });
  }
}
