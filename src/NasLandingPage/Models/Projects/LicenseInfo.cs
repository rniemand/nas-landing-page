using Newtonsoft.Json;

namespace NasLandingPage.Models;

public class LicenseInfo
{
  [JsonProperty("name")]
  public string Name { get; set; } = string.Empty;

  [JsonProperty("url")]
  public string Url { get; set; } = string.Empty;
}
