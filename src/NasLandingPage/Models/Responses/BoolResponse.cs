namespace NasLandingPage.Models.Responses;

public class BoolResponse
{
  public bool Success { get; set; } = true;
  public string? Error { get; set; }

  public BoolResponse() { }

  public BoolResponse AsError(string message)
  {
    Success = false;
    Error = message;
    return this;
  }
}
