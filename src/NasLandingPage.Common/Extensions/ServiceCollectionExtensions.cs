using Microsoft.Extensions.DependencyInjection;
using NasLandingPage.Common.Providers;
using NasLandingPage.Common.Services;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Helpers;
using Rn.NetCore.Common.Logging;

namespace NasLandingPage.Common.Extensions;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddNasLandingPage(this IServiceCollection serviceCollection)
  {
    // TODO: [ServiceCollectionExtensions.AddNasLandingPage] (TESTS) Add tests
    return serviceCollection
      .AddSingleton<IDirectoryAbstraction, DirectoryAbstraction>()
      .AddSingleton<IFileAbstraction, FileAbstraction>()
      .AddSingleton<IDateTimeAbstraction, DateTimeAbstraction>()
      .AddSingleton<IEnvironmentAbstraction, EnvironmentAbstraction>()

      .AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>))

      .AddSingleton<IJsonHelper, JsonHelper>()

      .AddSingleton<INasLandingPageConfigProvider, NasLandingPageConfigProvider>()

      .AddSingleton<IProjectsService, ProjectsService>()
      .AddSingleton<IConfigService, ConfigService>();
  }
}
