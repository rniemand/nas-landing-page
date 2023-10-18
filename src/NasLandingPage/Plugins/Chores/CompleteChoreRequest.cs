namespace NasLandingPage.Plugins.Chores;

public class CompleteChoreRequest
{
  public HomeChoreDto Chore { get; set; } = null!;
  public int CompletedBy { get; set; }
}
