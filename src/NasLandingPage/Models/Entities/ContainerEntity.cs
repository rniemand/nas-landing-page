namespace NasLandingPage.Models.Entities;

public class ContainerEntity
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
}
