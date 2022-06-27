namespace NasLandingPage.Common.Database.Entities;

public class UserLinkEntity
{
  public int LinkId { get; set; }
  public bool Deleted { get; set; }
  public DateTime DateAddedUtc { get; set; } = DateTime.MinValue.ToUniversalTime();
  public DateTime DateUpdatedUtc { get; set; } = DateTime.MinValue.ToUniversalTime();
  public DateTime DateLastFollowedUtc { get; set; } = DateTime.MinValue.ToUniversalTime();
  public string LinkName { get; set; } = string.Empty;
  public string LinkUrl { get; set; } = string.Empty;
  public int LinkOrder { get; set; } = 1024;
  public string LinkImage { get; set; } = string.Empty;
  public int FollowCount { get; set; } = 0;
  public string LinkCategory { get; set; } = string.Empty;
}
