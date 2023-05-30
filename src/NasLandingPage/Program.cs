
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

    var app = builder.Build();
    app.UseOpenApi();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
      app.UseSwaggerUi3();
      app.UseSwaggerUI();
    }

    app.UseAuthorization();


    app.MapControllers();

    app.Run();
  }
}
