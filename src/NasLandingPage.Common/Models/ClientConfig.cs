using NasLandingPage.Common.Enums;
using Newtonsoft.Json;

namespace NasLandingPage.Common.Models
{
  public class ClientConfig
  {
    [JsonProperty("columns")]
    public ProjectTableColumn[] Columns { get; set; } = Array.Empty<ProjectTableColumn>();

    [JsonProperty("sonarQubeUrl")]
    public string SonarQubeUrl { get; set; } = string.Empty;

    [JsonProperty("badges")]
    public string[] Badges { get; set; } = Array.Empty<string>();
  }
}
