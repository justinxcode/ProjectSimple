using Microsoft.AspNetCore.Mvc;

namespace ProjectSimple.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HeaterController : ControllerBase
{
    // GET: api/<HeaterController>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult Get()
    {
        var random = new Random();

        var heaters = new List<string>
        {
            "Electric",
            "Wood",
            "Gas"
        };

        var result = heaters[random.Next(heaters.Count)];

        return Ok(result);
    }
}