using NasLandingPage.Models.Entities;

namespace NasLandingPage.Models.Dto;

public class UserLinkDto
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

  public static UserLinkDto FromEntity(UserLinkEntity entity) => new()
  {
    LinkID = entity.LinkID,
    LinkName = entity.LinkName,
    LinkCategory = entity.LinkCategory,
    LinkUrl = entity.LinkUrl,
    LinkImage = entity.LinkImage,
    LinkOrder = entity.LinkOrder,
    FollowCount = entity.FollowCount,
    DateAddedUtc = entity.DateAddedUtc,
    DateUpdatedUtc = entity.DateUpdatedUtc
  };
}
