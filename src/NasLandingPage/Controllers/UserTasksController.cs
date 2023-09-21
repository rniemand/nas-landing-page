using Microsoft.AspNetCore.Mvc;
using NasLandingPage.Extensions;
using NasLandingPage.Models.Dto;
using NasLandingPage.Models.Requests;
using NasLandingPage.Models.Responses;
using NasLandingPage.Services;

namespace NasLandingPage.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserTasksController : ControllerBase
{
  private readonly IUserTasksService _userTasksService;

  public UserTasksController(IUserTasksService userTasksService)
  {
    _userTasksService = userTasksService;
  }

  [HttpGet]
  public async Task<UserTaskDto[]> GetUserTasks() =>
    await _userTasksService.GetUserTasksAsync(User.GetNlpUserContext());

  [HttpPost("categories")]
  public async Task<string[]> GetTaskCategories([FromBody] BasicSearchRequest request) =>
    await _userTasksService.GetTaskCategoriesAsync(User.GetNlpUserContext(), request);

  [HttpPost("sub-categories")]
  public async Task<string[]> GetTaskSubCategories([FromBody] BasicSearchRequest request) =>
    await _userTasksService.GetTaskSubCategoriesAsync(User.GetNlpUserContext(), request);

  [HttpPost]
  public async Task<BoolResponse> AddTask([FromBody] UserTaskDto task) =>
    await _userTasksService.AddUserTaskAsync(User.GetNlpUserContext(), task);

  [HttpPut("complete-task/id/{taskId:int}")]
  public async Task<BoolResponse> CompleteTask([FromRoute] int taskId) =>
    await _userTasksService.CompleteUserTaskAsync(User.GetNlpUserContext(), taskId);
}
