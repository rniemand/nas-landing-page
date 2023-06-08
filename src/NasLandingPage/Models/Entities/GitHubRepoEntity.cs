namespace NasLandingPage.Models.Entities;

public class GitHubRepoEntity
{
  public long RepoID { get; set; }
  public long RepoSize { get; set; }
  public bool IsTemplate { get; set; }
  public bool IsPrivate { get; set; }
  public bool IsFork { get; set; }
  public bool HasIssues { get; set; }
  public bool HasWiki { get; set; }
  public bool HasDownloads { get; set; }
  public bool HasPages { get; set; }
  public bool IsArchived { get; set; }
  public int ForksCount { get; set; }
  public int StargazersCount { get; set; }
  public int OpenIssuesCount { get; set; }
  public int SubscribersCount { get; set; }
  public DateTimeOffset? PushedAt { get; set; }
  public DateTimeOffset CreatedAt { get; set; }
  public DateTimeOffset UpdatedAt { get; set; }
  public string License { get; set; } = string.Empty;
  public string DefaultBranch { get; set; } = string.Empty;
  public string GitUrl { get; set; } = string.Empty;
  public string SshUrl { get; set; } = string.Empty;
  public string Name { get; set; } = string.Empty;
  public string FullName { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
}
