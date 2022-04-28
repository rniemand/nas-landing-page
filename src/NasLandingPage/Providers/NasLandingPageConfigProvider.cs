using NasLandingPage.Config;

namespace NasLandingPage.Providers
{
  public interface INasLandingPageConfigProvider
  {
    NasLandingPageConfig Provide();
  }

  public class NasLandingPageConfigProvider : INasLandingPageConfigProvider
  {
    private readonly NasLandingPageConfig _config;

    public NasLandingPageConfigProvider(IServiceProvider serviceProvider)
    {
      _config = BindConfiguration(serviceProvider);
    }

    public NasLandingPageConfig Provide() => _config;

    private static NasLandingPageConfig BindConfiguration(IServiceProvider serviceProvider)
    {
      // TODO: [NasLandingPageConfigProvider.BindConfiguration] (TESTS) Add tests
      var configuration = serviceProvider.GetRequiredService<IConfiguration>();
      var boundConfig = new NasLandingPageConfig();
      var configSection = configuration.GetSection("NasLandingPage");

      if (!configSection.Exists())
        return boundConfig;

      configSection.Bind(boundConfig);
      return boundConfig;
    }
  }
}
