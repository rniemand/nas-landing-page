namespace NasLandingPage.Common.Database.Entities;

public class ProjectEntity
{
  public int ProjectId { get; set; }

  public bool Deleted { get; set; }

  public string Language { get; set; } = string.Empty;

  public string ProjectName { get; set; } = string.Empty;

  public string ProjectDescription { get; set; } = string.Empty;

  public DateTime LastUpdatedUtc { get; set; } = DateTime.MinValue.ToUniversalTime();

  public DateTime DateAddedUtc { get; set; } = DateTime.MinValue.ToUniversalTime();
}
