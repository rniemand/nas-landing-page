using Newtonsoft.Json;

namespace NasLandingPage.Common.Models.Responses.Projects;

public class SourceCodeMaturityInfo
{
  [JsonProperty("hasReadme")]
  public bool HasReadme { get; set; } = false;

  [JsonProperty("hasGitAttributes")]
  public bool HasGitAttributes { get; set; } = false;

  [JsonProperty("hasPrTemplate")]
  public bool HasPrTemplate { get; set; } = false;

  [JsonProperty("prTemplate")]
  public string PrTemplate { get; set; } = string.Empty;

  [JsonProperty("hasEditorConfig")]
  public bool HasEditorConfig { get; set; } = false;

  [JsonProperty("editorConfig")]
  public string EditorConfig { get; set; } = string.Empty;

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
