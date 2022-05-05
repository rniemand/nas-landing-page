using Newtonsoft.Json;

namespace NasLandingPage.Common.Models.Responses.Projects;

public class SourceCodeMaturity
{
  [JsonProperty("readme")]
  public string Readme { get; set; } = string.Empty;

  [JsonProperty("gitAttributes")]
  public string GitAttributes { get; set; } = string.Empty;

  [JsonProperty("prTemplate")]
  public string PrTemplate { get; set; } = string.Empty;

  [JsonProperty("editorConfig")]
  public string EditorConfig { get; set; } = string.Empty;

  [JsonProperty("ciInfo")]
  public string CiInfo { get; set; } = string.Empty;




  [JsonProperty("hasBuildScript")]
  public bool HasBuildScript { get; set; } = false;

  [JsonProperty("hasTestScript")]
  public bool HasTestScript { get; set; } = false;

  [JsonProperty("buildScriptVersion")]
  public string BuildScriptVersion { get; set; } = "0";

  [JsonProperty("buildScripts")]
  public List<string> BuildScripts { get; set; } = new();

  [JsonProperty("testScripts")]
  public List<string> TestScripts { get; set; } = new();
  
  [JsonProperty("workFlows")]
  public string[] WorkFlows { get; set; } = Array.Empty<string>();
}
