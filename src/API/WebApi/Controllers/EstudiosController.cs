using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DTO.Estudios;
using WebApi.Application.Services.Estudios;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class EstudiosController : Controller
    {
        private readonly ILogger<EstudiosController> _logger;
        private readonly IEstudiosService _estudiosService;
        public EstudiosController(ILogger<EstudiosController> logger, IEstudiosService estudiosService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _estudiosService = estudiosService ?? throw new ArgumentNullException(nameof(estudiosService));
        }
        [HttpPost]
        [ProducesResponseType(typeof(object), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateEstudios([FromBody] CreateEstudiosDto createEstudiosDto)
        {
            _logger.LogInformation("Inicio del método CreateEstudios [CRUD]");
            var result = await _estudiosService.CreateEstudiosAsync(createEstudiosDto);
            return CreatedAtAction(nameof(GetEstudiosById), new { id = result.studiesid }, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetEstudiosById(long id)
        {
            _logger.LogInformation("Iniciando endpoint GetEstudiosById con ID: {EstudiosId} [CRUD AUTOMÁTICO]", id);
            var result = await _estudiosService.GetEstudiosByIdAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateEstudios(long id, [FromBody] UpdateEstudiosDto updateEstudiosDto)
        {
            _logger.LogInformation("Iniciando endpoint UpdateEstudios con ID: {EstudiosId} [CRUD AUTOMÁTICO]", id);
            // Validar que el ID de la URL coincida con el ID del DTO
            if (id != updateEstudiosDto.studiesid)
            {
                _logger.LogWarning("ID de URL ({EstudiosId}) no coincide con ID del DTO ({EstudiosId})", id, updateEstudiosDto.studiesid);
                return BadRequest($"El ID de EstudiosId ({id}) no coincide con el ID del objeto ({updateEstudiosDto.studiesid})");
            }
            var result = await _estudiosService.UpdateEstudiosAsync(updateEstudiosDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteEstudios(long id, [FromQuery] string eliminadoPor)
        {
            _logger.LogInformation("Iniciando endpoint DeleteEstudios con ID: {EstudiosId} [CRUD AUTOMÁTICO]", id);
            var result = await _estudiosService.DeleteEstudiosAsync(id, eliminadoPor);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetAllEstudios()
        {
            _logger.LogInformation("Iniciando endpoint GetAllEstudios [CRUD AUTOMÁTICO]");
            var result = await _estudiosService.GetAllEstudiosAsync();
            return Ok(result);
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetWhere([FromQuery] string condicion)
        {
            _logger.LogInformation("Iniciando endpoint GetWhere con condición: {Condicion} [CRUD AUTOMÁTICO]", condicion);
            var result = await _estudiosService.GetWhereAsync(condicion);
            return Ok(result);
        }
    }
}
