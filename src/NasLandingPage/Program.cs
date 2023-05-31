
using NasLandingPage.Extensions;

namespace NasLandingPage;

public class Program
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddSwaggerDocument();
    builder.Services.AddNasLandingPage(builder.Configuration);

    var app = builder.Build();

    app.UseOpenApi();
    app.UseSwaggerUi3();
    app.MapControllers();
    app.UseRouting();
    app.UseAuthorization();
    app.UseEndpoints(_ => { });

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
