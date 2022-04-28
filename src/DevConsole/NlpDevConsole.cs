using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Helpers;
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

      // Logging
      .AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>))
      .AddLogging(loggingBuilder =>
      {
        // configure Logging with NLog
        loggingBuilder.ClearProviders();
        loggingBuilder.SetMinimumLevel(LogLevel.Trace);
        loggingBuilder.AddNLog(config);
      })

      // Helpers
      .AddSingleton<IJsonHelper, JsonHelper>()

      // Abstractions
      .AddSingleton<IFileAbstraction, FileAbstraction>()
      .AddSingleton<IDirectoryAbstraction, DirectoryAbstraction>()
      .AddSingleton<IEnvironmentAbstraction, EnvironmentAbstraction>()
      .AddSingleton<IPathAbstraction, PathAbstraction>()
      .AddSingleton<IDateTimeAbstraction, DateTimeAbstraction>();

    return services.BuildServiceProvider();
  }
}
