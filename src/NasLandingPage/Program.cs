using NasLandingPage.Services;
using NLog;
using NLog.Web;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Helpers;
using Rn.NetCore.Common.Logging;

// https://github.com/NLog/NLog/wiki/Getting-started-with-ASP.NET-Core-6
var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
  var builder = WebApplication.CreateBuilder(args);

  // Add services to the container.
  builder.Services.AddControllersWithViews();

  builder.Services
    .AddSingleton<IDirectoryAbstraction, DirectoryAbstraction>()
    .AddSingleton<IFileAbstraction, FileAbstraction>()
    .AddSingleton<IDateTimeAbstraction, DateTimeAbstraction>()
    .AddSingleton<IEnvironmentAbstraction, EnvironmentAbstraction>()

    .AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>))

    .AddSingleton<IJsonHelper, JsonHelper>()

    .AddSingleton<IProjectsService, ProjectsService>();

  builder.Logging.ClearProviders();
  builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
  builder.Host.UseNLog();

  var app = builder.Build();

  app.UseStaticFiles();
  app.UseRouting();

  app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

  app.MapFallbackToFile("index.html");

  app.Run();
}
catch (Exception exception)
{
  // NLog: catch setup errors
  logger.Error(exception, "Stopped program because of exception");
  throw;
}
finally
{
  // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
  LogManager.Shutdown();
}
