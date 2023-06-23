using NasLandingPage.Models.Entities;

namespace NasLandingPage.Models.Dto;

public class BasicGameInfoDto
{
  public long GameID { get; set; }
  public string GameName { get; set; } = string.Empty;
  public int PlatformID { get; set; }
  public int LocationID { get; set; }
  public string GameCaseLocation { get; set; } = string.Empty;
  public bool HasGameBox { get; set; }
  public int GameRating { get; set; }
  public string ImagePath { get; set; } = string.Empty;
  public string LocationName { get; set; } = string.Empty;
  public string PlatformName { get; set; } = string.Empty;
  public bool HasProtection { get; set; }
  public string Store { get; set; } = string.Empty;
  public string ReceiptNumber { get; set; } = string.Empty;
  public double GamePrice { get; set; }
  public DateTime? ReceiptDate { get; set; }
  public bool GameSold { get; set; }
  public bool HasReceipt { get; set; }
  public string ReceiptName { get; set; } = string.Empty;
  public bool ReceiptScanned { get; set; }
  public int ReceiptID { get; set; }
  // Dynamic properties
  public string SearchTerm { get; set; } = string.Empty;

  public static BasicGameInfoDto FromEntity(BasicGameInfoEntity entity) => new()
  {
    ImagePath = entity.ImagePath,
    GameCaseLocation = entity.GameCaseLocation,
    GameID = entity.GameID,
    GameName = entity.GameName,
    HasGameBox = entity.HasGameBox,
    LocationID = entity.LocationID,
    LocationName = entity.LocationName,
    PlatformID = entity.PlatformID,
    PlatformName = entity.PlatformName,
    GameRating = entity.GameRating,
    GamePrice = entity.GamePrice,
    ReceiptDate = entity.ReceiptDate,
    HasProtection = entity.HasProtection,
    ReceiptNumber = entity.ReceiptNumber,
    Store = entity.Store,
    GameSold = entity.GameSold,
    HasReceipt = entity.HasReceipt,
    ReceiptName = entity.ReceiptName,
    ReceiptScanned = entity.ReceiptScanned,
    ReceiptID = entity.ReceiptID,
    SearchTerm = $"{entity.GameCaseLocation}|{entity.GameName}|{entity.LocationName}|{entity.PlatformName}|{entity.ReceiptNumber}|{entity.Store}|{entity.ReceiptName}|{entity.ReceiptID}".ToLower()
  };

  public BasicGameInfoEntity ToEntity() => new()
  {
    GamePrice = GamePrice,
    GameCaseLocation = GameCaseLocation,
    GameID = GameID,
    GameName = GameName,
    GameSold = GameSold,
    HasGameBox = HasGameBox,
    LocationID = LocationID,
    LocationName = LocationName,
    PlatformID = PlatformID,
    PlatformName = PlatformName,
    GameRating = GameRating,
    HasProtection = HasProtection,
    ImagePath = ImagePath,
    ReceiptNumber = ReceiptNumber,
    ReceiptDate = ReceiptDate,
    Store = Store,
    HasReceipt = HasReceipt,
    ReceiptName = ReceiptName,
    ReceiptScanned = ReceiptScanned,
    ReceiptID = ReceiptID,
  };
}
