using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain;

namespace OrderProcessor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController(ILogger<OrdersController> logger) : ControllerBase
    {
        [HttpPost("CreateOrder")]
        public IActionResult CreateOrder([FromBody] OrderRequest request)
        {
            
            // Basic logic
            var response = new OrderResponse($"Order '{request.OrderId}' created.");

            logger.LogInformation($"Sending response to Payment service: {response}");

            return Ok(response);
        }
    }
}
