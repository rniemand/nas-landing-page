using Dapper;
using NasLandingPage.Models.Entities;
using Octokit;

namespace NasLandingPage.Repos;

public interface IContainerRepo
{
  Task<int> AddContainerAsync(ContainerEntity container);
  Task<List<ContainerEntity>> GetAllContainersAsync();
}

public class ContainerRepo : IContainerRepo
{
  private readonly IConnectionHelper _connectionHelper;

  public ContainerRepo(IConnectionHelper connectionHelper)
  {
    _connectionHelper = connectionHelper;
  }

  public async Task<int> AddContainerAsync(ContainerEntity container)
  {
    const string query = @"INSERT INTO `Containers`
	    (`ShelfNumber`, `ShelfLevel`, `ShelfRow`, `ShelfRowPosition`, `ItemCount`, `ContainerLabel`, `ContainerName`, `Notes`)
    VALUES
	    (@ShelfNumber, @ShelfLevel, @ShelfRow, @ShelfRowPosition, @ItemCount, @ContainerLabel, @ContainerName, @Notes)";
    await using var connection = _connectionHelper.GetCoreConnection();
    return await connection.ExecuteAsync(query, container);
  }

  public async Task<List<ContainerEntity>> GetAllContainersAsync()
  {
    var query = @"SELECT
	    con.`ContainerId`,
	    con.`Deleted`,
	    con.`ShelfNumber`,
	    con.`ShelfLevel`,
	    con.`ShelfRow`,
	    con.`ShelfRowPosition`,
	    con.`ItemCount`,
	    con.`DateAddedUtc`,
	    con.`DateUpdatedUtc`,
	    con.`ContainerLabel`,
	    con.`ContainerName`,
	    con.`Notes`
    FROM `Containers` con
    WHERE con.`Deleted` = 0
    ORDER BY con.`ShelfNumber`, con.`ShelfLevel`, con.`ShelfRow`, con.`ShelfRowPosition`";
    await using var connection = _connectionHelper.GetCoreConnection();
    return (await connection.QueryAsync<ContainerEntity>(query)).ToList();
  }
}
