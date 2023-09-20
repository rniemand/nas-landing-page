using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using NasLandingPage.Auth;
using NasLandingPage.Extensions;
using NLog.Web;

namespace NasLandingPage;

public class Program
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);
    builder.Configuration.AddJsonFile("appsettings.machine.json", true);

    builder.Services.AddControllers()
      .ConfigureApplicationPartManager(manager =>
      {
        manager.FeatureProviders.Add(new NlpControllerFeatureProvider());
      })
      .AddJsonOptions(opts =>
      {
        opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
      });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddSwaggerDocument();
    builder.Services.AddNasLandingPage(builder.Configuration);

    builder.Services.AddNlpAuthentication();
    builder.Services.AddAuthorization(opts =>
    {
      opts.AddPolicy("NlpPassed", new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .RequireClaim("NlpPass")
        .Build());
    });

    builder.Host.UseNLog();

    var app = builder.Build();

    app.UseOpenApi();
    app.UseSwaggerUi3();
    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseEndpoints(endpoints =>
    {
      endpoints.MapControllers().RequireAuthorization("NlpPassed");
    });

#if DEBUG
    app.UseSpa(spa =>
    {
      spa.UseProxyToSpaDevelopmentServer("http://localhost:5173");
    });
#else
    app.UseStaticFiles();
    app.MapFallbackToFile("index.html");
#endif

    app.Run();
  }
}
