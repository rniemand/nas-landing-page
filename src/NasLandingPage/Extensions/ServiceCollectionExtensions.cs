using NasLandingPage.Repos;
using NasLandingPage.Services;

namespace NasLandingPage.Extensions;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddNasLandingPage(this IServiceCollection services)
  {
    return services
      // Database
      .AddSingleton<IConnectionHelper, ConnectionHelper>()
      .AddSingleton<IUserLinksRepo, UserUserLinksRepo>()
      // Services
      .AddSingleton<IUserLinkService, UserLinkService>();
  }
}
