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
        [Authorize("Admin")]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpGet("{Id}")]
        [Authorize("Admin")]
        public IActionResult Get([FromQuery] Guid id) 
        {
            return Ok();
        }

        
        [HttpPost("CreatePlace")]
        [Authorize("Admin")]
        public async Task<IActionResult> CreatePlace(DTOPlace placeModel)
        {
            if(ModelState.IsValid)
                await _placeService.UpsertPlace(placeModel);

            return Ok();
        }

    }
}
