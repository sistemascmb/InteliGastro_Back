using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DTO.Recursos;
using WebApi.Application.Services.Recursos;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class RecursosController : Controller
    {
        private readonly ILogger<RecursosController> _logger;
        private readonly IRecursosService _recursosService;
        public RecursosController(ILogger<RecursosController> logger, IRecursosService recursosService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _recursosService = recursosService ?? throw new ArgumentNullException(nameof(recursosService));
        }

        [HttpPost]
        [ProducesResponseType(typeof(object), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateRecursos([FromBody] CreateRecursosDto createRecursosDto)
        {
            _logger.LogInformation("Inicio del método CreateRecursos [CRUD]");
            var result = await _recursosService.CreateRecursosAsync(createRecursosDto);
            return CreatedAtAction(nameof(GetRecursosById), new { id = result.resourcesid }, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetRecursosById(long id)
        {
            _logger.LogInformation("Iniciando endpoint GetRecursosById con ID: {RecursosId} [CRUD AUTOMÁTICO]", id);
            var result = await _recursosService.GetRecursosByIdAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateRecursos(long id, [FromBody] UpdateRecursosDto updateRecursosDto)
        {
            _logger.LogInformation("Iniciando endpoint UpdateRecursos con ID: {RecursosId} [CRUD AUTOMÁTICO]", id);
            // Validar que el ID de la URL coincida con el ID del DTO
            if (id != updateRecursosDto.resourcesid)
            {
                _logger.LogWarning("ID de URL ({RecursosId}) no coincide con ID del DTO ({RecursosId})", id, updateRecursosDto.resourcesid);
                return BadRequest($"El ID de RecursosId ({id}) no coincide con el ID del objeto ({updateRecursosDto.resourcesid})");
            }
            var result = await _recursosService.UpdateRecursosAsync(updateRecursosDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteRecursos(long id, [FromQuery] string eliminadoPor)
        {
            _logger.LogInformation("Iniciando endpoint DeleteRecursos con ID: {RecursosId} [CRUD AUTOMÁTICO]", id);
            var success = await _recursosService.DeleteRecursosAsync(id, eliminadoPor);
            if (!success)
            {
                _logger.LogWarning("No se encontró el recurso con ID: {RecursosId} para eliminar", id);
                return NotFound();
            }
            return NoContent();
        }   

        [HttpGet("search")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetWhere([FromQuery] string condicion)
        {
            _logger.LogInformation("Iniciando endpoint GetWhere con condición: {Condicion} [CRUD AUTOMÁTICO]", condicion);
            var result = await _recursosService.GetWhereAsync(condicion);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetAllRecursos()
        {
            _logger.LogInformation("Iniciando endpoint GetAllRecursos [CRUD AUTOMÁTICO]");
            var result = await _recursosService.GetAllRecursosAsync();
            return Ok(result);
        }

    }
}
