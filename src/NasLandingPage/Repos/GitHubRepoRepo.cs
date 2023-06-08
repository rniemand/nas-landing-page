using Dapper;
using NasLandingPage.Models.Entities;

namespace NasLandingPage.Repos;

public interface IGitHubRepoRepo
{
  Task<int> AddRepoAsync(GitHubRepoEntity entity);
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
}
