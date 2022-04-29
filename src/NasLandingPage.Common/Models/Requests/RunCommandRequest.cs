using Newtonsoft.Json;

namespace NasLandingPage.Common.Models.Requests;

public class RunCommandRequest
{
  [JsonProperty("command")]
  public string Command { get; set; } = string.Empty;

  [JsonProperty("args")]
  public string Arguments { get; set; } = string.Empty;
}
