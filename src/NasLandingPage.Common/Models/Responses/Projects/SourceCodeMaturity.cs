using Newtonsoft.Json;

namespace NasLandingPage.Common.Models.Responses.Projects;

public class SourceCodeMaturity
{
  [JsonProperty("hasReadme")]
  public bool HasReadme { get; set; } = false;

  [JsonProperty("readme")]
  public string Readme { get; set; } = string.Empty;

  [JsonProperty("hasGitAttributes")]
  public bool HasGitAttributes { get; set; } = false;

  [JsonProperty("gitAttributes")]
  public string GitAttributes { get; set; } = string.Empty;

  [JsonProperty("hasPrTemplate")]
  public bool HasPrTemplate { get; set; } = false;
  
  [JsonProperty("hasEditorConfig")]
  public bool HasEditorConfig { get; set; } = false;

  [JsonProperty("editorConfig")]
  public string EditorConfig { get; set; } = string.Empty;
  
  [JsonProperty("hasBuildScript")]
  public bool HasBuildScript { get; set; } = false;

  [JsonProperty("hasTestScript")]
  public bool HasTestScript { get; set; } = false;

  [JsonProperty("hasCiInfo")]
  public bool HasCiInfo { get; set; } = false;

  [JsonProperty("buildScriptVersion")]
  public string BuildScriptVersion { get; set; } = "0";

  [JsonProperty("buildScripts")]
  public List<string> BuildScripts { get; set; } = new();

  [JsonProperty("testScripts")]
  public List<string> TestScripts { get; set; } = new();

  [JsonProperty("ciInfo")]
  public string CiInfoPath { get; set; } = string.Empty;

  [JsonProperty("workFlows")]
  public string[] WorkFlows { get; set; } = Array.Empty<string>();
}
