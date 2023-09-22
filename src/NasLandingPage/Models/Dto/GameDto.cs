namespace NasLandingPage.Models.Dto;

public class GameDto
{
  public int GameID { get; set; }
  public int PlatformID { get; set; }
  public int LocationID { get; set; }
  public bool HasGameBox { get; set; }
  public bool HasProtection { get; set; }
  public int GameRating { get; set; }
  public double GamePrice { get; set; }
  public string GameName { get; set; } = null!;
  public string GameCaseLocation { get; set; } = null!;

  // Additional properties
  public string? PlatformName { get; set; }
  public string? LocationName { get; set; }
  public string? ImagePath { get; set; }
  public string? SearchTerm { get; set; }
}
