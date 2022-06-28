using Rn.NetCore.DbCommon;

namespace NasLandingPage.Common.Database;

public interface IProjectRepo
{
}

public class ProjectRepo : BaseRepo<ProjectRepo>, IProjectRepo
{
  private readonly IProjectRepoQueries _repoQueries;

  public ProjectRepo(IBaseRepoHelper baseRepoHelper, IProjectRepoQueries repoQueries)
    : base(baseRepoHelper)
  {
    _repoQueries = repoQueries;
  }
}
