namespace NasLandingPage.Models.Requests;

public class BasicSearchRequest
{
  public string? Filter { get; set; }
  public string? SubFilter { get; set; }
  public bool IncludeCompletedEntries { get; set; } = false;
}
