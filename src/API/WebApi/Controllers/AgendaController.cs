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

        [HttpGet("search/date-range")]
        [ProducesResponseType(typeof(IEnumerable<AgendaDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> SearchByDateRange([FromQuery(Name = "startDate")] string startDateStr, [FromQuery(Name = "endDate")] string endDateStr)
        {
            try
            {
                _logger.LogInformation("Iniciando búsqueda de agenda por rango de fechas. StartDate: {StartDate}, EndDate: {EndDate}", startDateStr, endDateStr);

                if (string.IsNullOrEmpty(startDateStr) || string.IsNullOrEmpty(endDateStr))
                {
                    return BadRequest("Las fechas inicial y final son requeridas");
                }

                if (!DateTime.TryParse(startDateStr, out DateTime startDate) || !DateTime.TryParse(endDateStr, out DateTime endDate))
                {
                    return BadRequest("El formato de las fechas no es válido. Use el formato yyyy-MM-dd");
                }

                _logger.LogInformation("Fechas parseadas - StartDate: {StartDate}, EndDate: {EndDate}", startDate, endDate);

                var result = await _AgendaService.SearchAgendaByDateRangeAsync(startDate, endDate);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Error de validación en búsqueda por rango de fechas");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar agenda por rango de fechas: {Message}", ex.Message);
                return StatusCode(500, "Error interno del servidor al procesar la solicitud");
            }
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

        [HttpGet("search/studies")]
        [ProducesResponseType(typeof(IEnumerable<AgendaSearchResultDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> SearchStudies(
            [FromQuery] long? medicalscheduleid,
            [FromQuery] string? names,
            [FromQuery(Name = "lastNames")] string? lastNames,
            [FromQuery(Name = "dni")] string? dni)
        {
            try
            {
                _logger.LogInformation("Iniciando endpoint SearchStudies con filtros: medicalscheduleid={MedicalScheduleId}, names={Names}, lastNames={LastNames}, dni={Dni}", medicalscheduleid, names, lastNames, dni);
                var result = await _AgendaService.SearchAgendaStudiesAsync(medicalscheduleid, names, lastNames, dni);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Error de validación en búsqueda de estudios");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar estudios");
                return StatusCode(500, "Error interno del servidor al procesar la solicitud");
            }
        }
    }
}
