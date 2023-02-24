using Microsoft.AspNetCore.Mvc;

namespace TimeR.Core.Controllers;

[ApiController]
[Route("[controller]")]
public class TimersController : ControllerBase
{
    [HttpGet("/")]
    public string Get()
    {
        return "Hello Timers!";
    }
}
