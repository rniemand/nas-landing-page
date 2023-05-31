namespace NasLandingPage.Models.Entities;

public class UserLinkEntity
{
  public int LinkID { get; set; }
  public bool Deleted { get; set; }
  public int LinkOrder { get; set; }
  public int FollowCount { get; set; }
  public DateTime DateAddedUtc { get; set; }
  public DateTime DateUpdatedUtc { get; set; }
  public DateTime DateLastFollowedUtc { get; set; }
  public string LinkName { get; set; } = string.Empty;
  public string LinkCategory { get; set; } = string.Empty;
  public string LinkUrl { get; set; } = string.Empty;
  public string LinkImage { get; set; } = string.Empty;
}
