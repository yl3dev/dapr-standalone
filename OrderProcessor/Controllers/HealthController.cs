using Microsoft.AspNetCore.Mvc;

namespace OrderProcessor.Controllers
{
    [Route("/")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Payments service is healthy");
        }
    }
}
