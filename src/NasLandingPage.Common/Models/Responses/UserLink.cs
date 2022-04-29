using Newtonsoft.Json;

namespace NasLandingPage.Common.Models.Responses;

public class UserLink
{
  [JsonProperty("id")]
  public Guid LinkId { get; set; } = Guid.Empty;

  [JsonProperty("enabled")]
  public bool Enabled { get; set; } = false;

  [JsonProperty("name")]
  public string Name { get; set; } = string.Empty;

  [JsonProperty("url")]
  public string Url { get; set; } = string.Empty;
}
