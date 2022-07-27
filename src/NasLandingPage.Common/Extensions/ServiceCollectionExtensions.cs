using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NasLandingPage.Common.Clients;
using NasLandingPage.Common.Database;
using NasLandingPage.Common.Factories;
using NasLandingPage.Common.Helpers;
using NasLandingPage.Common.Providers;
using NasLandingPage.Common.Services;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Helpers;
using Rn.NetCore.Common.Logging;
using Rn.NetCore.DbCommon;
using Rn.NetCore.Metrics.Extensions;
using Rn.NetCore.Metrics.Rabbit.Extensions;

namespace NasLandingPage.Common.Extensions;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddNasLandingPage(this IServiceCollection serviceCollection, IConfiguration configuration)
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

      .AddRnDbMySql(configuration)
      .AddSingleton<IUserLinkRepo, UserLinkRepo>()
      .AddSingleton<IProjectRepo, ProjectRepo>()
      .AddSingleton<IRepositoryRepo, RepositoryRepo>()
      .AddSingleton<ISonarQubeInfoRepo, SonarQubeInfoRepo>()
      .AddSingleton<IScmRepo, ScmRepo>()
      .AddSingleton<IUserLinkRepoQueries, UserLinkRepoQueries>()
      .AddSingleton<IProjectRepoQueries, ProjectRepoQueries>()
      .AddSingleton<IRepositoryRepoQueries, RepositoryRepoQueries>()
      .AddSingleton<ISonarQubeInfoRepoQueries, SonarQubeInfoRepoQueries>()
      .AddSingleton<IScmRepoQueries, ScmRepoQueries>()

      .AddSingleton<INasLandingPageConfigProvider, NasLandingPageConfigProvider>()
      .AddSingleton<IProjectInfoProvider, ProjectInfoProvider>()
      .AddSingleton<ILinkImageProvider, LinkImageProvider>()

      .AddSingleton<IProjectsService, ProjectsService>()
      .AddSingleton<IConfigService, ConfigService>()
      .AddSingleton<ICredentialsService, CredentialsService>()
      .AddSingleton<IUserLinkService, UserLinkService>()

      .AddRnMetricsBase(configuration)
      .AddRnMetricRabbitMQ();
  }
}
