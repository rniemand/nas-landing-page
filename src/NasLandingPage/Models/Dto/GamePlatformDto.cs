using NasLandingPage.Models.Entities;

namespace NasLandingPage.Models.Dto;

public class GamePlatformDto
{
  public int PlatformID { get; set; }
  public string PlatformName { get; set; } = string.Empty;

  public static GamePlatformDto FromEntity(GamePlatformEntity entity) => new()
  {
    PlatformID = entity.PlatformID,
    PlatformName = entity.PlatformName,
  };
}
