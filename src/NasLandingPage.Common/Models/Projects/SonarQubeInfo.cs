using Newtonsoft.Json;

namespace NasLandingPage.Common.Models.Projects;

public class SonarQubeInfo
{
  [JsonProperty("url")]
  public string Url { get; set; } = string.Empty;

  [JsonProperty("id")]
  public string ProjectId { get; set; } = string.Empty;

  [JsonProperty("tokenBadge")]
  public string BadgeToken { get; set; } = string.Empty;

  [JsonProperty("badges")]
  public Dictionary<string, string> Badges { get; set; } = new();
}
