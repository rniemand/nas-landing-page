using NasLandingPage.Config;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Logging;

namespace NasLandingPage.Services
{
  public class ProjectInfo
  {
    public string Name { get; set; } = string.Empty;
  }

  public interface IProjectsService
  {
    List<ProjectInfo> GetAll();
  }

  public class ProjectsService : IProjectsService
  {
    private readonly ILoggerAdapter<ProjectsService> _logger;
    private readonly IDirectoryAbstraction _directory;
    private readonly IEnvironmentAbstraction _environment;
    private readonly IFileAbstraction _file;
    private readonly NasLandingPageConfig _config;
    private readonly string _dataDir;

    public ProjectsService(IServiceProvider serviceProvider)
    {
      _logger = serviceProvider.GetRequiredService<ILoggerAdapter<ProjectsService>>();
      _directory = serviceProvider.GetRequiredService<IDirectoryAbstraction>();
      _environment = serviceProvider.GetRequiredService<IEnvironmentAbstraction>();
      _file = serviceProvider.GetRequiredService<IFileAbstraction>();

      _config = GetConfig(serviceProvider);
      _dataDir = GenerateDataDirPath();

      EnsureDataDirExists();
    }

    private static NasLandingPageConfig GetConfig(IServiceProvider serviceProvider)
    {
      var configuration = serviceProvider.GetRequiredService<IConfiguration>();
      var boundConfig = new NasLandingPageConfig();
      var configSection = configuration.GetSection("NasLandingPage");

      if (!configSection.Exists())
        return boundConfig;

      configSection.Bind(boundConfig);
      return boundConfig;
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
      if(_directory.Exists(_dataDir))
        return;

      _directory.CreateDirectory(_dataDir);
    }

    public List<ProjectInfo> GetAll()
    {
      return new List<ProjectInfo>();
    }
  }
}
