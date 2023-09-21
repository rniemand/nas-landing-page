using Microsoft.AspNetCore.Mvc;
using NasLandingPage.Extensions;
using NasLandingPage.Models.Dto;
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
  public async Task<string[]> GetTaskCategories([FromBody] string filter) =>
    await _userTasksService.GetTaskCategoriesAsync(User.GetNlpUserContext(), filter);

  [HttpPost("sub-categories")]
  public async Task<string[]> GetTaskSubCategories([FromBody] string category) =>
    await _userTasksService.GetTaskSubCategoriesAsync(User.GetNlpUserContext(), category);

  [HttpPost]
  public async Task<BoolResponse> AddTask([FromBody] UserTaskDto task) =>
    await _userTasksService.AddUserTaskAsync(User.GetNlpUserContext(), task);
}
