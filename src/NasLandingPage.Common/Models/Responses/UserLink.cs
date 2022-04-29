using Newtonsoft.Json;

namespace NasLandingPage.Common.Models.Responses;

public class UserLink
{
  [JsonProperty("id")]
  public Guid LinkId { get; set; } = Guid.Empty;
  
  [JsonProperty("name")]
  public string Name { get; set; } = string.Empty;

  [JsonProperty("url")]
  public string Url { get; set; } = string.Empty;

  [JsonProperty("order")]
  public int Order { get; set; } = 1024;
}
