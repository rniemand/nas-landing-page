using Newtonsoft.Json;

namespace NasLandingPage.Common.Models.Responses.Projects;

public class ProjectFolderInfo
{
  [JsonIgnore]
  public bool HasDirectorySrc => !string.IsNullOrWhiteSpace(Src);

  [JsonProperty("src")]
  public string Src { get; set; } = string.Empty;




  [JsonProperty("test")]
  public bool Test { get; set; } = false;

  [JsonProperty("docs")]
  public bool Docs { get; set; } = false;

  [JsonProperty("build")]
  public bool Build { get; set; } = false;

  [JsonProperty("dotGithub")]
  public bool DotGithub { get; set; } = false;
}
