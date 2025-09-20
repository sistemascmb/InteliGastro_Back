using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DTO.MedicoReferencia;
using WebApi.Application.Services.MedicoReferencia;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class MedicoReferenciaController : Controller
    {
        private readonly ILogger<MedicoReferenciaController> _logger;
        private readonly IMedicoReferenciaService _MedicoReferenciaService;
        public MedicoReferenciaController(ILogger<MedicoReferenciaController> logger, IMedicoReferenciaService MedicoReferenciaService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _MedicoReferenciaService = MedicoReferenciaService ?? throw new ArgumentNullException(nameof(MedicoReferenciaService));
        }
        [HttpPost]
        [ProducesResponseType(typeof(object), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateMedicoReferencia([FromBody] CreateMedicoReferenciaDto createMedicoReferenciaDto)
        {
            _logger.LogInformation("Inicio del método CreateMedicoReferencia [CRUD]");
            var result = await _MedicoReferenciaService.CreateMedicoReferenciaAsync(createMedicoReferenciaDto);
            return CreatedAtAction(nameof(GetMedicoReferenciaById), new { id = result.referraldoctorsd }, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetMedicoReferenciaById(long id)
        {
            _logger.LogInformation("Iniciando endpoint GetMedicoReferenciaById con ID: {MedicoReferenciaId} [CRUD AUTOMÁTICO]", id);
            var result = await _MedicoReferenciaService.GetMedicoReferenciaByIdAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateMedicoReferencia(long id, [FromBody] UpdateMedicoReferenciaDto updateMedicoReferenciaDto)
        {
            _logger.LogInformation("Iniciando endpoint UpdateMedicoReferencia con ID: {MedicoReferenciaId} [CRUD AUTOMÁTICO]", id);
            // Validar que el ID de la URL coincida con el ID del DTO
            if (id != updateMedicoReferenciaDto.referraldoctorsd)
            {
                _logger.LogWarning("ID de URL ({MedicoReferenciaId}) no coincide con ID del DTO ({MedicoReferenciaId})", id, updateMedicoReferenciaDto.referraldoctorsd);
                return BadRequest($"El ID de MedicoReferenciaId ({id}) no coincide con el ID del objeto ({updateMedicoReferenciaDto.referraldoctorsd})");
            }
            var result = await _MedicoReferenciaService.UpdateMedicoReferenciaAsync(updateMedicoReferenciaDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteMedicoReferencia(long id, [FromQuery] string eliminadoPor)
        {
            _logger.LogInformation("Iniciando endpoint DeleteMedicoReferencia con ID: {MedicoReferenciaId} [CRUD AUTOMÁTICO]", id);
            var result = await _MedicoReferenciaService.DeleteMedicoReferenciaAsync(id, eliminadoPor);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetAllMedicoReferencia()
        {
            _logger.LogInformation("Iniciando endpoint GetAllMedicoReferencia [CRUD AUTOMÁTICO]");
            var result = await _MedicoReferenciaService.GetAllMedicoReferenciaAsync();
            return Ok(result);
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetWhere([FromQuery] string condicion)
        {
            _logger.LogInformation("Iniciando endpoint GetWhere con condición: {Condicion} [CRUD AUTOMÁTICO]", condicion);
            var result = await _MedicoReferenciaService.GetWhereAsync(condicion);
            return Ok(result);
        }
    }
}
