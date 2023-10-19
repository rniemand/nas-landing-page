using NasLandingPage.Models.Dto;
using NasLandingPage.Repos;

namespace NasLandingPage.Services;

public interface IUserService
{
  Task<IEnumerable<UserDto>> GetAllUsersAsync();
}

class UserService : IUserService
{
  private readonly IUserRepo _userRepo;

  public UserService(IUserRepo userRepo)
  {
    _userRepo = userRepo;
  }

  public async Task<IEnumerable<UserDto>> GetAllUsersAsync() =>
    (await _userRepo.GetAllUsersAsync()).Select(UserDto.FromEntity);
}
