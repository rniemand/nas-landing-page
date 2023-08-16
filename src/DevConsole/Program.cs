using Microsoft.Extensions.DependencyInjection;
using NasLandingPage.Repos;
using NasLandingPage.Services;

namespace DevConsole;

internal class Program
{
  static async Task Main(string[] args)
  {
    var ghService = DIContainer.Services.GetRequiredService<IGitHubService>();
    var repoRepo = DIContainer.Services.GetRequiredService<IGitHubRepoRepo>();
    var selectedRepo = await repoRepo.GetByIdAsync(388318548);

    await ghService.SyncRepoFilesAsync(selectedRepo);

    //await ghService.SyncCoreRepoInformationAsync();
    Console.WriteLine();
  } 
}
