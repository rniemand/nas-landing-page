using System.Security.Claims;

namespace NasLandingPage.Models.Responses;

public class WhoAmIResponse
{
  public string? Name { get; set; }
  public string? Email { get; set; }
  public bool SignedIn { get; set; }
  public Dictionary<string, string>? Claims { get; set; }

  public WhoAmIResponse(ClaimsPrincipal user, bool includeClaims)
  {
    if (user.Identity is null)
      return;

    if (includeClaims)
      Claims = new Dictionary<string, string>();

    foreach (var c in user.Claims)
    {
      switch (c.Type)
      {
        case ClaimTypes.Name: Name = c.Value; break;
        case ClaimTypes.Email: Email = c.Value; break;
        case "NlpPass": SignedIn = true; break;
        default:
        {
          if (includeClaims)
            Claims!.Add(c.Type, c.Value);
          break;
        }
      }

      if (!includeClaims && Name is not null && Email is not null && SignedIn)
        return;
    }
  }
}
