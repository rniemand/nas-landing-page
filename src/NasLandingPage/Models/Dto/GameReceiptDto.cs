using NasLandingPage.Models.Entities;

namespace NasLandingPage.Models.Dto;

public class GameReceiptDto
{
  public int ReceiptID { get; set; }
  public string Store { get; set; } = string.Empty;
  public string ReceiptNumber { get; set; } = string.Empty;
  public DateTime ReceiptDate { get; set; }
  public string ReceiptName { get; set; } = string.Empty;
  public string ReceiptUrl { get; set; } = string.Empty;
  public bool ReceiptScanned { get; set; }

  public static GameReceiptDto FromEntity(GameReceiptEntity entity) => new()
  {
    ReceiptDate = entity.ReceiptDate,
    ReceiptNumber = entity.ReceiptNumber,
    Store = entity.Store,
    ReceiptName = entity.ReceiptName,
    ReceiptUrl = entity.ReceiptUrl,
    ReceiptScanned = entity.ReceiptScanned,
    ReceiptID = entity.ReceiptID,
  };

  public GameReceiptEntity ToEntity() => new()
  {
    ReceiptDate = ReceiptDate,
    ReceiptNumber = ReceiptNumber,
    Store = Store,
    ReceiptName = ReceiptName,
    ReceiptUrl = ReceiptUrl,
    ReceiptScanned = ReceiptScanned,
    ReceiptID = ReceiptID,
  };
}
