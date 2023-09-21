using NasLandingPage.Repos;
using NasLandingPage.Services;
using RnCore.Abstractions;
using RnCore.Logging;

namespace NasLandingPage.Extensions;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddNasLandingPage(this IServiceCollection services) => services
    .AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>))
    .AddSingleton<IFileAbstraction, FileAbstraction>()
    .AddSingleton<IPathAbstraction, PathAbstraction>()
    .AddSingleton<IConnectionHelper, ConnectionHelper>()
    .AddSingleton<IUserRepo, UserRepo>()
    .AddSingleton<IUserLinksRepo, UserLinksRepo>()
    .AddSingleton<IJsonHelper, JsonHelper>()
    .AddSingleton<IAuthService, AuthService>()
    .AddSingleton<IUserLinksService, UserLinksService>();
}
