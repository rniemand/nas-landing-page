using Newtonsoft.Json;

namespace NasLandingPage.Common.Models.Projects;

public class ProjectFolderInfo
{
  [JsonProperty("src")]
  public bool Src { get; set; } = false;

  [JsonProperty("tests")]
  public bool Tests { get; set; } = false;

  [JsonProperty("docs")]
  public bool Docs { get; set; } = false;

  [JsonProperty("build")]
  public bool Build { get; set; } = false;
}
