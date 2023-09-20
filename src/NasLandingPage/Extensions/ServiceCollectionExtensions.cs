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
      .AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>))
      // Abstractions
      .AddSingleton<IFileAbstraction, FileAbstraction>()
      .AddSingleton<IPathAbstraction, PathAbstraction>()
      // Database
      .AddSingleton<IConnectionHelper, ConnectionHelper>()
      .AddSingleton<IUserRepo, UserRepo>()
      // Helpers
      .AddSingleton<IJsonHelper, JsonHelper>()
      // Services
      .AddSingleton<IAuthService, AuthService>();
  }

  //private static AppConfig BindAppConfig(IConfiguration configuration)
  //{
  //  var section = configuration.GetSection("NasLandingPage");
  //  if (!section.Exists())
  //    throw new Exception("Unable to find 'NasLandingPage' configuration");

  //  var config = new AppConfig();
  //  section.Bind(config);
  //  return config;
  //}
}
