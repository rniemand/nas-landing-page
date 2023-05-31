using Microsoft.AspNetCore.Mvc;
using NasLandingPage.Models.Dto;
using NasLandingPage.Services;

namespace NasLandingPage.Controllers;

[ApiController]
[Route("[controller]")]
public class UserTasksController : ControllerBase
{
  private readonly IUserTaskService _userTaskService;

  public UserTasksController(IUserTaskService userTaskService)
  {
    _userTaskService = userTaskService;
  }

  [HttpGet("all")]
  public async Task<List<UserTaskDto>> GetAllTasks() => await _userTaskService.GetAllAsync();

  [HttpPost("add")]
  public async Task<bool> AddTask([FromBody] UserTaskDto task) => await _userTaskService.AddAsync(task);
}
