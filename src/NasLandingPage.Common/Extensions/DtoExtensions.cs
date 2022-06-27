using NasLandingPage.Common.Database.Entities;
using NasLandingPage.Common.Models.Responses;

namespace NasLandingPage.Common.Extensions;

public static class DtoExtensions
{
  public static UserLink ToDto(this UserLinkEntity entity) =>
    new()
    {
      Category = entity.LinkCategory,
      FollowCount = entity.FollowCount,
      Image = entity.LinkImage,
      LinkId = entity.LinkId,
      Name = entity.LinkName,
      Order = entity.LinkOrder,
      Url = entity.LinkUrl
    };

  public static List<UserLink> ToDtoList(this List<UserLinkEntity> entities) =>
    entities.Count == 0 ? new List<UserLink>() : entities.Select(x => x.ToDto()).ToList();
}
