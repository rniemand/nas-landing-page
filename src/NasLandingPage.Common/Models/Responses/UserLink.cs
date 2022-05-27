using Newtonsoft.Json;

namespace NasLandingPage.Common.Models.Responses;

public class UserLink
{
  [JsonProperty("id")]
  public string LinkId { get; set; } = string.Empty;
  
  [JsonProperty("name")]
  public string Name { get; set; } = string.Empty;

  [JsonProperty("url")]
  public string Url { get; set; } = string.Empty;

  [JsonProperty("order")]
  public int Order { get; set; } = 1024;

  [JsonProperty("image")]
  public string Image { get; set; } = string.Empty;

  [JsonProperty("followCount")]
  public int FollowCount { get; set; } = 0;

  [JsonProperty("category")]
  public string Category { get; set; } = "none";
}
