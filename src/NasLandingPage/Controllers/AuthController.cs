using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NasLandingPage.Exceptions;
using NasLandingPage.Extensions;
using NasLandingPage.Models;
using NasLandingPage.Models.Requests;
using NasLandingPage.Models.Responses;
using NasLandingPage.Repos;
using NasLandingPage.Services;

namespace NasLandingPage.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
  private readonly IAuthService _authService;
  private readonly IUserRepo _userRepo;

  public AuthController(IAuthService authService, IUserRepo userRepo)
  {
    _authService = authService;
    _userRepo = userRepo;
  }

  [HttpGet("whoami")]
  public async Task<WhoAmIResponse> WhoAmI(bool includeClaims = false)
  {
    var whoAmIResponse = new WhoAmIResponse(HttpContext.User, includeClaims);
    if (string.IsNullOrWhiteSpace(whoAmIResponse.Email)) return whoAmIResponse;
    var user = await EnsureUserIdAndHomeIdAsync(User.GetNlpUserContext());
    whoAmIResponse.UserId = user.UserId;
    whoAmIResponse.HomeId = user.HomeId;
    return whoAmIResponse;
  }

  [HttpGet("authenticate")]
  [ProducesResponseType(typeof(WhoAmIResponse), 200)]
  public async Task<ActionResult?> Challenge(bool requestGoogleSelectAccount = false)
  {
    if (requestGoogleSelectAccount)
      await HttpContext.ChallengeAsync(new GoogleChallengeProperties
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
  public async Task<WhoAmIResponse?> Login(string password, [FromServices] IUserRepo userRepo)
  {
    if (User.Identity is null || !User.Identity.IsAuthenticated)
    {
      var response = await Challenge();
      if (response is EmptyResult) return null;
      return (response as OkObjectResult)?.Value as WhoAmIResponse;
    }

    var email = User.FindFirstValue(ClaimTypes.Email);
    if (email is null)
      throw new NlpException("User principal is authenticated, but has no Email claim!");

    var entity = await userRepo.GetByEmailAsync(email);
    if (entity is null)
    {
      HttpContext.Response.StatusCode = 401;
      return null;
    }

    var pvr = entity.VerifyPassword(password);
    if (pvr == PasswordVerificationResult.Failed)
    {
      HttpContext.Response.StatusCode = 401;
      return null;
    }

    if (pvr == PasswordVerificationResult.SuccessRehashNeeded)
      _ = userRepo.UpdatePasswordHash(entity).ConfigureAwait(false);

    (HttpContext.User.Identity as ClaimsIdentity)!.AddClaim(new Claim("NlpPass", "1"));
    (HttpContext.User.Identity as ClaimsIdentity)!.AddClaim(new Claim("NlpUser", $"{entity.UserID}:{entity.Email}"));
    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, User);
    return await WhoAmI();
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

    return email is null
      ? throw new NlpException("Failed to find users email")
      : Ok(await _authService.SetNewPasswordAsync(request, email));
  }

  private async Task<NlpUserContext> EnsureUserIdAndHomeIdAsync(NlpUserContext user)
  {
    if (user is { UserId: > 0, HomeId: > 0 }) return user;

    if (string.IsNullOrWhiteSpace(user.Email))
      throw new NlpException("Unable to resolve userId - no email provided");

    var dbUser = await _userRepo.GetByEmailAsync(user.Email);
    if (dbUser is null)
      throw new NlpException($"Unable to resolve an RPP user with an email of: {user.Email}");

    user.UserId = dbUser.UserID;
    user.HomeId = dbUser.CurrentHomeID;
    return user;
  }
}
