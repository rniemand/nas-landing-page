using Microsoft.Extensions.DependencyInjection;
using NasLandingPage.Common.Models.Responses;
using NasLandingPage.Common.Providers;

namespace NasLandingPage.Common.Services;

public interface IUserLinkService
{
  Task<List<UserLink>> GetAll();
}

public class UserLinkService : IUserLinkService
{
  private readonly IUserLinkProvider _linkProvider;

  public UserLinkService(IServiceProvider serviceProvider)
  {
    // TODO: [UserLinkService.UserLinkService] (TESTS) Add tests
    _linkProvider = serviceProvider.GetRequiredService<IUserLinkProvider>();
  }

  public async Task<List<UserLink>> GetAll()
  {
    // TODO: [UserLinkService.GetAll] (TESTS) Add tests
    await Task.CompletedTask;

    return new List<UserLink>();
  }
}
