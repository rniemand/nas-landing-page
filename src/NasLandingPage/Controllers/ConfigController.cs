using Microsoft.AspNetCore.Mvc;
using NasLandingPage.Common.Models.Responses;
using NasLandingPage.Common.Services;

namespace NasLandingPage.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConfigController : ControllerBase
{
  private readonly IConfigService _configService;

  public ConfigController(IConfigService configService)
  {
    _configService = configService;
  }

  [HttpGet]
  public ClientConfig GetClientConfig()
  {
    return _configService.GetClientConfig();
  }
}
