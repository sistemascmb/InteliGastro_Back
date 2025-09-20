using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DTO.Agenda;
using WebApi.Application.Services.Agenda;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AgendaController : Controller
    {
        private readonly ILogger<AgendaController> _logger;
        private readonly IAgendaService _AgendaService;
        public AgendaController(ILogger<AgendaController> logger, IAgendaService AgendaService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _AgendaService = AgendaService ?? throw new ArgumentNullException(nameof(AgendaService));
        }
        [HttpPost]
        [ProducesResponseType(typeof(object), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateAgenda([FromBody] CreateAgendaDto createAgendaDto)
        {
            _logger.LogInformation("Inicio del método CreateAgenda [CRUD]");
            var result = await _AgendaService.CreateAgendaAsync(createAgendaDto);
            return CreatedAtAction(nameof(GetAgendaById), new { id = result.medicalscheduleid }, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetAgendaById(long id)
        {
            _logger.LogInformation("Iniciando endpoint GetAgendaById con ID: {AgendaId} [CRUD AUTOMÁTICO]", id);
            var result = await _AgendaService.GetAgendaByIdAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateAgenda(long id, [FromBody] UpdateAgendaDto updateAgendaDto)
        {
            _logger.LogInformation("Iniciando endpoint UpdateAgenda con ID: {AgendaId} [CRUD AUTOMÁTICO]", id);
            // Validar que el ID de la URL coincida con el ID del DTO
            if (id != updateAgendaDto.medicalscheduleid)
            {
                _logger.LogWarning("ID de URL ({AgendaId}) no coincide con ID del DTO ({AgendaId})", id, updateAgendaDto.medicalscheduleid);
                return BadRequest($"El ID de AgendaId ({id}) no coincide con el ID del objeto ({updateAgendaDto.medicalscheduleid})");
            }
            var result = await _AgendaService.UpdateAgendaAsync(updateAgendaDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteAgenda(long id, [FromQuery] string eliminadoPor)
        {
            _logger.LogInformation("Iniciando endpoint DeleteAgenda con ID: {AgendaId} [CRUD AUTOMÁTICO]", id);
            var result = await _AgendaService.DeleteAgendaAsync(id, eliminadoPor);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetAllAgenda()
        {
            _logger.LogInformation("Iniciando endpoint GetAllAgenda [CRUD AUTOMÁTICO]");
            var result = await _AgendaService.GetAllAgendaAsync();
            return Ok(result);
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetWhere([FromQuery] string condicion)
        {
            _logger.LogInformation("Iniciando endpoint GetWhere con condición: {Condicion} [CRUD AUTOMÁTICO]", condicion);
            var result = await _AgendaService.GetWhereAsync(condicion);
            return Ok(result);
        }
    }
}
