using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NasLandingPage.Common.Config;

namespace NasLandingPage.Common.Providers
{
  public interface INasLandingPageConfigProvider
  {
    NlpConfig Provide();
  }

  public class NasLandingPageConfigProvider : INasLandingPageConfigProvider
  {
    private readonly NlpConfig _config;

    public NasLandingPageConfigProvider(IServiceProvider serviceProvider)
    {
      _config = BindConfiguration(serviceProvider);
    }

    public NlpConfig Provide() => _config;

    private static NlpConfig BindConfiguration(IServiceProvider serviceProvider)
    {
      // TODO: [NasLandingPageConfigProvider.BindConfiguration] (TESTS) Add tests
      var configuration = serviceProvider.GetRequiredService<IConfiguration>();
      var boundConfig = new NlpConfig();
      var configSection = configuration.GetSection("NasLandingPage");

      if (!configSection.Exists())
        return boundConfig;

      configSection.Bind(boundConfig);
      return boundConfig;
    }
  }
}
