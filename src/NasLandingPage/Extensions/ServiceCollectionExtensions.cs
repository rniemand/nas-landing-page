using NasLandingPage.Exceptions;
using NasLandingPage.Models;
using NasLandingPage.Repos;
using NasLandingPage.Services;
using RnCore.Abstractions;
using RnCore.Logging;

namespace NasLandingPage.Extensions;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddNasLandingPage(this IServiceCollection services, IConfiguration configuration) =>
    services
      .AddSingleton(BindConfiguration(configuration))
      .AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>))
      .AddSingleton<IFileAbstraction, FileAbstraction>()
      .AddSingleton<IPathAbstraction, PathAbstraction>()
      .AddSingleton<IConnectionHelper, ConnectionHelper>()
      .AddSingleton<IUserRepo, UserRepo>()
      .AddSingleton<IUserLinksRepo, UserLinksRepo>()
      .AddSingleton<IUserTasksRepo, UserTasksRepo>()
      .AddSingleton<IGamesRepo, GamesRepo>()
      .AddSingleton<IJsonHelper, JsonHelper>()
      .AddSingleton<IAuthService, AuthService>()
      .AddSingleton<IGamesService, GamesService>()
      .AddSingleton<IUserTasksService, UserTasksService>()
      .AddSingleton<IUserLinksService, UserLinksService>();

  private static NlpConfig BindConfiguration(IConfiguration configuration)
  {
    var section = configuration.GetSection("NasLandingPage");
    if (!section.Exists())
      throw new NlpException("Unable to find NasLandingPage configuration section");
    var config = new NlpConfig();
    section.Bind(config);
    return config;
  }
}
