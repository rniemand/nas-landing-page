using Newtonsoft.Json;

namespace NasLandingPage.Common.Models;

public class BasicCredentials
{
  [JsonProperty("user")]
  public string Username { get; set; } = string.Empty;

  [JsonProperty("pass")]
  public string Password { get; set; } = string.Empty;

  [JsonProperty("name")]
  public string CredentialsName { get; set; } = string.Empty;
}
