using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NasLandingPage.Extensions;
using NLog.Extensions.Logging;

namespace DevConsole;

internal static class DIContainer
{
  public static IServiceProvider Services { get; }

  static DIContainer()
  {
    var services = new ServiceCollection();

    var config = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json", true, true)
      .AddJsonFile("rn-mail.json", true, true)
      .Build();

    services
      .AddSingleton<IConfiguration>(config)
      .AddNasLandingPage(config)
      .AddLogging(loggingBuilder =>
      {
        loggingBuilder.ClearProviders();
        loggingBuilder.SetMinimumLevel(LogLevel.Trace);
        loggingBuilder.AddNLog(config);
      });

    Services = services.BuildServiceProvider();
  }
}
