using NasLandingPage.Common.Enums;
using Newtonsoft.Json;

namespace NasLandingPage.Common.Models.Responses.Projects;

public class RepositoryInfo
{
  [JsonProperty("type")]
  public RepoType RepoType { get; set; } = RepoType.Unknown;

  [JsonProperty("url")]
  public string HtmlUrl { get; set; } = string.Empty;

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

  [JsonProperty("forks")]
  public int ForksCount { get; set; } = 0;

  [JsonProperty("fullName")]
  public string FullName { get; set; } = string.Empty;

  [JsonProperty("gitUrl")]
  public string GitUrl { get; set; } = string.Empty;

  [JsonProperty("openIssues")]
  public int OpenIssuesCount { get; set; } = 0;

  [JsonProperty("sshUrl")]
  public string SshUrl { get; set; } = string.Empty;

  [JsonProperty("apiUrl")]
  public string ApiUrl { get; set; } = string.Empty;

  [JsonProperty("size")]
  public long Size { get; set; } = 0;
}
