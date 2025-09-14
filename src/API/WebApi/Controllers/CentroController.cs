using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DTO.Centro;
using WebApi.Application.Services.Centro;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CentroController : Controller
    {
        private readonly ICentroService _centroService;
        private readonly ILogger<CentroController> _logger;

        public CentroController(ICentroService centroService, ILogger<CentroController> logger)
        {
            _centroService = centroService ?? throw new ArgumentNullException(nameof(centroService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CentroDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateCentro([FromBody] CreateCentroDto createCentroDto)
        {
            _logger.LogInformation("Inicio del método CreateCentro [CRUD]");
            var result = await _centroService.CreateCentroAsync(createCentroDto);
            return CreatedAtAction(nameof(GetCentroById), new { id = result.centroid }, result);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CentroDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetCentroById(long id)
        {
            _logger.LogInformation("Iniciando endpoint GetCentroById con ID: {CentroId} [CRUD AUTOMÁTICO]", id);
            var result = await _centroService.GetCentroByIdAsync(id);
            return Ok(result);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CentroDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateCentro(long id, [FromBody] UpdateCentroDto updateCentroDto)
        {
            _logger.LogInformation("Iniciando endpoint UpdateCentro con ID: {CentroId} [CRUD AUTOMÁTICO]", id);
            // Validar que el ID de la URL coincida con el ID del DTO
            if (id != updateCentroDto.centroid)
            {
                _logger.LogWarning("ID de URL ({CentroId}) no coincide con ID del DTO ({CentroId})", id, updateCentroDto.centroid);
                return BadRequest($"El ID de CentroId ({id}) no coincide con el ID del objeto ({updateCentroDto.centroid})");
            }
            var result = await _centroService.UpdateCentroAsync(updateCentroDto);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteCentro(long id, [FromQuery] string eliminadoPor)
        {
            _logger.LogInformation("Iniciando endpoint DeleteCentro con ID: {CentroId} [CRUD AUTOMÁTICO]", id);
            if (string.IsNullOrWhiteSpace(eliminadoPor))
            {
                _logger.LogWarning("El parámetro eliminadoPor no puede estar vacío o ser nulo.");
                return BadRequest("El parámetro eliminadoPor no puede estar vacío o ser nulo.");
            }
            var result = await _centroService.DeleteCentroAsync(id, eliminadoPor);
            return Ok(result);
        }
    }
}
