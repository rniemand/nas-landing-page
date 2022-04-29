using Microsoft.Extensions.DependencyInjection;
using NasLandingPage.Common.Config;
using NasLandingPage.Common.Models.Responses;
using NasLandingPage.Common.Providers;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Extensions;
using Rn.NetCore.Common.Helpers;

namespace NasLandingPage.Common.Services;

public interface ICredentialsService
{
  BasicCredentials GetCredentials(string credentialsName);
}

public class CredentialsService : ICredentialsService
{
  private readonly IEnvironmentAbstraction _environment;
  private readonly IFileAbstraction _file;
  private readonly IJsonHelper _jsonHelper;
  private readonly NasLandingPageConfig _config;
  private readonly string _credentialsFile;
  private readonly List<BasicCredentials> _credentials;

  public CredentialsService(IServiceProvider serviceProvider)
  {
    // TODO: [CredentialsService.CredentialsService] (TESTS) Add tests
    _environment = serviceProvider.GetRequiredService<IEnvironmentAbstraction>();
    _file = serviceProvider.GetRequiredService<IFileAbstraction>();
    _jsonHelper = serviceProvider.GetRequiredService<IJsonHelper>();
    _config = serviceProvider.GetRequiredService<INasLandingPageConfigProvider>().Provide();

    _credentialsFile = GenerateCredentialsFilePath();
    _credentials = LoadCredentials();
  }

  public BasicCredentials GetCredentials(string credentialsName)
  {
    // TODO: [CredentialsService.GetCredentials] (TESTS) Add tests
    return !ContainsCredentials(credentialsName)
      ? new BasicCredentials()
      : _credentials.First(x => x.CredentialsName.IgnoreCaseEquals(credentialsName));
  }

  private bool ContainsCredentials(string name)
  {
    // TODO: [CredentialsService.ContainsCredentials] (TESTS) Add tests
    if (_credentials.Count == 0)
      return false;

    return _credentials.Any(x => x.CredentialsName.IgnoreCaseEquals(name));
  }

  private string GenerateCredentialsFilePath()
  {
    // TODO: [CredentialsService.GenerateCredentialsFilePath] (TESTS) Add tests
    var sep = _config.IsLinux ? "/" : "\\";
    var rootDir = _environment.CurrentDirectory;

    if (!rootDir.EndsWith(sep))
      rootDir += sep;

    var processed = _config.DataDir
      .Replace("./", rootDir);

    if (!processed.EndsWith(sep))
      processed += sep;

    processed += "credentials.json";

    return processed;
  }

  private void EnsureCredentialsFileExists()
  {
    // TODO: [CredentialsService.EnsureCredentialsFileExists] (TESTS) Add tests
    if(_file.Exists(_credentialsFile))
      return;

    var credentialsList = new List<BasicCredentials>
    {
      new BasicCredentials
      {
        CredentialsName = "sample",
        Password = "pass",
        Username = "user"
      }
    };

    var credentialsJson = _jsonHelper.SerializeObject(credentialsList, true);
    _file.WriteAllText(_credentialsFile, credentialsJson);
  }

  private List<BasicCredentials> LoadCredentials()
  {
    // TODO: [CredentialsService.LoadCredentials] (TESTS) Add tests
    EnsureCredentialsFileExists();
    var rawJson = _file.ReadAllText(_credentialsFile);
    var parsedCredentials = _jsonHelper.DeserializeObject<List<BasicCredentials>>(rawJson);

    foreach (var credentials in parsedCredentials)
    {
      credentials.CredentialsName = credentials.CredentialsName.LowerTrim();
    }

    return parsedCredentials;
  }
}
