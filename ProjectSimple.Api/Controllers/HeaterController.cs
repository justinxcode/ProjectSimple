using Microsoft.AspNetCore.Mvc;

namespace ProjectSimple.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HeaterController : ControllerBase
{
    // GET: api/<HeaterController>
    [HttpGet]
    public IActionResult Get()
    {
        var random = new Random();

        var heaters = new List<string>
        {
            "Electric",
            "Wood",
            "Gas"
        };

        return Ok(heaters[random.Next(heaters.Count)]);
    }
}
