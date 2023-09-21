using NasLandingPage.Models;
using NasLandingPage.Models.Dto;
using NasLandingPage.Models.Responses;
using NasLandingPage.Repos;

namespace NasLandingPage.Services;

public interface IUserTasksService
{
  Task<BoolResponse> AddUserTaskAsync(NlpUserContext userContext, UserTaskDto taskDto);
  Task<UserTaskDto[]> GetUserTasksAsync(NlpUserContext userContext);
  Task<string[]> GetTaskCategoriesAsync(NlpUserContext userContext);
  Task<string[]> GetTaskSubCategoriesAsync(NlpUserContext userContext, string category);
}

internal class UserTasksService : IUserTasksService
{
  private readonly IUserTasksRepo _userTasksRepo;

  public UserTasksService(IUserTasksRepo userTasksRepo)
  {
    _userTasksRepo = userTasksRepo;
  }

  public async Task<BoolResponse> AddUserTaskAsync(NlpUserContext userContext, UserTaskDto taskDto)
  {
    var response = new BoolResponse();
    var taskEntity = taskDto.ToEntity();
    taskEntity.UserID = userContext.UserId;
    var rowCount = await _userTasksRepo.AddTaskAsync(taskEntity);
    return rowCount == 0 ? response.AsError("Failed to add user task") : response;
  }

  public async Task<UserTaskDto[]> GetUserTasksAsync(NlpUserContext userContext) =>
    (await _userTasksRepo.GetUserTasksAsync(userContext)).Select(UserTaskDto.FromEntity).ToArray();

  public async Task<string[]> GetTaskCategoriesAsync(NlpUserContext userContext) =>
    (await _userTasksRepo.GetTaskCategoriesAsync(userContext)).ToArray();

  public async Task<string[]> GetTaskSubCategoriesAsync(NlpUserContext userContext, string category) =>
    (await _userTasksRepo.GetTaskSubCategoriesAsync(userContext, category)).ToArray();
}
