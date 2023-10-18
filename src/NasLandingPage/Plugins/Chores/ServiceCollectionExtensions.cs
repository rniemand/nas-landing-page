namespace NasLandingPage.Plugins.Chores;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddHomeChores(this IServiceCollection services)
  {
    return services
      .AddSingleton<IChoreRepo, ChoreRepo>()
      .AddSingleton<IChoreService, ChoreService>();
  }
}
