using NasLandingPage.Models.Requests;
using NasLandingPage.Repos;
using RnCore.Abstractions.Extensions;

namespace NasLandingPage.Services;

public interface IAuthService
{
  Task<string> SetNewPasswordAsync(SetNewPasswordRequest request, string email);
}

public class AuthService : IAuthService
{
  private readonly IUserRepo _userRepo;

  public AuthService(IUserRepo userRepo)
  {
    _userRepo = userRepo;
  }

  public async Task<string> SetNewPasswordAsync(SetNewPasswordRequest request, string email)
  {
    // Ensure that this is a valid reset request
    var dbUser = await _userRepo.GetByEmailAsync(email);
    if (dbUser is null) return "ERROR: Invalid user!";
    if (!dbUser.CanSetPass) return "ERROR: Not allowed to reset password!";
    if (!dbUser.Email.IgnoreCaseEquals(request.Email)) return "ERROR: Not your account";

    dbUser.SetPassword(request.Password);
    if (await _userRepo.UpdatePasswordHash(dbUser))
      return "SUCCESS: Password has been reset";

    return "ERROR: Failed to update password";
  }
}
