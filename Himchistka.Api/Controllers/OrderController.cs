using Himchistka.Services.DTO;
using Himchistka.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Himchistka.Api.Controllers
{
    [Route("api/Orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderServices _orderServices;
        public OrderController(IOrderServices orderServices) 
        {
            _orderServices= orderServices;
        }

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
        public IActionResult UpsertOrder([FromBody] DTOUpsertOrder model)
        {
            if(ModelState.IsValid)
                return Ok(_orderServices.UpsertOrder(model));

            return BadRequest();
        }

        [HttpDelete("{orderId}")]
        public IActionResult DeleteOrder([FromRoute] Guid orderId) 
        {
            return Ok();
        }
    }
}
