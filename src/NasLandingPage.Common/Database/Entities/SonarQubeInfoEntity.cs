namespace NasLandingPage.Common.Database.Entities;

public class SonarQubeInfoEntity
{
  public int SonarQubeInfoId { get; set; }
  public int ProjectId { get; set; }
  public bool Deleted { get; set; }
  public string SonarQubeId { get; set; } = string.Empty;
  public string BadgeToken { get; set; } = string.Empty;
  public string SonarQubeUrl { get; set; } = string.Empty;
  public string BadgesJson { get; set; } = string.Empty;
}
