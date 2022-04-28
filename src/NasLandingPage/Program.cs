using NasLandingPage.Common.Extensions;
using NLog;
using NLog.Web;

// https://github.com/NLog/NLog/wiki/Getting-started-with-ASP.NET-Core-6
var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
  var builder = WebApplication.CreateBuilder(args);

  // Add services to the container.
  builder.Services.AddControllersWithViews();
  builder.Services.AddNasLandingPage();
  builder.Services.AddSwaggerDocument();

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
