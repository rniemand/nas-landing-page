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

  [JsonProperty("testScript")]
  public string TestScript { get; set; } = string.Empty;
  
  [JsonProperty("ciVersion")]
  public string CiVersion { get; set; } = "0";
  
  [JsonProperty("ciFlows")]
  public string[] CiFlows { get; set; } = Array.Empty<string>();

  [JsonProperty("srcDirectory")]
  public string SrcDirectory { get; set; } = string.Empty;

  [JsonProperty("testDirectory")]
  public string TestDirectory { get; set; } = string.Empty;

  [JsonProperty("docsDirectory")]
  public string DocsDirectory { get; set; } = string.Empty;

  [JsonProperty("buildDirectory")]
  public string BuildDirectory { get; set; } = string.Empty;
}
