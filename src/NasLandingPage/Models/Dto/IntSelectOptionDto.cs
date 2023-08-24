using NasLandingPage.Models.Entities;

namespace NasLandingPage.Models.Dto;

public class IntSelectOptionDto
{
  public int Value { get; set; }
  public string Title { get; set; } = string.Empty;

  public static IntSelectOptionDto FromEntity(IntSelectOptionEntity entity) => new()
  {
    Title = entity.Title,
    Value = entity.Value,
  };
}
