using Microsoft.Extensions.DependencyInjection;
using NasLandingPage.Services;

namespace DevConsole;

internal class Program
{
  static void Main(string[] args)
  {
    var links = DIContainer.Services.GetRequiredService<IUserLinkService>();



    Console.WriteLine("Hello, World!");
    Console.WriteLine();
  }
}
