using Microsoft.Extensions.DependencyInjection;
using NasLandingPage.Common.Clients;
using NasLandingPage.Common.Sync;

namespace NasLandingPage.Common.Factories;

public interface IProjectInfoSyncFactory
{
  ICoreRepositoryInfoSync CreateCoreRepositoryInfoSync();
  IRootRepositoryContentInfoSync CreateRootRepositoryContentInfoSync();
}

public class ProjectInfoSyncFactory : IProjectInfoSyncFactory
{
  private readonly IServiceProvider _serviceProvider;

  public ProjectInfoSyncFactory(IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public ICoreRepositoryInfoSync CreateCoreRepositoryInfoSync()
  {
    return new CoreRepositoryInfoSync(_serviceProvider.GetRequiredService<INlpGitHubClient>());
  }

  public IRootRepositoryContentInfoSync CreateRootRepositoryContentInfoSync()
  {
    return new RootRepositoryContentInfoSync(_serviceProvider.GetRequiredService<INlpGitHubClient>());
  }
}
