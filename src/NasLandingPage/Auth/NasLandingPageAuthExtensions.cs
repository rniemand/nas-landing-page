using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication;

namespace NasLandingPage.Auth;

public static class NasLandingPageAuthExtensions
{
  private static IEnumerable<string> GetCommaParts(string commaString)
  {
    var start = 0;
    for (var x = 0; x < commaString.Length; x++)
    {
      if (commaString[x] != ',') continue;
      yield return commaString.Substring(start, x - start);
      start = x + 1;
    }
    yield return commaString.Substring(start);
  }

  private static bool Accepts(string acceptsHeader, string mimeType, string mimeSubtype)
  {
    foreach (var acceptPart in GetCommaParts(acceptsHeader))
    {
      if (acceptPart == "*/*")
        return true;
      if (acceptPart.Length == mimeSubtype.Length + 2 && acceptPart.StartsWith("*/") && acceptPart.EndsWith(mimeSubtype))
        return true;
      if (acceptPart.Length != mimeType.Length + mimeSubtype.Length + 1)
        continue;
      if (acceptPart[mimeType.Length] != '/')
        continue;
      if (acceptPart.StartsWith(mimeType) && acceptPart.EndsWith(mimeSubtype))
        return true;
    }

    return false;
  }

  public static IServiceCollection AddNlpAuthentication(this IServiceCollection services)
  {
    var config = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
    services.AddAuthentication(options =>
    {
      options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
      options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
      .AddCookie(options =>
      {
        options.Events.OnRedirectToAccessDenied = async (ctxt) =>
        {
          ctxt.Response.ContentType = "application/json";
          ctxt.Response.StatusCode = 403;
          await using StreamWriter sw = new StreamWriter(ctxt.Response.Body);
          await sw.WriteAsync("{\"error\":\"Access denied!\"}");
        };
      })
      .AddOAuth<GoogleOptions, NlpGoogleHandler>(GoogleDefaults.AuthenticationScheme, GoogleDefaults.DisplayName, options =>
      {
        options.CallbackPath = new PathString("/api/signin-google");
        var configurationSection = config.GetSection("GoogleAuth");
        options.ClientId = configurationSection["ClientId"];
        options.ClientSecret = configurationSection["Secret"];
        Func<RedirectContext<OAuthOptions>, Task> originalOnRedirectToAuthorizationEndpoint = options.Events.OnRedirectToAuthorizationEndpoint;
        options.Events.OnRedirectToAuthorizationEndpoint = async (ctxt) =>
        {
          if (Accepts(ctxt.Request.Headers.Accept, "text", "html"))
            await originalOnRedirectToAuthorizationEndpoint(ctxt);
          else
          {
            ctxt.Response.ContentType = "application/json";
            ctxt.Response.StatusCode = 401;
            await using StreamWriter sw = new StreamWriter(ctxt.Response.Body);
            await sw.WriteAsync($"{{\"redirectTo\":\"{ctxt.RedirectUri}\"}}");
          }
        };
      });

    return services;
  }
}
