using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DTO.Salas;
using WebApi.Application.Services.Salas;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SalasController : Controller
    {
        private readonly ILogger<SalasController> _logger;
        private readonly ISalasService _salasService;
        public SalasController(ILogger<SalasController> logger, ISalasService salasService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _salasService = salasService ?? throw new ArgumentNullException(nameof(salasService));
        }

        [HttpPost]
        [ProducesResponseType(typeof(object), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateSalas([FromBody] CreateSalasDto createSalasDto)
        {
            _logger.LogInformation("Inicio del método CreateSalas [CRUD]");
            var result = await _salasService.CreateSalasAsync(createSalasDto);
            return CreatedAtAction(nameof(GetSalasById), new { id = result.procedureroomid }, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetSalasById(long id)
        {
            _logger.LogInformation("Iniciando endpoint GetSalasById con ID: {SalasId} [CRUD AUTOMÁTICO]", id);
            var result = await _salasService.GetSalasByIdAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateSalas(long id, [FromBody] UpdateSalasDto updateSalasDto)
        {
            _logger.LogInformation("Iniciando endpoint UpdateSalas con ID: {SalasId} [CRUD AUTOMÁTICO]", id);
            // Validar que el ID de la URL coincida con el ID del DTO
            if (id != updateSalasDto.procedureroomid)
            {
                _logger.LogWarning("ID de URL ({SalasId}) no coincide con ID del DTO ({SalasId})", id, updateSalasDto.procedureroomid);
                return BadRequest($"El ID de SalasId ({id}) no coincide con el ID del objeto ({updateSalasDto.procedureroomid})");
            }
            var result = await _salasService.UpdateSalasAsync(updateSalasDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteSalas(long id, [FromQuery] string eliminadoPor)
        {
            _logger.LogInformation("Iniciando endpoint DeleteSalas con ID: {SalasId} [CRUD AUTOMÁTICO]", id);
            var result = await _salasService.DeleteSalasAsync(id, eliminadoPor);
            return Ok(result);
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetWhere([FromQuery] string condicion)
        {
            _logger.LogInformation("Iniciando endpoint GetWhere con condición: {Condicion} [CRUD AUTOMÁTICO]", condicion);
            var result = await _salasService.GetWhereAsync(condicion);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllSalas()
        {
            _logger.LogInformation("Iniciando endpoint GetAllSalas [CRUD AUTOMÁTICO]");
            var result = await _salasService.GetAllSalasAsync();
            return Ok(result);
        }
    }
}
