namespace NasLandingPage.Common.Database;

public interface IUserLinkRepoQueries
{
  string GetAll();
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
}
