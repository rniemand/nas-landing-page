namespace NasLandingPage.Models.Responses;

public class BoolResponse
{
  public bool Success { get; set; } = true;
  public string? Error { get; set; }

  public BoolResponse AsError(string message)
  {
    Error = message;
    Success = false;
    return this;
  }
}
