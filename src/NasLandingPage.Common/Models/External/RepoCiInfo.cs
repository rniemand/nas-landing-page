using Newtonsoft.Json;

namespace NasLandingPage.Common.Models.External;

public class RepoCiInfo
{
  [JsonProperty("infoVersion")]
  public string Version { get; set; } = "1";

  [JsonProperty("buildScriptVersion")]
  public string BuildScriptVersion { get; set; } = string.Empty;
}
