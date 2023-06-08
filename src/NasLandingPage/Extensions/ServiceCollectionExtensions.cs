using NasLandingPage.Models;
using NasLandingPage.Repos;
using NasLandingPage.Services;
using RnCore.Abstractions;
using RnCore.Logging;

namespace NasLandingPage.Extensions;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddNasLandingPage(this IServiceCollection services, IConfiguration configuration)
  {
    return services
      // Configuration & Logging
      .AddSingleton(BindAppConfig(configuration))
      .AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>))
      // Database
      .AddSingleton<IConnectionHelper, ConnectionHelper>()
      .AddSingleton<IUserLinksRepo, UserUserLinksRepo>()
      .AddSingleton<IUserTasksRepo, UserTasksRepo>()
      .AddSingleton<IGitHubRepoRepo, GitHubRepoRepo>()
      // Helpers
      .AddSingleton<IJsonHelper, JsonHelper>()
      // Services
      .AddSingleton<IUserLinkService, UserLinkService>()
      .AddSingleton<IUserTaskService, UserTaskService>()
      .AddSingleton<IGitHubService, GitHubService>();
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
