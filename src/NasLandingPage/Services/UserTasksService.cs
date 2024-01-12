using NasLandingPage.Models;
using NasLandingPage.Models.Dto;
using NasLandingPage.Models.Requests;
using NasLandingPage.Models.Responses;
using NasLandingPage.Repos;

namespace NasLandingPage.Services;

public interface IUserTasksService
{
  Task<BoolResponse> AddUserTaskAsync(NlpUserContext userContext, UserTaskDto taskDto);
  Task<IEnumerable<UserTaskDto>> GetUserTasksAsync(NlpUserContext userContext, BasicSearchRequest request);
  Task<IEnumerable<string>> GetTaskCategoriesAsync(NlpUserContext userContext, BasicSearchRequest request);
  Task<IEnumerable<string>> GetAllTaskCategoriesAsync(NlpUserContext userContext, BasicSearchRequest request);
  Task<IEnumerable<string>> GetTaskSubCategoriesAsync(NlpUserContext userContext, BasicSearchRequest request);
  Task<BoolResponse> CompleteUserTaskAsync(NlpUserContext userContext, int taskId);
  Task<BoolResponse> UpdateUserTaskAsync(NlpUserContext userContext, UserTaskDto taskDto);
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

  public async Task<IEnumerable<UserTaskDto>> GetUserTasksAsync(NlpUserContext userContext, BasicSearchRequest request) =>
    (await _userTasksRepo.GetUserTasksAsync(userContext, request)).Select(UserTaskDto.FromEntity);

  public async Task<IEnumerable<string>> GetTaskCategoriesAsync(NlpUserContext userContext, BasicSearchRequest request) =>
    await _userTasksRepo.GetTaskCategoriesAsync(userContext, request.Filter ?? "", request.IncludeCompletedEntries);

  public async Task<IEnumerable<string>> GetAllTaskCategoriesAsync(NlpUserContext userContext, BasicSearchRequest request) =>
    await _userTasksRepo.GetAllTaskCategoriesAsync(userContext, request.IncludeCompletedEntries);

  public async Task<IEnumerable<string>> GetTaskSubCategoriesAsync(NlpUserContext userContext, BasicSearchRequest request) =>
    await _userTasksRepo.GetTaskSubCategoriesAsync(userContext, request.Filter ?? "", request.SubFilter ?? "", request.IncludeCompletedEntries);

  public async Task<BoolResponse> CompleteUserTaskAsync(NlpUserContext userContext, int taskId)
  {
    var response = new BoolResponse();
    var rowCount = await _userTasksRepo.CompleteUserTaskAsync(userContext, taskId);
    return rowCount == 0 ? response.AsError("Failed to complete task") : response;
  }

  public async Task<BoolResponse> UpdateUserTaskAsync(NlpUserContext userContext, UserTaskDto taskDto)
  {
    var response = new BoolResponse();
    var taskEntity = taskDto.ToEntity();
    taskEntity.UserID = userContext.UserId;
    var rowCount = await _userTasksRepo.UpdateUserTaskAsync(taskEntity);
    return rowCount == 0 ? response.AsError("Failed to update task") : response;
  }
}
