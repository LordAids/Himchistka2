﻿using Himchistka.Services.DTO;
using Himchistka.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet("{orderId}")]
        public IActionResult GetOrderById([FromRoute] Guid orderId)
        {
            return Ok(_orderServices.GetOrderById(orderId));
        }

        [HttpPost]
        [Authorize]
        public IActionResult GetOrders([FromQuery]Guid placeId)
        {
            return Ok(_orderServices.GetAllOrders(placeId));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetOrders()
        {
            return Ok(_orderServices.GetAllOrders());
        }

        [Authorize]
        [HttpPost("CreateOrder")]
        public async Task<IActionResult> UpsertOrder([FromBody] DTOOrders model)
        {
            if(ModelState.IsValid)
                return Ok(await _orderServices.UpsertOrder(model));

            return BadRequest();
        }

        [Authorize]
        [HttpDelete("{orderId}")]
        public IActionResult DeleteOrder([FromRoute] Guid orderId) 
        {
            _orderServices.DeleteOrder(orderId);
            return Ok();
        }

        [Authorize]
        [HttpPost("ChangeStatus")]
        public IActionResult ChangeOrderStatus([FromQuery] Guid orderId, [FromQuery] int statusId)
        {
            _orderServices.ChangeOrderStatus(orderId, statusId);
            return Ok();
        }

        [Authorize]
        [HttpGet("ClientOrder")]
        public IActionResult GetClientOrder([FromQuery] Guid clientId)
        {
            return Ok(_orderServices.GetClientOrder(clientId));
        }
    }
}
