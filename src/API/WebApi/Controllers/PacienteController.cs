using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DTO.Paciente;
using WebApi.Application.Services.Paciente;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PacienteController : Controller
    {
        private readonly ILogger<PacienteController> _logger;
        private readonly IPacienteService _PacienteService;
        public PacienteController(ILogger<PacienteController> logger, IPacienteService PacienteService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _PacienteService = PacienteService ?? throw new ArgumentNullException(nameof(PacienteService));
        }
        [HttpPost]
        [ProducesResponseType(typeof(object), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreatePaciente([FromBody] CreatePacienteDto createPacienteDto)
        {
            _logger.LogInformation("Inicio del método CreatePaciente [CRUD]");
            var result = await _PacienteService.CreatePacienteAsync(createPacienteDto);
            return CreatedAtAction(nameof(GetPacienteById), new { id = result.pacientid }, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetPacienteById(long id)
        {
            _logger.LogInformation("Iniciando endpoint GetPacienteById con ID: {PacienteId} [CRUD AUTOMÁTICO]", id);
            var result = await _PacienteService.GetPacienteByIdAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdatePaciente(long id, [FromBody] UpdatePacienteDto updatePacienteDto)
        {
            _logger.LogInformation("Iniciando endpoint UpdatePaciente con ID: {PacienteId} [CRUD AUTOMÁTICO]", id);
            // Validar que el ID de la URL coincida con el ID del DTO
            if (id != updatePacienteDto.pacientid)
            {
                _logger.LogWarning("ID de URL ({PacienteId}) no coincide con ID del DTO ({PacienteId})", id, updatePacienteDto.pacientid);
                return BadRequest($"El ID de PacienteId ({id}) no coincide con el ID del objeto ({updatePacienteDto.pacientid})");
            }
            var result = await _PacienteService.UpdatePacienteAsync(updatePacienteDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeletePaciente(long id, [FromQuery] string eliminadoPor)
        {
            _logger.LogInformation("Iniciando endpoint DeletePaciente con ID: {PacienteId} [CRUD AUTOMÁTICO]", id);
            var result = await _PacienteService.DeletePacienteAsync(id, eliminadoPor);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetAllPaciente()
        {
            _logger.LogInformation("Iniciando endpoint GetAllPaciente [CRUD AUTOMÁTICO]");
            var result = await _PacienteService.GetAllPacienteAsync();
            return Ok(result);
        }

        [HttpGet("where")]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetWhere([FromQuery] string condicion)
        {
            _logger.LogInformation("Iniciando endpoint GetWhere con condición: {Condicion} [CRUD AUTOMÁTICO]", condicion);
            var result = await _PacienteService.GetWhereAsync(condicion);
            return Ok(result);
        }
        /// <summary>
        /// Obtiene un paciente por su número de documento
        /// </summary>
        /// <param name="documentNumber">Número de documento del paciente</param>
        /// <returns>Información del paciente</returns>
        /// <response code="200">Devuelve el paciente encontrado o null si no existe</response>
        /// <response code="400">Si el número de documento está vacío</response>
        /// <response code="500">Si ocurre un error interno en el servidor</response>
        [HttpGet("document/{documentNumber}")]
        [ProducesResponseType(typeof(PacienteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PacienteDto>> GetByDocumentNumber([FromRoute] string documentNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(documentNumber))
                {
                    return BadRequest("El número de documento no puede estar vacío");
                }

                var paciente = await _PacienteService.GetByDocumentNumberAsync(documentNumber);
                return Ok(paciente);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener paciente por número de documento: {DocumentNumber}", documentNumber);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor");
            }
        }
        /// <summary>
        /// Busca pacientes por DNI, nombres y/o apellidos
        /// </summary>
        /// <param name="documentNumber">Número de documento (opcional)</param>
        /// <param name="names">Nombres (opcional)</param>
        /// <param name="lastNames">Apellidos (opcional)</param>
        /// <returns>Lista de pacientes que coinciden con los criterios de búsqueda</returns>
        /// <response code="200">Devuelve la lista de pacientes encontrados</response>
        /// <response code="400">Si no se proporciona ningún criterio de búsqueda</response>
        /// <response code="500">Si ocurre un error interno en el servidor</response>
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<PacienteDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PacienteDto>>> SearchPacientes(
            [FromQuery] string? documentNumber,
            [FromQuery] string? names,
            [FromQuery] string? lastNames)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(documentNumber) && 
                    string.IsNullOrWhiteSpace(names) && 
                    string.IsNullOrWhiteSpace(lastNames))
                {
                    return BadRequest("Debe proporcionar al menos un criterio de búsqueda (DNI, nombres o apellidos)");
                }

                var pacientes = await _PacienteService.SearchPacientesAsync(documentNumber, names, lastNames);
                return Ok(pacientes);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar pacientes con los filtros especificados");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor");
            }
        }
    }

}
