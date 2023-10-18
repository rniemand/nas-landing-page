using NasLandingPage.Models.Entities;

namespace NasLandingPage.Models.Dto;

public class UserLinkDto
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

  public static UserLinkDto FromEntity(UserLinkEntity entity) => new()
  {
    DateAdded = entity.DateAdded,
    DateUpdated = entity.DateUpdated,
    LinkName = entity.LinkName,
    LinkCategory = entity.LinkCategory,
    LinkUrl = entity.LinkUrl,
    LinkImage = entity.LinkImage,
    LinkOrder = entity.LinkOrder,
    FollowCount = entity.FollowCount,
    DateLastFollowed = entity.DateLastFollowed,
    Deleted = entity.Deleted,
    LinkId = entity.LinkId,
    UserID = entity.UserID,
  };
}
