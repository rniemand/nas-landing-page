using NasLandingPage.Common.Enums;
using Newtonsoft.Json;

namespace NasLandingPage.Common.Models.Responses
{
  public class ClientConfig
  {
    [JsonProperty("columns")]
    public TableColumn[] Columns { get; set; } = Array.Empty<TableColumn>();

    [JsonProperty("sonarQubeUrl")]
    public string SonarQubeUrl { get; set; } = string.Empty;

    [JsonProperty("badges")]
    public string[] Badges { get; set; } = Array.Empty<string>();
  }
}
