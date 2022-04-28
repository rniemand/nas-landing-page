using Newtonsoft.Json;

namespace NasLandingPage.Common.Models;

public class ProjectInfoMetadata
{
  [JsonProperty("fileName")]
  public string FileName { get; set; } = string.Empty;

  [JsonProperty("fileNameWithoutExtension")]
  public string FileNameWithoutExtension { get; set; } = string.Empty;
}
