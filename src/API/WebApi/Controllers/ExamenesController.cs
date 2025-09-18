using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DTO.Examenes;
using WebApi.Application.DTO.Salas;
using WebApi.Application.Services.Examenes;
using WebApi.Application.Services.Salas;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ExamenesController : Controller
    {
        private readonly ILogger<ExamenesController> _logger;
        private readonly IExamenesService _examenesService;

        public ExamenesController(ILogger<ExamenesController> logger, IExamenesService examenesService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _examenesService = examenesService ?? throw new ArgumentNullException(nameof(examenesService));
        }

        [HttpPost]
        [ProducesResponseType(typeof(object),201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateExamenes([FromBody] CreateExamenesDto createExamenesDto)
        {
            _logger.LogInformation("Inicio de método CreateExamenes [CRUD]");
            var result = await _examenesService.CreateSalasAsync(createExamenesDto);
            return CreatedAtAction(nameof(GetExamenesById), new { id = result.examsid }, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetExamenesById(long id)
        {
            _logger.LogInformation("Iniciando endpoint GetExamenesById con ID: {examsid} [CRUD AUTOMÁTICO]", id);
            var result = await _examenesService.GetExamenesByIdAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateExamenes(long id, [FromBody] UpdateExamenesDto updateExamenesDto)
        {
            _logger.LogInformation("Inciando endpoint UpdateExamenes con ID: {examsid} [CRUD AUTOMÁTICO]", id);
            if (id != updateExamenesDto.examsid)
            {
                _logger.LogWarning("ID de URL ({SalasId}) no coincide con ID del DTO ({SalasId})", id, updateExamenesDto.examsid);
                return BadRequest($"El ID de SalasId ({id}) no coincide con el ID del objeto ({updateExamenesDto.examsid})");
            }
            var result = await _examenesService.UpdateExamenesAsync(updateExamenesDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteExamns(long id, [FromQuery] string eliminadoPor)
        {
            _logger.LogInformation("Iniciando endpoint DeleteSalas con ID: {SalasId} [CRUD AUTOMÁTICO]", id);
            var result = await _examenesService.DeleteExamenesAsync(id, eliminadoPor);
            return Ok(result);
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetWhere([FromQuery] string condicion)
        {
            _logger.LogInformation("Iniciando endpoint GetWhere con condición: {Condicion} [CRUD AUTOMÁTICO]", condicion);
            var result = await _examenesService.GetWhereAsync(condicion);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllSalas()
        {
            _logger.LogInformation("Iniciando endpoint GetAllSalas [CRUD AUTOMÁTICO]");
            var result = await _examenesService.GetAllExamenesAsync();
            return Ok(result);
        }
    }
}
