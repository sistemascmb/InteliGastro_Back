using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DTO.Cie10;
using WebApi.Application.Services.Cie10;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class Cie10Controller : Controller
    {
        private readonly ILogger<Cie10Controller> _logger;
        private readonly ICie10Service _Cie10Service;
        public Cie10Controller(ILogger<Cie10Controller> logger, ICie10Service Cie10Service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _Cie10Service = Cie10Service ?? throw new ArgumentNullException(nameof(Cie10Service));
        }
        [HttpPost]
        [ProducesResponseType(typeof(object), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateCie10([FromBody] CreateCie10Dto createCie10Dto)
        {
            _logger.LogInformation("Inicio del método CreateCie10 [CRUD]");
            var result = await _Cie10Service.CreateCie10Async(createCie10Dto);
            return CreatedAtAction(nameof(GetCie10ById), new { id = result.cieid }, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetCie10ById(long id)
        {
            _logger.LogInformation("Iniciando endpoint GetCie10ById con ID: {Cie10Id} [CRUD AUTOMÁTICO]", id);
            var result = await _Cie10Service.GetCie10ByIdAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateCie10(long id, [FromBody] UpdateCie10Dto updateCie10Dto)
        {
            _logger.LogInformation("Iniciando endpoint UpdateCie10 con ID: {Cie10Id} [CRUD AUTOMÁTICO]", id);
            // Validar que el ID de la URL coincida con el ID del DTO
            if (id != updateCie10Dto.cieid)
            {
                _logger.LogWarning("ID de URL ({Cie10Id}) no coincide con ID del DTO ({Cie10Id})", id, updateCie10Dto.cieid);
                return BadRequest($"El ID de Cie10Id ({id}) no coincide con el ID del objeto ({updateCie10Dto.cieid})");
            }
            var result = await _Cie10Service.UpdateCie10Async(updateCie10Dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteCie10(long id, [FromQuery] string eliminadoPor)
        {
            _logger.LogInformation("Iniciando endpoint DeleteCie10 con ID: {Cie10Id} [CRUD AUTOMÁTICO]", id);
            var result = await _Cie10Service.DeleteCie10Async(id, eliminadoPor);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetAllCie10()
        {
            _logger.LogInformation("Iniciando endpoint GetAllCie10 [CRUD AUTOMÁTICO]");
            var result = await _Cie10Service.GetAllCie10Async();
            return Ok(result);
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetWhere([FromQuery] string condicion)
        {
            _logger.LogInformation("Iniciando endpoint GetWhere con condición: {Condicion} [CRUD AUTOMÁTICO]", condicion);
            var result = await _Cie10Service.GetWhereAsync(condicion);
            return Ok(result);
        }
    }
}
