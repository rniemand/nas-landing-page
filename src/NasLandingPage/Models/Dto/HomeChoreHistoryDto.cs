namespace NasLandingPage.Models.Dto;

public class HomeChoreHistoryDto
{
  public long ChoreHistoryId { get; set; }
  public int ChoreId { get; set; }
  public int UserId { get; set; }
  public int Points { get; set; }
  public DateOnly DateAdded { get; set; } = DateOnly.MinValue;
  public DateOnly? DateClaimed { get; set; }
}
