using Himchistka.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("{Id}")]
        public IActionResult Get([FromQuery] Guid id) 
        {
            
        }
    }
}
