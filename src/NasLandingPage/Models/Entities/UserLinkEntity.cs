namespace NasLandingPage.Models.Entities;

public class UserLinkEntity
{
  public int LinkId { get; set; }
  public int UserID { get; set; }
  public bool Deleted { get; set; }
  public int LinkOrder { get; set; }
  public int FollowCount { get; set; }
  public DateTimeOffset DateAdded { get; set; } = DateTimeOffset.MinValue;
  public DateTimeOffset DateUpdated { get; set; } = DateTimeOffset.MinValue;
  public DateTimeOffset DateLastFollowed { get; set; } = DateTimeOffset.MinValue;
  public string LinkName { get; set; } = null!;
  public string LinkCategory { get; set; } = null!;
  public string LinkUrl { get; set; } = null!;
  public string LinkImage { get; set; } = null!;
}
