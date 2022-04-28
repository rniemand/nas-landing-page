using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NasLandingPage.Common.Extensions;
using NasLandingPage.Common.Providers;
using NasLandingPage.Common.Services;
using NLog.Extensions.Logging;
using Octokit;
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
    var credentialsService = _services.GetRequiredService<ICredentialsService>();
    var credentials = credentialsService.GetCredentials("github");

    // https://github.com/octokit/octokit.net/blob/main/docs/index.md
    // https://github.com/octokit/octokit.net/blob/main/docs/getting-started.md

    var github = new GitHubClient(new ProductHeaderValue("MyAmazingApp"));

    if (!string.IsNullOrWhiteSpace(credentials.Password))
    {
      var basicAuth = new Credentials(credentials.Username, credentials.Password);
      github.Credentials = basicAuth;
    }

    var repository = github.Repository.Get(137949496).GetAwaiter().GetResult();
    var tags = github.Repository.GetAllTags(137949496).GetAwaiter().GetResult();

    return this;
  }

  public NlpDevConsole TestProjectInfoProvider()
  {
    var infoProvider = _services.GetRequiredService<IProjectInfoProvider>();


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
