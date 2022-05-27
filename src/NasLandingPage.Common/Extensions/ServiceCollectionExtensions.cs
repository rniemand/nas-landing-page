using Microsoft.Extensions.DependencyInjection;
using NasLandingPage.Common.Clients;
using NasLandingPage.Common.Factories;
using NasLandingPage.Common.Helpers;
using NasLandingPage.Common.Providers;
using NasLandingPage.Common.Services;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Helpers;
using Rn.NetCore.Common.Logging;
using Rn.NetCore.Metrics.Extensions;
using Rn.NetCore.Metrics.Rabbit.Extensions;

namespace NasLandingPage.Common.Extensions;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddNasLandingPage(this IServiceCollection serviceCollection)
  {
    return serviceCollection
      .AddSingleton<IDirectoryAbstraction, DirectoryAbstraction>()
      .AddSingleton<IFileAbstraction, FileAbstraction>()
      .AddSingleton<IDateTimeAbstraction, DateTimeAbstraction>()
      .AddSingleton<IEnvironmentAbstraction, EnvironmentAbstraction>()
      .AddSingleton<IPathAbstraction, PathAbstraction>()

      .AddSingleton<INlpGitHubClient, NlpGitHubClient>()

      .AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>))

      .AddSingleton<IProjectInfoSyncFactory, ProjectInfoSyncFactory>()

      .AddSingleton<IJsonHelper, JsonHelper>()
      .AddSingleton<IFileSystemHelper, FileSystemHelper>()

      .AddSingleton<INasLandingPageConfigProvider, NasLandingPageConfigProvider>()
      .AddSingleton<IProjectInfoProvider, ProjectInfoProvider>()
      .AddSingleton<ILinkProvider, LinkProvider>()
      .AddSingleton<ILinkImageProvider, LinkImageProvider>()

      .AddSingleton<IProjectsService, ProjectsService>()
      .AddSingleton<IConfigService, ConfigService>()
      .AddSingleton<ICredentialsService, CredentialsService>()
      .AddSingleton<IUserLinkService, UserLinkService>()

      .AddRnMetricsBase()
      .AddRnRabbitMQMetrics();
  }
}
