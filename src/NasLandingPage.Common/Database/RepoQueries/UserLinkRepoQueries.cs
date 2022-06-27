namespace NasLandingPage.Common.Database;

public interface IUserLinkRepoQueries
{
  string GetAll();
  string GetCategories();
  string UpdateFollowed();
}

public class UserLinkRepoQueries : IUserLinkRepoQueries
{
  public string GetAll()
  {
    return @"SELECT *
    FROM `UserLinks` ul
    WHERE ul.`Deleted` = 0
    ORDER BY ul.`LinkCategory`, ul.`LinkOrder` ASC";
  }

  public string GetCategories()
  {
    return @"SELECT DISTINCT(ul.LinkCategory) AS `Value`
    FROM `UserLinks` ul
    WHERE ul.`Deleted` = 0
    ORDER BY ul.`LinkCategory` ASC";
  }

  public string UpdateFollowed()
  {
    return @"UPDATE `UserLinks` ul
    SET
	    ul.`FollowCount` = `FollowCount` + 1,
	    ul.`DateLastFollowedUtc` = utc_timestamp(6)
    WHERE
	    ul.`LinkId` = @LinkId";
  }
}
