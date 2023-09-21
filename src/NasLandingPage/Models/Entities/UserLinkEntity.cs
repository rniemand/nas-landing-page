namespace NasLandingPage.Models.Entities;

public class UserLinkEntity
{
  public int LinkId { get; set; }
  public int UserID { get; set; }
  public bool Deleted { get; set; }
  public int LinkOrder { get; set; }
  public int FollowCount { get; set; }
  public DateTimeOffset DateAddedUtc { get; set; } = DateTimeOffset.MinValue;
  public DateTimeOffset DateUpdatedUtc { get; set; } = DateTimeOffset.MinValue;
  public DateTimeOffset DateLastFollowedUtc { get; set; } = DateTimeOffset.MinValue;
  public string LinkName { get; set; } = null!;
  public string LinkCategory { get; set; } = null!;
  public string LinkUrl { get; set; } = null!;
  public string LinkImage { get; set; } = null!;
}
