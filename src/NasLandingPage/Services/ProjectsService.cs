using NasLandingPage.Common.Config;
using NasLandingPage.Models;
using NasLandingPage.Providers;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Helpers;

namespace NasLandingPage.Services
{
  public interface IProjectsService
  {
    List<ProjectInfo> GetAll();
  }

  public class ProjectsService : IProjectsService
  {
    private readonly IDirectoryAbstraction _directory;
    private readonly IEnvironmentAbstraction _environment;
    private readonly IFileAbstraction _file;
    private readonly IJsonHelper _jsonHelper;
    private readonly NasLandingPageConfig _config;
    private readonly string _dataDir;

    public ProjectsService(IServiceProvider serviceProvider)
    {
      // TODO: [ProjectsService.ProjectsService] (TESTS) Add tests
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
      var projects = new List<ProjectInfo>();

      var files = _directory.GetFiles(_dataDir, "*.json", SearchOption.TopDirectoryOnly);

      foreach (var file in files)
      {
        if (string.IsNullOrWhiteSpace(file)) continue;
        projects.Add(LoadProjectFile(file));
      }

      return projects;
    }


    private ProjectInfo LoadProjectFile(string path)
    {
      // TODO: [ProjectsService.LoadProjectFile] (TESTS) Add tests
      var fileJson = _file.ReadAllText(path);
      return _jsonHelper.DeserializeObject<ProjectInfo>(fileJson);
    }
    
    private string GenerateDataDirPath()
    {
      // TODO: [ProjectsService.GenerateDataDirPath] (TESTS) Add tests
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
      // TODO: [ProjectsService.EnsureDataDirExists] (TESTS) Add tests
      if (_directory.Exists(_dataDir))
        return;

      _directory.CreateDirectory(_dataDir);
    }
  }
}
