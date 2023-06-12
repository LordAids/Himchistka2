using Himchistka.Services.DTO;
using Himchistka.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Himchistka.Api.Controllers
{
    /// <summary>
    /// Контролер для работы с затратами
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SpendingController : ControllerBase
    {
        private ISpendingService _spendingService;

        public SpendingController(ISpendingService spendingService)
        {
            _spendingService = spendingService;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _spendingService.GetAllPlaces());
        }

        [HttpGet("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get([FromQuery] Guid Id)
        {
            return Ok(await _spendingService.GetSpendingById(Id));
        }


        [HttpPost("CreateSpending")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateSpending([FromBody] DTOSpending placeModel)
        {
            if (ModelState.IsValid)
                return Ok(await _spendingService.UpsertSpending(placeModel));

            return BadRequest();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePlace([FromQuery] Guid Id)
        {
            if (Guid.Empty != Id)
            {
                await _spendingService.DeleteSpending(Id);
                return Ok();
            }
            return BadRequest();
        }
    }
}
