using Himchistka.Services.DTO;
using Himchistka.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Himchistka.Api.Controllers
{
    /// <summary>
    /// Контролер для работы с услугами
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private IServicesService _servicesService;
        public ServicesController(IServicesService servicesService)
        {
            _servicesService = servicesService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _servicesService.GetAllServices());
        }

        [HttpGet("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get([FromQuery] Guid Id)
        {
            return Ok(await _servicesService.GetServiceById(Id));
        }


        [HttpPost("CreateServices")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateServices([FromBody] DTOServices model)
        {
            if (ModelState.IsValid)
                return Ok(await _servicesService.UpsertService(model));

            return BadRequest();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePlace([FromQuery] Guid Id)
        {
            if (Guid.Empty != Id)
            {
                await _servicesService.DeleteService(Id);
                return Ok();
            }
            return BadRequest();
        }
    }
}
