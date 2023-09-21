using System.Security.Claims;
using NasLandingPage.Models;

namespace NasLandingPage.Extensions;

public static class AuthExtensions
{
  public static NlpUserContext GetNlpUserContext(this ClaimsPrincipal user)
  {
    if (user.Identity is null || !user.Identity.IsAuthenticated)
      return new NlpUserContext(false);

    var response = new NlpUserContext(true)
    {
      Email = user.FindFirstValue(ClaimTypes.Email)
    };

    var userString = user.FindFirstValue("NlpUser");
    if (!string.IsNullOrWhiteSpace(userString))
    {
      var userStringParts = userString.Split(':', StringSplitOptions.RemoveEmptyEntries);
      response.UserId = int.Parse(userStringParts[0]);
    }

    return response;
  }
}
