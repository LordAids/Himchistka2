using Himchistka.Services.DTO;
using Himchistka.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Himchistka.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private IClientService _clientService;
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [Authorize]
        [HttpGet("{orderId}")]
        public IActionResult GetClientById([FromRoute] Guid orderId)
        {
            return Ok(_clientService.GetClientById(orderId));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetClients()
        {
            return Ok(_clientService.GetAllClients());
        }

        [Authorize]
        [HttpPost("CreateClient")]
        public async Task<IActionResult> UpsertClients([FromBody] DTOClient model)
        {
            if (ModelState.IsValid)
                return Ok(await _clientService.UpsertClient(model));

            return BadRequest();
        }

        [Authorize]
        [HttpDelete("{ClientId}")]
        public IActionResult DeleteClients([FromRoute] Guid ClientId)
        {
            _clientService.DeleteClient(ClientId);
            return Ok();
        }
    }
}
