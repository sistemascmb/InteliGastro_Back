using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DTO.Suministros;
using WebApi.Application.Services.Suministros;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SuministrosController : Controller
    {
        private readonly ILogger<SuministrosController> _logger;
        private readonly ISuministrosService _SuministrosService;
        public SuministrosController(ILogger<SuministrosController> logger, ISuministrosService SuministrosService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _SuministrosService = SuministrosService ?? throw new ArgumentNullException(nameof(SuministrosService));
        }
        [HttpPost]
        [ProducesResponseType(typeof(object), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateSuministros([FromBody] CreateSuministrosDto createSuministrosDto)
        {
            _logger.LogInformation("Inicio del método CreateSuministros [CRUD]");
            var result = await _SuministrosService.CreateSuministrosAsync(createSuministrosDto);
            return CreatedAtAction(nameof(GetSuministrosById), new { id = result.provisionid }, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetSuministrosById(long id)
        {
            _logger.LogInformation("Iniciando endpoint GetSuministrosById con ID: {SuministrosId} [CRUD AUTOMÁTICO]", id);
            var result = await _SuministrosService.GetSuministrosByIdAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateSuministros(long id, [FromBody] UpdateSuministrosDto updateSuministrosDto)
        {
            _logger.LogInformation("Iniciando endpoint UpdateSuministros con ID: {SuministrosId} [CRUD AUTOMÁTICO]", id);
            // Validar que el ID de la URL coincida con el ID del DTO
            if (id != updateSuministrosDto.provisionid)
            {
                _logger.LogWarning("ID de URL ({SuministrosId}) no coincide con ID del DTO ({SuministrosId})", id, updateSuministrosDto.provisionid);
                return BadRequest($"El ID de SuministrosId ({id}) no coincide con el ID del objeto ({updateSuministrosDto.provisionid})");
            }
            var result = await _SuministrosService.UpdateSuministrosAsync(updateSuministrosDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteSuministros(long id, [FromQuery] string eliminadoPor)
        {
            _logger.LogInformation("Iniciando endpoint DeleteSuministros con ID: {SuministrosId} [CRUD AUTOMÁTICO]", id);
            var result = await _SuministrosService.DeleteSuministrosAsync(id, eliminadoPor);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetAllSuministros()
        {
            _logger.LogInformation("Iniciando endpoint GetAllSuministros [CRUD AUTOMÁTICO]");
            var result = await _SuministrosService.GetAllSuministrosAsync();
            return Ok(result);
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetWhere([FromQuery] string condicion)
        {
            _logger.LogInformation("Iniciando endpoint GetWhere con condición: {Condicion} [CRUD AUTOMÁTICO]", condicion);
            var result = await _SuministrosService.GetWhereAsync(condicion);
            return Ok(result);
        }
    }
}
