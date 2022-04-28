using NasLandingPage.Common.Config;
using NasLandingPage.Models;
using NasLandingPage.Providers;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Helpers;

namespace NasLandingPage.Services;

public interface IConfigService
{
  ClientConfig GetClientConfig();
}

public class ConfigService : IConfigService
{
  private readonly IEnvironmentAbstraction _environment;
  private readonly IFileAbstraction _file;
  private readonly IJsonHelper _jsonHelper;
  private readonly NasLandingPageConfig _config;
  private readonly string _configFilePath;

  public ConfigService(IServiceProvider serviceProvider)
  {
    // TODO: [ConfigService.ConfigService] (TESTS) Add tests
    _environment = serviceProvider.GetRequiredService<IEnvironmentAbstraction>();
    _file = serviceProvider.GetRequiredService<IFileAbstraction>();
    _jsonHelper = serviceProvider.GetRequiredService<IJsonHelper>();
    _config = serviceProvider.GetRequiredService<INasLandingPageConfigProvider>().Provide();

    _configFilePath = GenerateConfigFilePath();
  }

  public ClientConfig GetClientConfig()
  {
    // TODO: [ConfigService.GetClientConfig] (TESTS) Add tests
    var configJson = _file.ReadAllText(_configFilePath);
    return _jsonHelper.DeserializeObject<ClientConfig>(configJson);
  }

  private string GenerateConfigFilePath()
  {
    // TODO: [ConfigService.GenerateConfigFilePath] (TESTS) Add tests
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
