using NasLandingPage.Common.Enums;
using Newtonsoft.Json;

namespace NasLandingPage.Common.Models;

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

  [JsonProperty("id")]
  public long RepoId { get; set; } = 0;

  [JsonProperty("defaultBranch")]
  public string DefaultBranch { get; set; } = string.Empty;

  [JsonProperty("lastUpdated")]
  public DateTimeOffset LastUpdated { get; set; } = DateTimeOffset.MinValue;
}
