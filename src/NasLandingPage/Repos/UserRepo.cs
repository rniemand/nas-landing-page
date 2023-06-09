using Microsoft.AspNetCore.Identity;
using NasLandingPage.Models.Entities;

namespace NasLandingPage.Repos;
public interface IUserRepo
{
  Task<UserEntity?> GetByEmailAsync(string email);
  Task<bool> UpdatePasswordHash(UserEntity user);
}

public class UserRepo : IUserRepo
{
  public async Task<UserEntity?> GetByEmailAsync(string email)
  {
    await Task.CompletedTask;

    var userEntity = new UserEntity
    {
      Email = "niemand.richard@gmail.com",
      Role = "user",
      UserID = 1
    };

    userEntity.PasswordHash = new PasswordHasher<UserEntity>().HashPassword(userEntity, "pass");

    return userEntity;
  }

  public async Task<bool> UpdatePasswordHash(UserEntity user)
  {
    await Task.CompletedTask;
    return true;
  }
}
