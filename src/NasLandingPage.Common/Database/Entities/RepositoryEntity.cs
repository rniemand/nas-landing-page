using NasLandingPage.Common.Enums;

namespace NasLandingPage.Common.Database.Entities;

public class RepositoryEntity
{
  public int ProjectRepoId { get; set; }
  public int ProjectId { get; set; }
  public int ForkCount { get; set; }
  public int OpenIssueCount { get; set; }
  public bool Deleted { get; set; }
  public bool IsPublic { get; set; }
  public RepoType RepoType { get; set; } = RepoType.Unknown;
  public long RepoId { get; set; }
  public long RepoSize { get; set; }
  public DateTime LastUpdatedUtc { get; set; } = DateTime.MinValue.ToUniversalTime();
  public DateTime DateAddedUtc { get; set; } = DateTime.MinValue.ToUniversalTime();
  public string DefaultBranch { get; set; } = "master";
  public string FullName { get; set; } = string.Empty;
  public string RepoUrl { get; set; } = string.Empty;
  public string HtmlUrl { get; set; } = string.Empty;
  public string CiCdUrl { get; set; } = string.Empty;
  public string GitUrl { get; set; } = string.Empty;
  public string SshUrl { get; set; } = string.Empty;
  public string ApiUrl { get; set; } = string.Empty;
}
