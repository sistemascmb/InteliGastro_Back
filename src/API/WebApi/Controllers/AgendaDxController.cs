using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DTO.ArchivoDigital;
using WebApi.Application.DTO.AgendaDx;
using WebApi.Application.Services.AgendaDx;
using WebApi.Application.Services.AgendaDx;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AgendaDxController : Controller
    {
        private readonly ILogger<AgendaDxController> _logger;
        private readonly IAgendaDxService _AgendaDxService;
        public AgendaDxController(ILogger<AgendaDxController> logger, IAgendaDxService AgendaDxService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _AgendaDxService = AgendaDxService ?? throw new ArgumentNullException(nameof(AgendaDxService));
        }
        [HttpPost]
        [ProducesResponseType(typeof(object), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateAgendaDx([FromBody] CreateAgendaDx createAgendaDxDto)
        {
            _logger.LogInformation("Inicio del método CreateAgendaDx [CRUD]");
            var result = await _AgendaDxService.CreateAgendaDxAsync(createAgendaDxDto);
            return CreatedAtAction(nameof(GetAgendaDxById), new { id = result.medicalscheduledxid }, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetAgendaDxById(long id)
        {
            _logger.LogInformation("Iniciando endpoint GetAgendaDxById con ID: {AgendaDxId} [CRUD AUTOMÁTICO]", id);
            var result = await _AgendaDxService.GetAgendaDxByIdAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateAgendaDx(long id, [FromBody] UpdateAgendaDx updateAgendaDxDto)
        {
            _logger.LogInformation("Iniciando endpoint UpdateAgendaDx con ID: {AgendaDxId} [CRUD AUTOMÁTICO]", id);
            // Validar que el ID de la URL coincida con el ID del DTO
            if (id != updateAgendaDxDto.medicalscheduledxid)
            {
                _logger.LogWarning("ID de URL ({AgendaDxId}) no coincide con ID del DTO ({AgendaDxId})", id, updateAgendaDxDto.medicalscheduledxid);
                return BadRequest($"El ID de AgendaDxId ({id}) no coincide con el ID del objeto ({updateAgendaDxDto.medicalscheduledxid})");
            }
            var result = await _AgendaDxService.UpdateAgendaDxAsync(updateAgendaDxDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteAgendaDx(long id, [FromQuery] string eliminadoPor)
        {
            _logger.LogInformation("Iniciando endpoint DeleteAgendaDx con ID: {AgendaDxId} [CRUD AUTOMÁTICO]", id);
            var result = await _AgendaDxService.DeleteAgendaDxAsync(id, eliminadoPor);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetAllAgendaDx()
        {
            _logger.LogInformation("Iniciando endpoint GetAllAgendaDx [CRUD AUTOMÁTICO]");
            var result = await _AgendaDxService.GetAllAgendaDxAsync();
            return Ok(result);
        }

        [HttpGet("where")]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetWhere([FromQuery] string condicion)
        {
            _logger.LogInformation("Iniciando endpoint GetWhere con condición: {Condicion} [CRUD AUTOMÁTICO]", condicion);
            var result = await _AgendaDxService.GetWhereAsync(condicion);
            return Ok(result);
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<AgendaDxDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<AgendaDxDto>>> SearchAgendaDx(
            [FromQuery] string? value1)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(value1))
                {
                    return BadRequest("Debe proporcionar al menos un criterio de búsqueda (value1)");
                }

                var AgendaDx = await _AgendaDxService.SearchAgendaDxAsync(value1);
                return Ok(AgendaDx);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar AgendaDx con los filtros especificados");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor");
            }
        }
    }
}
