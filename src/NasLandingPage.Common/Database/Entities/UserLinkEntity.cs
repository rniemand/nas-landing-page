namespace NasLandingPage.Common.Database.Entities;

public class UserLinkEntity
{
  public int Id { get; set; }
  public DateTime DateAddedUtc { get; set; } = DateTime.MinValue.ToUniversalTime();
  public DateTime DateUpdatedUtc { get; set; } = DateTime.MinValue.ToUniversalTime();
  public string Name { get; set; } = string.Empty;
  public string Url { get; set; } = string.Empty;
  public int Order { get; set; } = 1024;
  public string Image { get; set; } = string.Empty;
  public int FollowCount { get; set; } = 0;
  public string Category { get; set; } = string.Empty;
  public string SubCategory { get; set; } = string.Empty;
}
