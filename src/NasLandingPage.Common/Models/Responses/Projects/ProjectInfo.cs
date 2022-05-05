using NasLandingPage.Common.Models.External;
using Newtonsoft.Json;

namespace NasLandingPage.Common.Models.Responses.Projects;

public class ProjectInfo
{
  [JsonProperty("name")]
  public string Name { get; set; } = string.Empty;

  [JsonProperty("description")]
  public string Description { get; set; } = string.Empty;

  [JsonProperty("lastUpdated")]
  public DateTimeOffset LastUpdated { get; set; } = DateTimeOffset.MinValue;

  [JsonProperty("metadata")]
  public ProjectInfoMetadata Metadata { get; set; } = new();

  [JsonProperty("repo")]
  public RepositoryInfo Repo { get; set; } = new();

  [JsonProperty("sonarQube")]
  public SonarQubeInfo SonarQube { get; set; } = new();

  [JsonProperty("scm")]
  public SourceCodeMaturity Scm { get; set; } = new();
  
  [JsonProperty("languages")]
  public string[] Languages { get; set; } = Array.Empty<string>();

  [JsonProperty("ciInfo")]
  public RepoCiInfo CiInfo { get; set; } = new();
}
