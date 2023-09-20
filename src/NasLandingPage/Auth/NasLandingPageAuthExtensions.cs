using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using NasLandingPage.Exceptions;

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
    yield return commaString[start..];
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
        options.Events.OnRedirectToAccessDenied = async (context) =>
        {
          context.Response.ContentType = "application/json";
          context.Response.StatusCode = 403;
          await using var sw = new StreamWriter(context.Response.Body);
          await sw.WriteAsync("{\"error\":\"Access denied!\"}");
        };
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.SlidingExpiration = true;
      })
      .AddOAuth<GoogleOptions, NlpGoogleHandler>(GoogleDefaults.AuthenticationScheme, GoogleDefaults.DisplayName, options =>
      {
        options.CallbackPath = new PathString("/api/signin-google");
        var configurationSection = config.GetSection("GoogleAuth");
        if (!configurationSection.Exists())
          throw new NlpException("Failed to find 'GoogleAuth' configuration section");

        options.ClientId = configurationSection["ClientId"];
        options.ClientSecret = configurationSection["Secret"];
        var originalOnRedirectToAuthorizationEndpoint = options.Events.OnRedirectToAuthorizationEndpoint;
        options.Events.OnRedirectToAuthorizationEndpoint = async context =>
        {
          if (Accepts(context.Request.Headers.Accept, "text", "html"))
            await originalOnRedirectToAuthorizationEndpoint(context);
          else
          {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 401;
            await using var sw = new StreamWriter(context.Response.Body);
            await sw.WriteAsync($"{{\"redirectTo\":\"{context.RedirectUri}\"}}");
          }
        };
      });

    return services;
  }
}
