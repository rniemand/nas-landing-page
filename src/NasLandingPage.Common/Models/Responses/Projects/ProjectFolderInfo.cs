using Newtonsoft.Json;

namespace NasLandingPage.Common.Models.Responses.Projects;

public class ProjectFolderInfo
{
  [JsonProperty("src")]
  public bool Src { get; set; } = false;

  [JsonProperty("test")]
  public bool Test { get; set; } = false;

  [JsonProperty("docs")]
  public bool Docs { get; set; } = false;

  [JsonProperty("build")]
  public bool Build { get; set; } = false;

  [JsonProperty("dotGithub")]
  public bool DotGithub { get; set; } = false;
}
