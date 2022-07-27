using Rn.NetCore.DbCommon;

namespace NasLandingPage.Common.Database;

public interface ISonarQubeInfoRepo
{
}

public class SonarQubeInfoRepo : BaseRepo<SonarQubeInfoRepo>, ISonarQubeInfoRepo
{
  private readonly ISonarQubeInfoRepoQueries _repoQueries;

  public SonarQubeInfoRepo(IBaseRepoHelper baseRepoHelper, ISonarQubeInfoRepoQueries repoQueries)
    : base(baseRepoHelper)
  {
    _repoQueries = repoQueries;
  }
}
