using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DTO.Estudios;
using WebApi.Application.DTO.Preparacion;
using WebApi.Application.Services.Estudios;
using WebApi.Application.Services.Preparacion;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PreparacionController : Controller
    {
        private readonly ILogger<PreparacionController> _logger;
        private readonly IPreparacionService _preparacionService;

        public PreparacionController(ILogger<PreparacionController> logger, IPreparacionService preparacionService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _preparacionService = preparacionService ?? throw new ArgumentNullException(nameof(preparacionService));
        }

        [HttpPost]
        [ProducesResponseType(typeof(object), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreatePreparacion([FromBody] CreatePreparacionDto createPreparacionDto)
        {
            _logger.LogInformation("Inicio del método CreatePreparacion [CRUD]");
            var result = await _preparacionService.CreatePreparacionAsync(createPreparacionDto);
            return CreatedAtAction(nameof(GetPreparacionById), new { id = result.preparationid }, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetPreparacionById(long id)
        {
            _logger.LogInformation("Iniciando endpoint GetPreparacionById con ID: {preparationid} [CRUD AUTOMÁTICO]", id);
            var result = await _preparacionService.GetPreparacionByIdAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdatePreparacion(long id, [FromBody] UpdatePreparacionDto updatePreparacionDto)
        {
            _logger.LogInformation("Iniciando endpoint UpdatePreparacion con ID: {EstudiosId} [CRUD AUTOMÁTICO]", id);
            // Validar que el ID de la URL coincida con el ID del DTO
            if (id != updatePreparacionDto.preparationid)
            {
                _logger.LogWarning("ID de URL ({preparationid}) no coincide con ID del DTO ({preparationid})", id, updatePreparacionDto.preparationid);
                return BadRequest($"El ID de PreparacionId ({id}) no coincide con el ID del objeto ({updatePreparacionDto.preparationid})");
            }
            var result = await _preparacionService.UpdatePreparacionAsync(updatePreparacionDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeletePreparacion(long id, [FromQuery] string eliminadoPor)
        {
            _logger.LogInformation("Iniciando endpoint DeletePreparacion con ID: {preparationid} [CRUD AUTOMÁTICO]", id);
            var result = await _preparacionService.DeletePreparacionAsync(id, eliminadoPor);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetAllPreparacion()
        {
            _logger.LogInformation("Iniciando endpoint GetAllPreparacion [CRUD AUTOMÁTICO]");
            var result = await _preparacionService.GetAllPreparacionAsync();
            return Ok(result);
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetWhere([FromQuery] string condicion)
        {
            _logger.LogInformation("Iniciando endpoint GetWhere con condición: {Condicion} [CRUD AUTOMÁTICO]", condicion);
            var result = await _preparacionService.GetWhereAsync(condicion);
            return Ok(result);
        }
    }
}
