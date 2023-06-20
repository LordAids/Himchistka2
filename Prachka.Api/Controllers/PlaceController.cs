using Himchistka.Services.DTO;
using Himchistka.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Himchistka.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с точками
    /// </summary>
    [Route("api/Places")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        private IPlaceService _placeService;
        public PlaceController(IPlaceService placeService)
        {
            _placeService = placeService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _placeService.GetAllPlaces());
        }

        [HttpGet("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get([FromQuery] Guid Id) 
        {
            return Ok(await _placeService.GetPlaceById(Id));
        }

        
        [HttpPost("CreatePlace")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreatePlace([FromBody]DTOPlace placeModel)
        {
            if(ModelState.IsValid)
                return Ok(await _placeService.UpsertPlace(placeModel));

            return BadRequest();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePlace([FromQuery] Guid Id)
        {
            if (Guid.Empty != Id)
            {
                await _placeService.DeletePlace(Id);
                return Ok();
            }
            return BadRequest();
        }

    }
}
