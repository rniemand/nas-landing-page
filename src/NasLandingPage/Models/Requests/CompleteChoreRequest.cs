using NasLandingPage.Models.Dto;

namespace NasLandingPage.Models.Requests;

public class CompleteChoreRequest
{
  public HomeChoreDto Chore { get; set; } = null!;
  public int CompletedBy { get; set; }
}
