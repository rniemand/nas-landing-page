using Microsoft.Extensions.DependencyInjection;
using NasLandingPage.Services;

namespace DevConsole;

internal class Program
{
  static async Task Main(string[] args)
  {
    var ghService = DIContainer.Services.GetRequiredService<IGitHubService>();
    await ghService.SyncCoreRepoInformationAsync();
    Console.WriteLine();
  } 
}
