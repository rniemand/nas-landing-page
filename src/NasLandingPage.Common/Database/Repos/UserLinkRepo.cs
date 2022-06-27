using NasLandingPage.Common.Database.Entities;
using Rn.NetCore.DbCommon;

namespace NasLandingPage.Common.Database;

public interface IUserLinkRepo
{
  Task<List<UserLinkEntity>> GetAllAsync();
}

public class UserLinkRepo : BaseRepo<UserLinkRepo>, IUserLinkRepo
{
  private readonly IUserLinkRepoQueries _queries;

  public UserLinkRepo(IBaseRepoHelper baseRepoHelper, IUserLinkRepoQueries queries)
    : base(baseRepoHelper)
  {
    _queries = queries;
  }

  public async Task<List<UserLinkEntity>> GetAllAsync() =>
    await GetList<UserLinkEntity>(nameof(GetAllAsync), _queries.GetAll());
}
