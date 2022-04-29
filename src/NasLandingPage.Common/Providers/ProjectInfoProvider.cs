using Microsoft.Extensions.DependencyInjection;
using NasLandingPage.Common.Config;
using NasLandingPage.Common.Helpers;
using NasLandingPage.Common.Models.Responses;
using NasLandingPage.Common.Models.Responses.Projects;

namespace NasLandingPage.Common.Providers;

public interface IProjectInfoProvider
{
  List<ProjectInfo> GetAll();
  ProjectInfo? GetByName(string name);
  void UpdateProjectInfo(ProjectInfo projectInfo);
  List<string> ListProjectFiles();
}

public class ProjectInfoProvider : IProjectInfoProvider
{
  private readonly IFileSystemHelper _fsHelper;
  private readonly NasLandingPageConfig _config;
  private readonly string _dataDir;
  private readonly string _backupDir;

  public ProjectInfoProvider(IServiceProvider serviceProvider)
  {
    // TODO: [ProjectInfoProvider.ProjectInfoProvider] (TESTS) Add tests
    _fsHelper = serviceProvider.GetRequiredService<IFileSystemHelper>();
    _config = serviceProvider.GetRequiredService<INasLandingPageConfigProvider>().Provide();

    _dataDir = GenerateDataDirPath();
    _backupDir = GenerateBackupDirPath();
  }


  public List<ProjectInfo> GetAll()
  {
    // TODO: [ProjectInfoProvider.GetAll] (TESTS) Add tests
    var files = _fsHelper.DirectoryGetFiles(_dataDir, "*.json", SearchOption.TopDirectoryOnly);

    return files
      .Where(file => !string.IsNullOrWhiteSpace(file))
      .Select(LoadProjectFile)
      .OrderBy(x => x.Name)
      .ToList();
  }

  public ProjectInfo? GetByName(string name)
  {
    // TODO: [ProjectInfoProvider.GetByName] (TESTS) Add tests
    var filePath = GenerateProjectFilePath(name);
    return !_fsHelper.FileExists(filePath) ? null : LoadProjectFile(filePath);
  }

  public void UpdateProjectInfo(ProjectInfo projectInfo)
  {
    // TODO: [ProjectInfoProvider.UpdateProjectInfo] (TESTS) Add tests
    var sourceFilePath = GenerateProjectFilePath(projectInfo.Metadata.FileNameWithoutExtension);
    var backupFilePath = sourceFilePath.Replace(_dataDir, _backupDir);

    if (!_fsHelper.BackupFileToFolder(sourceFilePath, backupFilePath))
    {
      // TODO: [ProjectInfoProvider.UpdateProjectInfo] (EX) Use better exception here
      throw new Exception("Unable to backup file");
    }

    projectInfo.Metadata = new ProjectInfoMetadata();
    _fsHelper.SaveJsonFile(sourceFilePath, projectInfo, true);
  }

  public List<string> ListProjectFiles() =>
    // TODO: [ProjectInfoProvider.ListDirectoryFiles] (TESTS) Add tests
    _fsHelper.ListDirectoryFiles(_dataDir, true);


  private string GenerateProjectFilePath(string name) =>
    // TODO: [ProjectInfoProvider.GenerateProjectFilePath] (TESTS) Add tests
    $"{_dataDir}{name}.json";

  private ProjectInfo LoadProjectFile(string path)
  {
    // TODO: [ProjectInfoProvider.LoadProjectFile] (TESTS) Add tests
    var projectInfo = _fsHelper.LoadJsonFile<ProjectInfo>(path);
    if (projectInfo is null)
      throw new Exception("Unable to load project file");

    projectInfo.Metadata = new ProjectInfoMetadata
    {
      FileName = _fsHelper.GetFileName(path),
      FileNameWithoutExtension = _fsHelper.GetFileNameWithoutExtension(path)
    };

    return projectInfo;
  }

  private string GenerateDataDirPath()
  {
    // TODO: [ProjectInfoProvider.GenerateDataDirPath] (TESTS) Add tests
    var sep = _config.IsLinux ? "/" : "\\";
    var rootDir = _fsHelper.CurrentDirectory;

    if (!rootDir.EndsWith(sep))
      rootDir += sep;

    var processed = _config.DataDir
      .Replace("./", rootDir);

    if (!processed.EndsWith(sep))
      processed += sep;

    processed += $"projects{sep}";
    _fsHelper.EnsureFolderExists(processed);

    return processed;
  }

  private string GenerateBackupDirPath()
  {
    // TODO: [ProjectInfoProvider.GenerateBackupDirPath] (TESTS) Add tests
    var sep = _config.IsLinux ? "/" : "\\";
    var rootDir = _fsHelper.CurrentDirectory;

    if (!rootDir.EndsWith(sep))
      rootDir += sep;

    var processed = _config.DataDir
      .Replace("./", rootDir);

    if (!processed.EndsWith(sep))
      processed += sep;

    processed += $"projects-backup{sep}";
    _fsHelper.EnsureFolderExists(processed);

    return processed;
  }
}
