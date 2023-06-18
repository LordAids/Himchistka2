using Himchistka.Services.DTO.Analitic;
using Himchistka.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Himchistka.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalitycController : ControllerBase
    {
        private readonly IAnaliticService _analiticService;
        public AnalitycController(IAnaliticService analiticService)
        {
            _analiticService = analiticService;
        }

        [HttpPost("Chart")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetChartResult(MakeChartAnalitic model)
        {
            return Ok(await _analiticService.GetChartAnalitic(model));
        }

        [HttpPost("Pie")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPieResult(MakeChartAnalitic model)
        {
            return Ok(await _analiticService.GetPieAnalitic(model));
        }

        
    }
}
