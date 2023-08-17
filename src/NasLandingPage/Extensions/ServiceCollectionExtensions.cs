using NasLandingPage.Factories;
using NasLandingPage.Helpers;
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
      // Abstractions
      .AddSingleton<IFileAbstraction, FileAbstraction>()
      .AddSingleton<IPathAbstraction, PathAbstraction>()
      // Database
      .AddSingleton<IConnectionHelper, ConnectionHelper>()
      .AddSingleton<IUserLinksRepo, UserUserLinksRepo>()
      .AddSingleton<IUserTasksRepo, UserTasksRepo>()
      .AddSingleton<IGitHubRepoRepo, GitHubRepoRepo>()
      .AddSingleton<IUserRepo, UserRepo>()
      .AddSingleton<IGamesRepo, GamesRepo>()
      .AddSingleton<IGameLocationRepo, GameLocationRepo>()
      .AddSingleton<IGameImageRepo, GameImageRepo>()
      .AddSingleton<IGameReceiptRepo, GameReceiptRepo>()
      .AddSingleton<IGamePlatformRepo, GamePlatformRepo>()
      .AddSingleton<IOAuthRepo, OAuthRepo>()
      // Factories
      .AddSingleton<IGitHubClientFactory, GitHubClientFactory>()
      // Helpers
      .AddSingleton<IJsonHelper, JsonHelper>()
      .AddSingleton<IAppPathHelper, AppPathHelper>()
      // Services
      .AddSingleton<IUserLinkService, UserLinkService>()
      .AddSingleton<IUserTaskService, UserTaskService>()
      .AddSingleton<IGitHubService, GitHubService>()
      .AddSingleton<IAuthService, AuthService>()
      .AddSingleton<IGamesService, GamesService>()
      .AddSingleton<IImageService, ImageService>()
      .AddSingleton<IGameLocationService, GameLocationService>()
      .AddSingleton<IGamePlatformService, GamePlatformService>()
      .AddSingleton<IGameReceiptService, GameReceiptService>()
      .AddSingleton<IFitBitService, FitBitService>();
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
