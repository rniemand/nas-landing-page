using NasLandingPage.Models;
using NasLandingPage.Repos;
using NasLandingPage.Services;

namespace NasLandingPage.Extensions;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddNasLandingPage(this IServiceCollection services, IConfiguration configuration)
  {
    return services
      // Configuration
      .AddSingleton(BindAppConfig(configuration))
      // Database
      .AddSingleton<IConnectionHelper, ConnectionHelper>()
      .AddSingleton<IUserLinksRepo, UserUserLinksRepo>()
      // Services
      .AddSingleton<IUserLinkService, UserLinkService>();
  }

  private static AppConfig BindAppConfig(IConfiguration configuration)
  {
    var section = configuration.GetSection("NasLandingPage");
    if (!section.Exists())
      throw new Exception("Unable to find 'NasLandingPage' configuration");

    var config = new AppConfig();
    section.Bind(config);
    return config;
  }
}
