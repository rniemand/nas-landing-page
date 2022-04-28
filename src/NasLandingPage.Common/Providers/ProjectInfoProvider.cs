using Microsoft.Extensions.DependencyInjection;
using NasLandingPage.Common.Config;
using NasLandingPage.Common.Models;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Helpers;

namespace NasLandingPage.Common.Providers;

public interface IProjectInfoProvider
{
  List<ProjectInfo> GetAll();
  ProjectInfo GetByName(string name);
}

public class ProjectInfoProvider : IProjectInfoProvider
{
  private readonly IDirectoryAbstraction _directory;
  private readonly IEnvironmentAbstraction _environment;
  private readonly IFileAbstraction _file;
  private readonly IJsonHelper _jsonHelper;
  private readonly NasLandingPageConfig _config;
  private readonly string _dataDir;

  public ProjectInfoProvider(IServiceProvider serviceProvider)
  {
    // TODO: [ProjectInfoProvider.ProjectInfoProvider] (TESTS) Add tests
    _directory = serviceProvider.GetRequiredService<IDirectoryAbstraction>();
    _environment = serviceProvider.GetRequiredService<IEnvironmentAbstraction>();
    _file = serviceProvider.GetRequiredService<IFileAbstraction>();
    _jsonHelper = serviceProvider.GetRequiredService<IJsonHelper>();
    _config = serviceProvider.GetRequiredService<INasLandingPageConfigProvider>().Provide();

    _dataDir = GenerateDataDirPath();
    EnsureDataDirExists();
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

  public ProjectInfo GetByName(string name)
  {
    // TODO: [ProjectInfoProvider.GetByName] (TESTS) Add tests
    var filePath = GenerateProjectFilePath(name);
    return LoadProjectFile(filePath);
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
    return _jsonHelper.DeserializeObject<ProjectInfo>(fileJson);
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

    return processed;
  }

  private void EnsureDataDirExists()
  {
    // TODO: [ProjectInfoProvider.EnsureDataDirExists] (TESTS) Add tests
    if (_directory.Exists(_dataDir))
      return;

    _directory.CreateDirectory(_dataDir);
  }
}
