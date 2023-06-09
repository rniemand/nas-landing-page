using Microsoft.AspNetCore.Identity;

namespace NasLandingPage.Models.Entities;

public class UserEntity
{
  public long UserID { get; set; }
  public string Email { get; set; } = string.Empty;
  public string PasswordHash { get; set; } = string.Empty;

  public void SetPassword(string password) =>
    PasswordHash = new PasswordHasher<UserEntity>().HashPassword(this, password);

  public PasswordVerificationResult VerifyPassword(string password)
  {
    var pvr = new PasswordHasher<UserEntity>()
      .VerifyHashedPassword(this, PasswordHash, password);

    if (pvr == PasswordVerificationResult.SuccessRehashNeeded)
      SetPassword(password);

    return pvr;
  }
}
