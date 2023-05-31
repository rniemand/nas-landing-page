using NasLandingPage.Models.Dto;
using NasLandingPage.Repos;

namespace NasLandingPage.Services;

public interface IUserTaskService
{
  Task<List<UserTaskDto>> GetAllAsync();
  Task<bool> AddAsync(UserTaskDto task);
}

public class UserTaskService : IUserTaskService
{
  private readonly IUserTasksRepo _userTasksRepo;

  public UserTaskService(IUserTasksRepo userTasksRepo)
  {
    _userTasksRepo = userTasksRepo;
  }

  public async Task<List<UserTaskDto>> GetAllAsync()
  {
    var dbTasks = await _userTasksRepo.GetAllAsync();
    return dbTasks.Select(UserTaskDto.FromEntity).ToList();
  }

  public async Task<bool> AddAsync(UserTaskDto task) => (await _userTasksRepo.AddAsync(task.AsEntity())) == 1;
}
