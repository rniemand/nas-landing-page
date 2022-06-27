using Microsoft.Extensions.DependencyInjection;
using NasLandingPage.Common.Config;
using NasLandingPage.Common.Models.Responses;
using NasLandingPage.Common.Providers;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Helpers;
using Rn.NetCore.Metrics;
using Rn.NetCore.Metrics.Builders;
using Rn.NetCore.Metrics.Extensions;

namespace NasLandingPage.Common.Services;

public interface IConfigService
{
  ClientConfig GetClientConfig();
}

public class ConfigService : IConfigService
{
  private readonly IEnvironmentAbstraction _environment;
  private readonly IFileAbstraction _file;
  private readonly IJsonHelper _jsonHelper;
  private readonly IMetricService _metrics;
  private readonly NlpConfig _config;
  private readonly string _configFilePath;

  public ConfigService(IServiceProvider serviceProvider)
  {
    _environment = serviceProvider.GetRequiredService<IEnvironmentAbstraction>();
    _file = serviceProvider.GetRequiredService<IFileAbstraction>();
    _jsonHelper = serviceProvider.GetRequiredService<IJsonHelper>();
    _metrics = serviceProvider.GetRequiredService<IMetricService>();
    _config = serviceProvider.GetRequiredService<INasLandingPageConfigProvider>().Provide();

    _configFilePath = GenerateConfigFilePath();
  }

  public ClientConfig GetClientConfig()
  {
    var metricBuilder = new ServiceMetricBuilder(nameof(ConfigService), nameof(GetClientConfig));

    try
    {
      using (metricBuilder.WithTiming())
      {
        var configJson = _file.ReadAllText(_configFilePath);
        return _jsonHelper.DeserializeObject<ClientConfig>(configJson);
      }
    }
    catch (Exception ex)
    {
      metricBuilder.WithException(ex);
      throw;
    }
    finally
    {
      _metrics.SubmitAsync(metricBuilder);
    }
  }

  private string GenerateConfigFilePath()
  {
    var sep = _config.IsLinux ? "/" : "\\";
    var rootDir = _environment.CurrentDirectory;

    if (!rootDir.EndsWith(sep))
      rootDir += sep;

    var processed = _config.DataDir
      .Replace("./", rootDir);

    if (!processed.EndsWith(sep))
      processed += sep;

    processed += "config.json";

    return processed;
  }
}
