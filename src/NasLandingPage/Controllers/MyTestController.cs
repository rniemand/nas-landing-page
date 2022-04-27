using Microsoft.AspNetCore.Mvc;

namespace NasLandingPage.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class MyTestController : ControllerBase
  {
    [HttpGet]
    public object Get()
    {
      return new Response
      {
        Message = "Hello World"
      };
    }
  }

  public class Response
  {
    public string Message { get; set; }
  }
}
