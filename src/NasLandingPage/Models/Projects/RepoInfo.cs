using NasLandingPage.Enums;
using Newtonsoft.Json;

namespace NasLandingPage.Models;

public class RepoInfo
{
  [JsonProperty("type")]
  public RepoType RepoType { get; set; } = RepoType.Unknown;

  [JsonProperty("url")]
  public string RepoUrl { get; set; } = string.Empty;

  [JsonProperty("cicd")]
  public string CiCd { get; set; } = string.Empty;

  [JsonProperty("isPublic")]
  public bool IsPublic { get; set; } = true;
}