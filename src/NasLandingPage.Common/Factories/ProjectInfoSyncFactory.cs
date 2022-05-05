using Microsoft.Extensions.DependencyInjection;
using NasLandingPage.Common.Clients;
using NasLandingPage.Common.Sync;
using Rn.NetCore.Common.Helpers;

namespace NasLandingPage.Common.Factories;

public interface IProjectInfoSyncFactory
{
  ICoreRepositoryInfoSync CreateCoreRepositoryInfoSync();
  IRootRepositoryContentInfoSync CreateRootRepositoryContentInfoSync();
  IBuildScriptInfoSync CreateBuildScriptInfoSync();
  IProjectCiInfoSync GetProjectCiInfoSync();
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
    return new RepoRootInfoSync(_serviceProvider.GetRequiredService<INlpGitHubClient>());
  }

  public IBuildScriptInfoSync CreateBuildScriptInfoSync()
  {
    return new BuildScriptInfoSync(_serviceProvider.GetRequiredService<INlpGitHubClient>());
  }

  public IProjectCiInfoSync GetProjectCiInfoSync()
  {
    return new ProjectCiInfoSync(_serviceProvider.GetRequiredService<INlpGitHubClient>(),
      _serviceProvider.GetRequiredService<IJsonHelper>());
  }
}
