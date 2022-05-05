using Microsoft.Extensions.DependencyInjection;
using NasLandingPage.Common.Config;
using NasLandingPage.Common.Helpers;
using NasLandingPage.Common.Models.Responses;
using Rn.NetCore.Common.Extensions;

namespace NasLandingPage.Common.Providers;

public interface IUserLinkProvider
{
  Task AddLink(UserLink link);
  Task<List<UserLink>> GetAll();
  Task<UserLink?> GetById(string linkId);
}

public class UserLinkProvider : IUserLinkProvider
{
  private readonly IFileSystemHelper _fsHelper;
  private readonly NlpConfig _config;
  private readonly string _dataDir;
  private readonly string _backupDir;

  public UserLinkProvider(IServiceProvider serviceProvider)
  {
    // TODO: [UserLinkProvider.UserLinkProvider] (TESTS) Add tests
    _fsHelper = serviceProvider.GetRequiredService<IFileSystemHelper>();
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

  public async Task<List<UserLink>> GetAll()
  {
    // TODO: [UserLinkProvider.GetAll] (TESTS) Add tests
    var linkFiles = _fsHelper.DirectoryGetFiles(_dataDir, "*.json", SearchOption.TopDirectoryOnly);
    await Task.CompletedTask;

    return GetEnabledLinks(linkFiles.ToList())
      .OrderBy(x => x.Order)
      .ToList();
  }

  public async Task<UserLink?> GetById(string linkId)
  {
    // TODO: [UserLinkProvider.GetById] (TESTS) Add tests
    var linkFilePath = GenerateLinkFilePath(linkId);
    return _fsHelper.LoadJsonFile<UserLink>(linkFilePath);
  }


  private List<UserLink> GetEnabledLinks(List<string> filePaths)
  {
    var enabledLinks = new List<UserLink>();

    foreach (var filePath in filePaths)
    {
      var parsedLink = _fsHelper.LoadJsonFile<UserLink>(filePath);

      if(parsedLink is null)
        continue;

      enabledLinks.Add(parsedLink);
    }

    return enabledLinks;
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
