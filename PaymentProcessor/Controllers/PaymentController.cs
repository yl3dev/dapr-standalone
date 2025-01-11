using Dapr.Client;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PaymentProcessor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(DaprClient daprClient, ILogger<PaymentController> logger) : ControllerBase
    {
        [HttpPost("Pay")]
        public async Task<IActionResult> Pay([FromBody] PaymentRequest request)
        {
            // Example: we want to create an order with the Orders service
            var orderRequest = new OrderRequest(request.OrderId);

            OrderResponse orderResponse;
            try
            {
                // Dapr: call "orders" service, method = "Orders/CreateOrder"
                // Note: "orders" here is the same as the --app-id set in the Orders sidecar
                orderResponse = await daprClient.InvokeMethodAsync<OrderRequest, OrderResponse>(
                    "orders",       // The target app-id
                    "Orders/CreateOrder", // The relative path (controller + action in ASP.NET)
                    orderRequest);
            } catch(Exception ex)
            {
                logger.LogError(ex, "Error calling Orders service");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error calling Orders service");
            }


            // Return combined info
            return Ok(new
            {
                Message = $"Payment processed for OrderId = {request.OrderId}",
                OrderResult = orderResponse.Message
            });
        }
    }
}
