using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NasLandingPage.Common.Database;
using NasLandingPage.Common.Extensions;
using NasLandingPage.Common.Models.Requests;
using NasLandingPage.Common.Models.Responses;
using NasLandingPage.Common.Providers;
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
      //Arguments = "BitMeterCollector"
      Arguments = "BuildScriptHelper"
    }).GetAwaiter().GetResult();

    Console.WriteLine(commandResponse.Success);
    return this;
  }

  public NlpDevConsole FollowLink()
  {
    _services
      .GetRequiredService<IUserLinkService>()
      .RegisterFollow("005b08955aea4b34b50525fc462421b0")
      .GetAwaiter()
      .GetResult();

    return this;
  }

  public NlpDevConsole GetAllLinks()
  {
    var links = _services.GetRequiredService<IUserLinkRepo>()
      .GetAllAsync()
      .ConfigureAwait(false)
      .GetAwaiter()
      .GetResult();

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
      .AddNasLandingPage(config)

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

  private void AddLink()
  {
    _services.GetRequiredService<ILinkProvider>().AddLink(new UserLink
    {
      Name = "",
      Url = "",
      Image = ""
    });

    Console.WriteLine();
  }
}
