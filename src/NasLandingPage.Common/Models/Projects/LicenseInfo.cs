using Newtonsoft.Json;

namespace NasLandingPage.Common.Models.Projects;

public class LicenseInfo
{
  [JsonProperty("name")]
  public string Name { get; set; } = string.Empty;

  [JsonProperty("url")]
  public string Url { get; set; } = string.Empty;
}