using Newtonsoft.Json;

namespace NasLandingPage.Common.Models.Responses.Projects;

public class SourceCodeMaturityInfo
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
