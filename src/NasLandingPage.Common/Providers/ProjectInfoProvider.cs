using Microsoft.Extensions.DependencyInjection;
using NasLandingPage.Common.Config;
using NasLandingPage.Common.Helpers;
using NasLandingPage.Common.Models.Responses;
using NasLandingPage.Common.Models.Responses.Projects;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Helpers;

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
  private readonly IDirectoryAbstraction _directory;
  private readonly IEnvironmentAbstraction _environment;
  private readonly IFileAbstraction _file;
  private readonly IJsonHelper _jsonHelper;
  private readonly IPathAbstraction _path;
  private readonly NasLandingPageConfig _config;
  private readonly string _dataDir;
  private readonly string _backupDir;

  public ProjectInfoProvider(IServiceProvider serviceProvider)
  {
    // TODO: [ProjectInfoProvider.ProjectInfoProvider] (TESTS) Add tests
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


  public List<ProjectInfo> GetAll()
  {
    // TODO: [ProjectInfoProvider.GetAll] (TESTS) Add tests
    var projects = new List<ProjectInfo>();

    var files = _directory.GetFiles(_dataDir, "*.json", SearchOption.TopDirectoryOnly);

    foreach (var file in files)
    {
      if (string.IsNullOrWhiteSpace(file)) continue;
      projects.Add(LoadProjectFile(file));
    }

    return projects;
  }

  public ProjectInfo? GetByName(string name)
  {
    // TODO: [ProjectInfoProvider.GetByName] (TESTS) Add tests
    var filePath = GenerateProjectFilePath(name);

    if (!_file.Exists(filePath))
      return null;

    return LoadProjectFile(filePath);
  }

  public void UpdateProjectInfo(ProjectInfo projectInfo)
  {
    // TODO: [ProjectInfoProvider.UpdateProjectInfo] (TESTS) Add tests
    var sourceFilePath = GenerateProjectFilePath(projectInfo.Metadata.FileNameWithoutExtension);
    var backupFilePath = sourceFilePath.Replace(_dataDir, _backupDir);

    if (!BackupFile(sourceFilePath, backupFilePath))
    {
      // TODO: [ProjectInfoProvider.UpdateProjectInfo] (EX) Use better exception here
      throw new Exception("Unable to backup file");
    }

    projectInfo.Metadata = new ProjectInfoMetadata();
    var projectJson = _jsonHelper.SerializeObject(projectInfo, true);
    _file.WriteAllText(sourceFilePath, projectJson);
  }

  public List<string> ListProjectFiles()
  {
    // TODO: [ProjectInfoProvider.ListProjectFiles] (TESTS) Add tests
    var projectFiles = new List<string>();
    var files = _directory.GetFiles(_dataDir, "*.json", SearchOption.TopDirectoryOnly);

    foreach (var file in files)
    {
      var fileName = _path.GetFileNameWithoutExtension(file);
      projectFiles.Add(fileName);
    }

    return projectFiles;
  }


  private bool BackupFile(string source, string destination)
  {
    // TODO: [ProjectInfoProvider.BackupFile] (TESTS) Add tests
    if (!_file.Exists(source))
    {
      return false;
    }

    if (_file.Exists(destination))
    {
      _file.Delete(destination);
    }

    _file.Move(source, destination);
    return true;
  }

  private string GenerateProjectFilePath(string name)
  {
    // TODO: [ProjectInfoProvider.GenerateProjectFilePath] (TESTS) Add tests
    return $"{_dataDir}{name}.json";
  }

  private ProjectInfo LoadProjectFile(string path)
  {
    // TODO: [ProjectInfoProvider.LoadProjectFile] (TESTS) Add tests
    var fileJson = _file.ReadAllText(path);
    var projectInfo = _jsonHelper.DeserializeObject<ProjectInfo>(fileJson);

    projectInfo.Metadata = new ProjectInfoMetadata
    {
      FileName = _path.GetFileName(path),
      FileNameWithoutExtension = _path.GetFileNameWithoutExtension(path)
    };

    return projectInfo;
  }

  private string GenerateDataDirPath()
  {
    // TODO: [ProjectInfoProvider.GenerateDataDirPath] (TESTS) Add tests
    var sep = _config.IsLinux ? "/" : "\\";
    var rootDir = _environment.CurrentDirectory;

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
    var rootDir = _environment.CurrentDirectory;

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
