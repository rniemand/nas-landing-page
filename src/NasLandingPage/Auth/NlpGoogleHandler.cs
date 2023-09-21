using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace NasLandingPage.Auth;

public class NlpGoogleHandler : GoogleHandler
{
  public NlpGoogleHandler(IOptionsMonitor<GoogleOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
    : base(options, logger, encoder, clock)
  { }

  protected override string BuildChallengeUrl(AuthenticationProperties properties, string redirectUri)
  {
    if (properties.RedirectUri is null || properties.RedirectUri.StartsWith("/api"))
      properties.RedirectUri = Context.Request.Headers["Referer"].ToString();
    return base.BuildChallengeUrl(properties, redirectUri);
  }
}
