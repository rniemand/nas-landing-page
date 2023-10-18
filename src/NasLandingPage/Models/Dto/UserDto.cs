using NasLandingPage.Models.Entities;

namespace NasLandingPage.Models.Dto;

public class UserDto
{
  public int UserID { get; set; }
  public int CurrentHomeID { get; set; }
  public string Email { get; set; } = null!;
  public string FirstName { get; set; } = null!;
  public string Surname { get; set; } = null!;
  public string DisplayName => $"{FirstName} {Surname}";

  public static UserDto FromEntity(UserEntity entity) => new()
  {
    Email = entity.Email,
    FirstName = entity.FirstName,
    Surname = entity.Surname,
    UserID = entity.UserID,
    CurrentHomeID = entity.CurrentHomeID,
  };
}
