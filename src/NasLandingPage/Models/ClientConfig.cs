using NasLandingPage.Enums;
using Newtonsoft.Json;

namespace NasLandingPage.Models
{
  public class ClientConfig
  {
    [JsonProperty("columns")]
    public ProjectTableColumn[] Columns { get; set; } = Array.Empty<ProjectTableColumn>();
  }
}
