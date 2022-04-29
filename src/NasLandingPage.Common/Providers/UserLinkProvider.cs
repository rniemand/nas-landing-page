using Microsoft.Extensions.DependencyInjection;
using NasLandingPage.Common.Config;
using NasLandingPage.Common.Helpers;
using NasLandingPage.Common.Models.Responses;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Extensions;
using Rn.NetCore.Common.Helpers;

namespace NasLandingPage.Common.Providers;

public interface IUserLinkProvider
{
  Task AddLink(UserLink link);
}

public class UserLinkProvider : IUserLinkProvider
{
  private readonly IFileSystemHelper _fsHelper;
  private readonly IDirectoryAbstraction _directory;
  private readonly IEnvironmentAbstraction _environment;
  private readonly IFileAbstraction _file;
  private readonly IJsonHelper _jsonHelper;
  private readonly IPathAbstraction _path;
  private readonly NasLandingPageConfig _config;
  private readonly string _dataDir;
  private readonly string _backupDir;

  public UserLinkProvider(IServiceProvider serviceProvider)
  {
    // TODO: [UserLinkProvider.UserLinkProvider] (TESTS) Add tests
    _fsHelper = serviceProvider.GetRequiredService<IFileSystemHelper>();
    _directory = serviceProvider.GetRequiredService<IDirectoryAbstraction>();
    _environment = serviceProvider.GetRequiredService<IEnvironmentAbstraction>();
    _file = serviceProvider.GetRequiredService<IFileAbstraction>();
    _jsonHelper = serviceProvider.GetRequiredService<IJsonHelper>();
    _path = serviceProvider.GetRequiredService<IPathAbstraction>();
    _config = serviceProvider.GetRequiredService<INasLandingPageConfigProvider>().Provide();

    _dataDir = GenerateDataDirPath();
    _backupDir = GenerateBackupDirPath();
  }


  public async Task AddLink(UserLink link)
  {
    // TODO: [UserLinkProvider.AddLink] (TESTS) Add tests
    link.LinkId = Guid.NewGuid();
    var filePath = GenerateLinkFilePath(link.LinkId);
    await Task.CompletedTask;
    _fsHelper.SaveJsonFile(filePath, link, true);
  }


  private string GenerateLinkFilePath(Guid linkId) =>
    GenerateLinkFilePath(linkId.ToString("N"));

  private string GenerateLinkFilePath(string linkId)
  {
    // TODO: [UserLinkProvider.GenerateLinkFilePath] (TESTS) Add tests
    var cleanLinkId = linkId.Replace("-", "").LowerTrim();
    return $"{_dataDir}{cleanLinkId}.json";
  }

  private string GenerateDataDirPath()
  {
    // TODO: [UserLinkProvider.GenerateDataDirPath] (TESTS) Add tests
    var sep = _config.IsLinux ? "/" : "\\";
    var rootDir = _fsHelper.CurrentDirectory;

    if (!rootDir.EndsWith(sep))
      rootDir += sep;

    var processed = _config.DataDir
      .Replace("./", rootDir);

    if (!processed.EndsWith(sep))
      processed += sep;

    processed += $"links{sep}";
    _fsHelper.EnsureFolderExists(processed);

    return processed;
  }

  private string GenerateBackupDirPath()
  {
    // TODO: [UserLinkProvider.GenerateBackupDirPath] (TESTS) Add tests
    var sep = _config.IsLinux ? "/" : "\\";
    var rootDir = _fsHelper.CurrentDirectory;

    if (!rootDir.EndsWith(sep))
      rootDir += sep;

    var processed = _config.DataDir
      .Replace("./", rootDir);

    if (!processed.EndsWith(sep))
      processed += sep;

    processed += $"links-backup{sep}";
    _fsHelper.EnsureFolderExists(processed);

    return processed;
  }
}
