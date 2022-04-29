using Newtonsoft.Json;

namespace NasLandingPage.Common.Models.Responses
{
  public class CommandResponse
  {
    [JsonProperty("command")]
    public string Command { get; set; } = string.Empty;

    [JsonProperty("success")]
    public bool Success { get; set; } = false;

    [JsonProperty("messages")]
    public string[] Messages { get; set; } = Array.Empty<string>();

    [JsonProperty("runningTime")]
    public TimeSpan RunningTime { get; set; } = TimeSpan.Zero;
  }
}
