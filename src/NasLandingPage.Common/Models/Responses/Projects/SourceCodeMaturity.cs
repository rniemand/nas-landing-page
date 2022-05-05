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

  [JsonProperty("buildScript")]
  public string BuildScript { get; set; } = string.Empty;




  [JsonProperty("buildScriptVersion")]
  public string BuildScriptVersion { get; set; } = "0";

  [JsonProperty("testScripts")]
  public List<string> TestScripts { get; set; } = new();
  
  [JsonProperty("workFlows")]
  public string[] WorkFlows { get; set; } = Array.Empty<string>();
}
