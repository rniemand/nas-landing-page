namespace NasLandingPage.Models;

public class NlpUserContext
{
  public int UserId { get; set; }
  public bool Authenticated { get; }
  public string Email { get; set; } = string.Empty;

  public NlpUserContext(bool authenticated)
  {
    Authenticated = authenticated;
  }
}
