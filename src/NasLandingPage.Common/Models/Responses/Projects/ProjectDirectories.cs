using Newtonsoft.Json;

namespace NasLandingPage.Common.Models.Responses.Projects;

public class ProjectDirectories
{
  [JsonProperty("src")]
  public string Src { get; set; } = string.Empty;

  [JsonProperty("test")]
  public string Test { get; set; } = string.Empty;
  
  [JsonProperty("docs")]
  public string Docs { get; set; } = string.Empty;

  [JsonProperty("build")]
  public string Build { get; set; } = string.Empty;
}
