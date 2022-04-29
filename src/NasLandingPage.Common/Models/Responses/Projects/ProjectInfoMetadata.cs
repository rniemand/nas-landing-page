using Newtonsoft.Json;

namespace NasLandingPage.Common.Models.Responses.Projects;

public class ProjectInfoMetadata
{
  [JsonProperty("fileName")]
  public string FileName { get; set; } = string.Empty;

  [JsonProperty("fileNameWithoutExtension")]
  public string FileNameWithoutExtension { get; set; } = string.Empty;
}
