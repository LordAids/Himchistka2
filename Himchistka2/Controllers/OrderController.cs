using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Himchistka.Api.Controllers
{
    [Route("api/Orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpGet("{orderId}")]
        public IActionResult GetOrderById([FromRoute] Guid orderId)
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult UpsertOrder()
        {
            return Ok();
        }

        [HttpDelete("{orderId}")]
        public IActionResult DeleteOrder([FromRoute] Guid orderId) 
        {
            return Ok();
        }
    }
}
