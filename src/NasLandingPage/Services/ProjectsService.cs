using NasLandingPage.Config;
using NasLandingPage.Enums;
using Newtonsoft.Json;
using Rn.NetCore.Common.Abstractions;
using Rn.NetCore.Common.Helpers;
using Rn.NetCore.Common.Logging;

namespace NasLandingPage.Services
{
  public class RepoInfo
  {
    [JsonProperty("type")]
    public RepoType RepoType { get; set; } = RepoType.Unknown;

    [JsonProperty("url")]
    public string RepoUrl { get; set; } = string.Empty;

    [JsonProperty("cicd")]
    public string CiCd { get; set; } = string.Empty;

    [JsonProperty("isPublic")]
    public bool IsPublic { get; set; } = true;
  }

  public class SonarQubeInfo
  {
    [JsonProperty("url")]
    public string Url { get; set; } = string.Empty;

    [JsonProperty("id")]
    public string ProjectId { get; set; } = string.Empty;

    [JsonProperty("tokenBadge")]
    public string BadgeToken { get; set; } = string.Empty;

    [JsonProperty("badges")]
    public Dictionary<string, string> Badges { get; set; } = new();
  }

  public class SourceCodeMaturityInfo
  {
    [JsonProperty("hasReadme")]
    public bool HasReadme { get; set; } = false;

    [JsonProperty("hasGitAttributes")]
    public bool HasGitAttributes { get; set; } = false;

    [JsonProperty("hasPrTemplate")]
    public bool HasPrTemplate { get; set; } = false;

    [JsonProperty("hasEditorConfig")]
    public bool HasEditorConfig { get; set; } = false;

    [JsonProperty("hasBuildScripts")]
    public bool HasBuildScripts { get; set; } = false;

    [JsonProperty("buildScriptVersion")]
    public string BuildScriptVersion { get; set; } = "0";

    [JsonProperty("buildScripts")]
    public string[] BuildScripts { get; set; } = Array.Empty<string>();

    [JsonProperty("testScripts")]
    public string[] TestScripts { get; set; } = Array.Empty<string>();

    [JsonProperty("workFlows")]
    public string[] WorkFlows { get; set; } = Array.Empty<string>();
  }
  
  public class ProjectInfo
  {
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("repo")]
    public RepoInfo Repo { get; set; } = new();

    [JsonProperty("sonarQube")]
    public SonarQubeInfo SonarQube { get; set; } = new();

    [JsonProperty("scm")]
    public SourceCodeMaturityInfo Scm { get; set; } = new();

    [JsonProperty("folders")]
    public Dictionary<string, bool> Folders { get; set; } = new();

    [JsonProperty("languages")]
    public string[] Languages { get; set; } = Array.Empty<string>();
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
    private readonly IJsonHelper _jsonHelper;
    private readonly NasLandingPageConfig _config;
    private readonly string _dataDir;

    public ProjectsService(IServiceProvider serviceProvider)
    {
      _logger = serviceProvider.GetRequiredService<ILoggerAdapter<ProjectsService>>();
      _directory = serviceProvider.GetRequiredService<IDirectoryAbstraction>();
      _environment = serviceProvider.GetRequiredService<IEnvironmentAbstraction>();
      _file = serviceProvider.GetRequiredService<IFileAbstraction>();
      _jsonHelper = serviceProvider.GetRequiredService<IJsonHelper>();

      _config = GetConfig(serviceProvider);
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

    private static NasLandingPageConfig GetConfig(IServiceProvider serviceProvider)
    {
      // TODO: [ProjectsService.GetConfig] (TESTS) Add tests
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
      // TODO: [ProjectsService.EnsureDataDirExists] (TESTS) Add tests
      if (_directory.Exists(_dataDir))
        return;

      _directory.CreateDirectory(_dataDir);
    }
  }
}
