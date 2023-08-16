using Dapper;
using NasLandingPage.Models.Entities;

namespace NasLandingPage.Repos;

public interface IGitHubRepoRepo
{
  Task<int> AddRepoAsync(GitHubRepoEntity entity);
  Task<int> UpdateRepoAsync(GitHubRepoEntity entity);
  Task<List<GitHubRepoEntity>> GetReposAsync();
  Task<GitHubRepoEntity> GetByIdAsync(long repoId);
}

public class GitHubRepoRepo : IGitHubRepoRepo
{
  private readonly IConnectionHelper _connectionHelper;
  public const string TableName = "GitHubRepos";

  public GitHubRepoRepo(IConnectionHelper connectionHelper)
  {
    _connectionHelper = connectionHelper;
  }

  public async Task<int> AddRepoAsync(GitHubRepoEntity entity)
  {
    const string query = $@"INSERT INTO {TableName}
	    (
		    `RepoID`,`RepoSize`,`IsTemplate`,`IsPrivate`,`IsFork`,`HasIssues`,
		    `HasWiki`,`HasDownloads`,`HasPages`,`IsArchived`,`ForksCount`,
		    `StargazersCount`,`OpenIssuesCount`,`SubscribersCount`,
		    `PushedAt`,`CreatedAt`,`UpdatedAt`,`License`,`DefaultBranch`,`GitUrl`,
		    `SshUrl`,`Name`,`FullName`,`Description`
	    )
    VALUES
	    (
		    @RepoID,@RepoSize,@IsTemplate,@IsPrivate,@IsFork,@HasIssues,
		    @HasWiki,@HasDownloads,@HasPages,@IsArchived,@ForksCount,
		    @StargazersCount,@OpenIssuesCount,@SubscribersCount,
		    @PushedAt,@CreatedAt,@UpdatedAt,@License,@DefaultBranch,@GitUrl,
		    @SshUrl,@Name,@FullName,@Description
	    )";

    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, entity);
  }

  public async Task<int> UpdateRepoAsync(GitHubRepoEntity entity)
  {
    const string query = $@"UPDATE {TableName}
    SET
      `RepoSize` = @RepoSize,
      `IsTemplate` = @IsTemplate,
      `IsPrivate` = @IsPrivate,
      `HasIssues` = @HasIssues,
      `HasWiki` = @HasWiki,
      `HasDownloads` = @HasDownloads,
      `HasPages` = @HasPages,
      `IsArchived` = @IsArchived,
      `ForksCount` = @ForksCount,
      `StargazersCount` = @StargazersCount,
      `OpenIssuesCount` = @OpenIssuesCount,
      `SubscribersCount` = @SubscribersCount,
      `UpdatedAt` = @UpdatedAt,
      `License` = @License,
      `DefaultBranch` = @DefaultBranch,
      `GitUrl` = @GitUrl,
      `SshUrl` = @SshUrl,
      `Name` = @Name,
      `FullName` = @FullName,
      `Description` = @Description
    WHERE `RepoID` = @RepoID";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, entity);
  }

  public async Task<List<GitHubRepoEntity>> GetReposAsync()
  {
    const string query = $@"SELECT
      `RepoID`,`RepoSize`,`IsTemplate`,`IsPrivate`,`IsFork`,`HasIssues`,
		  `HasWiki`,`HasDownloads`,`HasPages`,`IsArchived`,`ForksCount`,
		  `StargazersCount`,`OpenIssuesCount`,`SubscribersCount`,
		  `PushedAt`,`CreatedAt`,`UpdatedAt`,`License`,`DefaultBranch`,`GitUrl`,
		  `SshUrl`,`Name`,`FullName`,`Description`
    FROM {TableName}";
    await using var connection = _connectionHelper.GetCoreConnection();
    return (await connection.QueryAsync<GitHubRepoEntity>(query)).AsList();
  }

  public async Task<GitHubRepoEntity> GetByIdAsync(long repoId)
  {
    const string query = $@"SELECT
      `RepoID`,`RepoSize`,`IsTemplate`,`IsPrivate`,`IsFork`,`HasIssues`,
		  `HasWiki`,`HasDownloads`,`HasPages`,`IsArchived`,`ForksCount`,
		  `StargazersCount`,`OpenIssuesCount`,`SubscribersCount`,
		  `PushedAt`,`CreatedAt`,`UpdatedAt`,`License`,`DefaultBranch`,`GitUrl`,
		  `SshUrl`,`Name`,`FullName`,`Description`
    FROM `{TableName}`
    WHERE `RepoID` = @RepoID";
    var connection = _connectionHelper.GetCoreConnection();
    return await connection.QuerySingleAsync<GitHubRepoEntity>(query, new { RepoID = repoId });
  }
}
