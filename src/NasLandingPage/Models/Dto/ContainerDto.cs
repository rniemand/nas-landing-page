using NasLandingPage.Models.Entities;

namespace NasLandingPage.Models.Dto;

public class ContainerDto
{
  public int ContainerId { get; set; }
  public int ShelfNumber { get; set; }
  public int ShelfLevel { get; set; }
  public int ShelfRow { get; set; }
  public int ShelfRowPosition { get; set; }
  public int ItemCount { get; set; }
  public string ContainerLabel { get; set; } = string.Empty;
  public string ContainerName { get; set; } = string.Empty;
  public string Notes { get; set; } = string.Empty;

  public static ContainerDto FromEntity(ContainerEntity entity) => new()
  {
    ContainerId = entity.ContainerId,
    ContainerLabel = entity.ContainerLabel,
    ContainerName = entity.ContainerName,
    Notes = entity.Notes,
    ItemCount = entity.ItemCount,
    ShelfLevel = entity.ShelfLevel,
    ShelfNumber = entity.ShelfNumber,
    ShelfRow = entity.ShelfRow,
    ShelfRowPosition = entity.ShelfRowPosition,
  };

  public ContainerEntity AsEntity() => new()
  {
    ShelfNumber = ShelfNumber,
    ShelfLevel = ShelfLevel,
    ShelfRowPosition = ShelfRowPosition,
    ItemCount = ItemCount,
    ShelfRow = ShelfRow,
    ContainerLabel = ContainerLabel,
    ContainerId = ContainerId,
    ContainerName = ContainerName,
    Notes = Notes,
  };
}
