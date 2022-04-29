using NasLandingPage.Common.Models.Responses.Projects;
using Newtonsoft.Json;

namespace NasLandingPage.Common.Models.Responses;

public class ProjectInfo
{
  [JsonProperty("name")]
  public string Name { get; set; } = string.Empty;

  [JsonProperty("description")]
  public string Description { get; set; } = string.Empty;

  [JsonProperty("metadata")]
  public ProjectInfoMetadata Metadata { get; set; } = new();

  [JsonProperty("repo")]
  public RepoInfo Repo { get; set; } = new();

  [JsonProperty("sonarQube")]
  public SonarQubeInfo SonarQube { get; set; } = new();

  [JsonProperty("scm")]
  public SourceCodeMaturityInfo Scm { get; set; } = new();

  [JsonProperty("folders")]
  public ProjectFolderInfo Folders { get; set; } = new();

  [JsonProperty("license")]
  public LicenseInfo License { get; set; } = new();

  [JsonProperty("languages")]
  public string[] Languages { get; set; } = Array.Empty<string>();
}
