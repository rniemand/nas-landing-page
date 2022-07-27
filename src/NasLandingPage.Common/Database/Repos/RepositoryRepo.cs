using Rn.NetCore.DbCommon;

namespace NasLandingPage.Common.Database;

public interface IRepositoryRepo
{
}

public class RepositoryRepo : BaseRepo<RepositoryRepo>, IRepositoryRepo
{
  private readonly IRepositoryRepoQueries _repoQueries;

  public RepositoryRepo(IBaseRepoHelper baseRepoHelper, IRepositoryRepoQueries repoQueries)
    : base(baseRepoHelper)
  {
    _repoQueries = repoQueries;
  }
}
