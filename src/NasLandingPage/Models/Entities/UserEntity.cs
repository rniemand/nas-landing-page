using Microsoft.AspNetCore.Identity;

namespace NasLandingPage.Models.Entities;

public class UserEntity
{
  public int UserID { get; set; }
  public int CurrentHomeID { get; set; }
  public string Email { get; set; } = null!;
  public string PasswordHash { get; set; } = null!;
  public bool CanSetPass { get; set; }
  public string FirstName { get; set; } = null!;
  public string Surname { get; set; } = null!;

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
