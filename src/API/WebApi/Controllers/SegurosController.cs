using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DTO.Seguros;
using WebApi.Application.Services.Seguros;
using WebApi.Application.Services.Seguros;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SegurosController : Controller
    {
        private readonly ILogger<SegurosController> _logger;
        private readonly ISegurosService _segurosService;

        public SegurosController(ILogger<SegurosController> logger, ISegurosService segurosService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _segurosService = segurosService ?? throw new ArgumentNullException(nameof(segurosService));
        }

        [HttpPost]
        [ProducesResponseType(typeof(object), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateSeguros([FromBody] CreateSegurosDto createSegurosDto)
        {
            _logger.LogInformation("Inicio del método CreateSeguros [CRUD]");
            var result = await _segurosService.CreateSegurosAsync(createSegurosDto);
            return CreatedAtAction(nameof(GetSegurosById), new { id = result.insuranceid }, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetSegurosById(long id)
        {
            _logger.LogInformation("Iniciando endpoint GetSegurosById con ID: {insuranceid} [CRUD AUTOMÁTICO]", id);
            var result = await _segurosService.GetSegurosByIdAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateSeguros(long id, [FromBody] UpdateSegurosDto updateSegurosDto)
        {
            _logger.LogInformation("Iniciando endpoint UpdateSeguros con ID: {EstudiosId} [CRUD AUTOMÁTICO]", id);
            // Validar que el ID de la URL coincida con el ID del DTO
            if (id != updateSegurosDto.insuranceid)
            {
                _logger.LogWarning("ID de URL ({insuranceid}) no coincide con ID del DTO ({insuranceid})", id, updateSegurosDto.insuranceid);
                return BadRequest($"El ID de SegurosId ({id}) no coincide con el ID del objeto ({updateSegurosDto.insuranceid})");
            }
            var result = await _segurosService.UpdateSegurosAsync(updateSegurosDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteSeguros(long id, [FromQuery] string eliminadoPor)
        {
            _logger.LogInformation("Iniciando endpoint DeleteSeguros con ID: {insuranceid} [CRUD AUTOMÁTICO]", id);
            var result = await _segurosService.DeleteSegurosAsync(id, eliminadoPor);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetAllSeguros()
        {
            _logger.LogInformation("Iniciando endpoint GetAllSeguros [CRUD AUTOMÁTICO]");
            var result = await _segurosService.GetAllSegurosAsync();
            return Ok(result);
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetWhere([FromQuery] string condicion)
        {
            _logger.LogInformation("Iniciando endpoint GetWhere con condición: {Condicion} [CRUD AUTOMÁTICO]", condicion);
            var result = await _segurosService.GetWhereAsync(condicion);
            return Ok(result);
        }
    }
}
