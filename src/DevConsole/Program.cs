using System.Net.Http.Headers;
using System.Text;
using System.Web;
using Microsoft.Extensions.DependencyInjection;
using NasLandingPage.Auth;
using NasLandingPage.Repos;
using NasLandingPage.Services;

namespace DevConsole;

internal class Program
{
  static async Task Main(string[] args)
  {
    //var ghService = DIContainer.Services.GetRequiredService<IGitHubService>();
    //var repoRepo = DIContainer.Services.GetRequiredService<IGitHubRepoRepo>();
    //var selectedRepo = await repoRepo.GetByIdAsync(388318548);
    //await ghService.SyncRepoFilesAsync(selectedRepo);
    //await ghService.SyncCoreRepoInformationAsync();

    var fitBitService = DIContainer.Services.GetRequiredService<IFitBitService>();
    var response = await fitBitService.GetFitbitActivitySummaryAsync(DateOnly.Parse("2023-08-10"));


    Console.WriteLine();
    Console.WriteLine();
  }

}
