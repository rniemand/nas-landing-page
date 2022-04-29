using Microsoft.Extensions.DependencyInjection;
using NasLandingPage.Common.Providers;

namespace NasLandingPage.Common.Services;

public interface IUserLinkService
{
}

public class UserLinkService : IUserLinkService
{
  private readonly IUserLinkProvider _linkProvider;

  public UserLinkService(IServiceProvider serviceProvider)
  {
    // TODO: [UserLinkService.UserLinkService] (TESTS) Add tests
    _linkProvider = serviceProvider.GetRequiredService<IUserLinkProvider>();
  }
}
