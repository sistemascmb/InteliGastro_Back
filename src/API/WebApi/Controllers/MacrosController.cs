using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DTO.Macros;
using WebApi.Application.Services.Macros;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class MacrosController : Controller
    {
        private readonly ILogger<MacrosController> _logger;
        private readonly IMacrosService _MacrosService;
        public MacrosController(ILogger<MacrosController> logger, IMacrosService MacrosService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _MacrosService = MacrosService ?? throw new ArgumentNullException(nameof(MacrosService));
        }
        [HttpPost]
        [ProducesResponseType(typeof(object), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateMacros([FromBody] CreateMacrosDto createMacrosDto)
        {
            _logger.LogInformation("Inicio del método CreateMacros [CRUD]");
            var result = await _MacrosService.CreateMacrosAsync(createMacrosDto);
            return CreatedAtAction(nameof(GetMacrosById), new { id = result.macrosid }, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetMacrosById(long id)
        {
            _logger.LogInformation("Iniciando endpoint GetMacrosById con ID: {MacrosId} [CRUD AUTOMÁTICO]", id);
            var result = await _MacrosService.GetMacrosByIdAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateMacros(long id, [FromBody] UpdateMacrosDto updateMacrosDto)
        {
            _logger.LogInformation("Iniciando endpoint UpdateMacros con ID: {MacrosId} [CRUD AUTOMÁTICO]", id);
            // Validar que el ID de la URL coincida con el ID del DTO
            if (id != updateMacrosDto.macrosid)
            {
                _logger.LogWarning("ID de URL ({MacrosId}) no coincide con ID del DTO ({MacrosId})", id, updateMacrosDto.macrosid);
                return BadRequest($"El ID de MacrosId ({id}) no coincide con el ID del objeto ({updateMacrosDto.macrosid})");
            }
            var result = await _MacrosService.UpdateMacrosAsync(updateMacrosDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteMacros(long id, [FromQuery] string eliminadoPor)
        {
            _logger.LogInformation("Iniciando endpoint DeleteMacros con ID: {MacrosId} [CRUD AUTOMÁTICO]", id);
            var result = await _MacrosService.DeleteMacrosAsync(id, eliminadoPor);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetAllMacros()
        {
            _logger.LogInformation("Iniciando endpoint GetAllMacros [CRUD AUTOMÁTICO]");
            var result = await _MacrosService.GetAllMacrosAsync();
            return Ok(result);
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetWhere([FromQuery] string condicion)
        {
            _logger.LogInformation("Iniciando endpoint GetWhere con condición: {Condicion} [CRUD AUTOMÁTICO]", condicion);
            var result = await _MacrosService.GetWhereAsync(condicion);
            return Ok(result);
        }
    }
}
