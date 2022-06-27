using Rn.NetCore.DbCommon;

namespace NasLandingPage.Common.Database;

public interface IProjectRepo
{
}

public class ProjectRepo : BaseRepo<ProjectRepo>, IProjectRepo
{
  private readonly IProjectRepoQueries _queries;

  public ProjectRepo(IBaseRepoHelper baseRepoHelper, IProjectRepoQueries queries)
    : base(baseRepoHelper)
  {
    _queries = queries;
  }
}
