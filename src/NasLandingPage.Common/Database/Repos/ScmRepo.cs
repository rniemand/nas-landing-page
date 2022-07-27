using Rn.NetCore.DbCommon;

namespace NasLandingPage.Common.Database;

public interface IScmRepo
{
}

public class ScmRepo : BaseRepo<ScmRepo>, IScmRepo
{
  private readonly IScmRepoQueries _repoQueries;

  public ScmRepo(IBaseRepoHelper baseRepoHelper, IScmRepoQueries repoQueries)
    : base(baseRepoHelper)
  {
    _repoQueries = repoQueries;
  }
}
