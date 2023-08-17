using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NasLandingPage.Auth;
using NasLandingPage.Models.Requests;
using NasLandingPage.Models.Responses;
using NasLandingPage.Repos;
using NasLandingPage.Services;
using Newtonsoft.Json;

namespace NasLandingPage.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
  private readonly IAuthService _authService;

  public AuthController(IAuthService authService)
  {
    _authService = authService;
  }

  [HttpGet("whoami")]
  public async Task<WhoAmIResponse> WhoAmI(bool includeClaims = false)
  {
    await Task.CompletedTask;
    var whoAmI = new WhoAmIResponse(HttpContext.User, includeClaims);
    return whoAmI;
  }

  [HttpGet("authenticate")]
  [ProducesResponseType(typeof(WhoAmIResponse), 200)]
  public async Task<ActionResult?> Challenge(bool requestGoogleSelectAccount = false)
  {
    if (requestGoogleSelectAccount)
      await HttpContext.ChallengeAsync(new GoogleChallengeProperties()
      {
        Prompt = "select_account"
      });
    else if (HttpContext.User.Identity is null || !HttpContext.User.Identity.IsAuthenticated)
      await HttpContext.ChallengeAsync();
    else
      return Ok(await WhoAmI());
    return new EmptyResult();
  }

  [HttpPost("login")]
  public async Task Login(string password, [FromServices] IUserRepo userRepo)
  {
    if (User.Identity is null || !User.Identity.IsAuthenticated)
    {
      await Challenge();
      return;
    }
    var email = User.FindFirstValue(ClaimTypes.Email);
    if (email is null)
      throw new Exception("User principal is authenticated, but has no Email claim!");

    var entity = await userRepo.GetByEmailAsync(email);
    if (entity is null)
    {
      HttpContext.Response.StatusCode = 401;
      return;
    }

    var pvr = entity.VerifyPassword(password);
    if (pvr == PasswordVerificationResult.Failed)
    {
      HttpContext.Response.StatusCode = 401;
      return;
    }

    if (pvr == PasswordVerificationResult.SuccessRehashNeeded)
      _ = userRepo.UpdatePasswordHash(entity).ConfigureAwait(false);

    (HttpContext.User.Identity as ClaimsIdentity)!.AddClaim(new Claim("NlpPass", "1"));
    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, User);
  }

  [HttpPost("logout")]
  public async Task Logout() => await HttpContext.SignOutAsync();

  [HttpPost("set-new-password")]
  [ProducesResponseType(typeof(string), 200)]
  public async Task<ActionResult?> SetNewPassword([FromBody] SetNewPasswordRequest request)
  {
    if (User.Identity is null || !User.Identity.IsAuthenticated)
      return await Challenge();

    var email = User.FindFirstValue(ClaimTypes.Email);
    return Ok(await _authService.SetNewPasswordAsync(request, email));
  }

  [HttpGet("fitbit-start")]
  public async Task<ActionResult> StartFitbitFlow([FromServices] IOAuthRepo oAuthRepo)
  {
    var pkce = new Pkce();
    await oAuthRepo.SetPkceCodesAsync("FitBit", pkce.CodeChallenge, pkce.CodeVerifier);

    var entry = await oAuthRepo.GetOAuthEntryAsync("FitBit");
    if (entry is null) throw new Exception("Missing FitBit OAuth entry");

    var sb = new StringBuilder($"{entry.AuthUrl}?")
      .Append("response_type=code")
      .Append($"&client_id={entry.ClientID}")
      .Append("&scope=activity+cardio_fitness+electrocardiogram+heartrate+location+nutrition+oxygen_saturation+profile+respiratory_rate+settings+sleep+social+temperature+weight")
      .Append($"&code_challenge={entry.PkceCodeChallenge}")
      .Append("&code_challenge_method=S256")
      .Append($"&state={entry.State}")
      .Append($"&redirect_uri={HttpUtility.UrlEncode(entry.RedirectUri)}");

    return Ok(sb.ToString());
  }

  [HttpGet("fitbit")]
  public async Task<ActionResult> ProcessFitbitResponse([FromServices] IOAuthRepo oAuthRepo)
  {
    var queryCollection = Request.Query;

    if (queryCollection.ContainsKey("code"))
    {
      var fitbitCode = queryCollection["code"].ToString();
      await oAuthRepo.SetAuthorizationCodeAsync("FitBit", fitbitCode);
      var entry = await oAuthRepo.GetOAuthEntryAsync("FitBit");
      if (entry is null) throw new Exception("Missing FitBit OAuth entry");

      var request = new HttpRequestMessage(HttpMethod.Post, entry.TokenUrl);
      request.Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
      {
        new("client_id", entry.ClientID),
        new("grant_type", "authorization_code"),
        new("redirect_uri", entry.RedirectUri),
        new("code", entry.AuthorizationCode),
        new("code_verifier", entry.PkceCodeVerifier),
      });

      var userName = entry.ClientID;
      var userPassword = entry.ClientSecret;
      var authenticationString = $"{userName}:{userPassword}";
      var base64String = Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationString));
      request.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64String);

      var httpClient = new HttpClient();
      var response = await httpClient.SendAsync(request);
      response.EnsureSuccessStatusCode();
      var jsonResponse = await response.Content.ReadAsStringAsync();
      var fitbitResponse = JsonConvert.DeserializeObject<GetTokensResponse>(jsonResponse);
      if (fitbitResponse is null) throw new Exception("Unable to parse fitbit response");

      await oAuthRepo.SetAccessTokensAsync("FitBit",
        fitbitResponse.AccessToken,
        fitbitResponse.RefreshToken,
        fitbitResponse.ExpiresIn
      );

      return Ok("SOMETHING WAS DONE!");
    }

    return Ok("NOTHING HAPPENED");
  }

  [HttpGet("fitbit-refresh")]
  public async Task<ActionResult> RefreshFitBitToken([FromServices] IOAuthRepo oAuthRepo)
  {
    var entry = await oAuthRepo.GetOAuthEntryAsync("FitBit");
    if (entry is null) throw new Exception("Missing FitBit OAuth entry");

    var request = new HttpRequestMessage(HttpMethod.Post, entry.TokenUrl);
    request.Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
    {
      new("grant_type", "refresh_token"),
      new("client_id", entry.ClientID),
      new("refresh_token", entry.RefreshToken),
    });

    var userName = entry.ClientID;
    var userPassword = entry.ClientSecret;
    var authenticationString = $"{userName}:{userPassword}";
    var base64String = Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationString));
    request.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64String);

    var httpClient = new HttpClient();
    var response = await httpClient.SendAsync(request);
    response.EnsureSuccessStatusCode();
    var jsonResponse = await response.Content.ReadAsStringAsync();
    var fitbitResponse = JsonConvert.DeserializeObject<GetTokensResponse>(jsonResponse);
    if (fitbitResponse is null) throw new Exception("Unable to parse fitbit response");

    await oAuthRepo.SetAccessTokensAsync("FitBit",
      fitbitResponse.AccessToken,
      fitbitResponse.RefreshToken,
      fitbitResponse.ExpiresIn
    );

    return Ok("ALL DONE");
  }
}

public class GetTokensResponse
{
  [JsonProperty("access_token")]
  public string AccessToken { get; set; } = string.Empty;

  [JsonProperty("expires_in")]
  public int ExpiresIn { get; set; }

  [JsonProperty("refresh_token")]
  public string RefreshToken { get; set; } = string.Empty;

  [JsonProperty("scope")]
  public string Scope { get; set; } = string.Empty;

  [JsonProperty("token_type")]
  public string TokenType { get; set; } = string.Empty;

  [JsonProperty("user_id")]
  public string UserId { get; set; } = string.Empty;
}
