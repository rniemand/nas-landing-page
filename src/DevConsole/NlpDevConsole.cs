using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NasLandingPage.Common.Extensions;
using NasLandingPage.Common.Models.Requests;
using NasLandingPage.Common.Services;
using NLog.Extensions.Logging;

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
  
  public NlpDevConsole TestGetCredentials()
  {
    var credentialsService = _services.GetRequiredService<ICredentialsService>();
    var credentials = credentialsService.GetCredentials("github");
    Console.WriteLine($"{credentials.Username}|{credentials.Password}");
    return this;
  }
  
  public NlpDevConsole TestProjectSync()
  {
    var projectsService = _services.GetRequiredService<IProjectsService>();

    var commandResponse = projectsService.SyncProject(new RunCommandRequest
    {
      Command = "SyncProject",
      Arguments = "DnsUpdater"
    }).GetAwaiter().GetResult();


    Console.WriteLine(commandResponse.Success);
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
