using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NasLandingPage.Exceptions;
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

  public AuthController(IAuthService authService)
  {
    _authService = authService;
  }

  [HttpGet("whoami")]
  public WhoAmIResponse WhoAmI(bool includeClaims = false) =>
    new(HttpContext.User, includeClaims);

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
      return Ok(WhoAmI());
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
      throw new NlpException("User principal is authenticated, but has no Email claim!");

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

    return email is null
      ? throw new NlpException("Failed to find users email")
      : Ok(await _authService.SetNewPasswordAsync(request, email));
  }
}
