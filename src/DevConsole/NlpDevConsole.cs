using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NasLandingPage.Common.Clients;
using NasLandingPage.Common.Extensions;
using NasLandingPage.Common.Providers;
using NasLandingPage.Common.Services;
using NLog.Extensions.Logging;
using Rn.NetCore.Common.Logging;

namespace DevConsole;

public class NlpDevConsole
{
  private readonly IServiceProvider _services;

  public NlpDevConsole()
  {
    _services = BuildServiceContainer();
  }

  public NlpDevConsole DoNothing()
  {
    return this;
  }

  public NlpDevConsole HelloWorld()
  {
    _services
      .GetRequiredService<ILoggerAdapter<NlpDevConsole>>()
      .LogInformation("Hello World");

    return this;
  }

  public NlpDevConsole TestGetCredentials()
  {
    var credentialsService = _services.GetRequiredService<ICredentialsService>();
    var credentials = credentialsService.GetCredentials("github");
    Console.WriteLine($"{credentials.Username}|{credentials.Password}");
    return this;
  }

  public NlpDevConsole TestGitHubClient()
  {
    var gitHubClient = _services.GetRequiredService<INlpGitHubClient>();

    var repository = gitHubClient.GetRepositoryAsync(137949496).GetAwaiter().GetResult();

    return this;
  }

  public NlpDevConsole TestProjectInfoProvider()
  {
    var infoProvider = _services.GetRequiredService<IProjectInfoProvider>();
    var projectInfo = infoProvider.GetByName("Alert-Maker");
    infoProvider.UpdateProjectInfo(projectInfo);

    return this;
  }

  private static IServiceProvider BuildServiceContainer()
  {
    var services = new ServiceCollection();

    var config = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json", true, true)
      .Build();

    services
      // Configuration
      .AddSingleton<IConfiguration>(config)
      .AddNasLandingPage()

      // Logging
      .AddLogging(loggingBuilder =>
      {
        // configure Logging with NLog
        loggingBuilder.ClearProviders();
        loggingBuilder.SetMinimumLevel(LogLevel.Trace);
        loggingBuilder.AddNLog(config);
      });

    return services.BuildServiceProvider();
  }
}
