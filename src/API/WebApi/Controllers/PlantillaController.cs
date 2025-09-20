using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DTO.Plantilla;
using WebApi.Application.Services.Plantilla;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PlantillaController : Controller
    {
        private readonly ILogger<PlantillaController> _logger;
        private readonly IPlantillaService _PlantillaService;
        public PlantillaController(ILogger<PlantillaController> logger, IPlantillaService PlantillaService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _PlantillaService = PlantillaService ?? throw new ArgumentNullException(nameof(PlantillaService));
        }
        [HttpPost]
        [ProducesResponseType(typeof(object), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreatePlantilla([FromBody] CreatePlantillaDto createPlantillaDto)
        {
            _logger.LogInformation("Inicio del método CreatePlantilla [CRUD]");
            var result = await _PlantillaService.CreatePlantillaAsync(createPlantillaDto);
            return CreatedAtAction(nameof(GetPlantillaById), new { id = result.templatesid }, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetPlantillaById(long id)
        {
            _logger.LogInformation("Iniciando endpoint GetPlantillaById con ID: {PlantillaId} [CRUD AUTOMÁTICO]", id);
            var result = await _PlantillaService.GetPlantillaByIdAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdatePlantilla(long id, [FromBody] UpdatePlantillaDto updatePlantillaDto)
        {
            _logger.LogInformation("Iniciando endpoint UpdatePlantilla con ID: {PlantillaId} [CRUD AUTOMÁTICO]", id);
            // Validar que el ID de la URL coincida con el ID del DTO
            if (id != updatePlantillaDto.templatesid)
            {
                _logger.LogWarning("ID de URL ({PlantillaId}) no coincide con ID del DTO ({PlantillaId})", id, updatePlantillaDto.templatesid);
                return BadRequest($"El ID de PlantillaId ({id}) no coincide con el ID del objeto ({updatePlantillaDto.templatesid})");
            }
            var result = await _PlantillaService.UpdatePlantillaAsync(updatePlantillaDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeletePlantilla(long id, [FromQuery] string eliminadoPor)
        {
            _logger.LogInformation("Iniciando endpoint DeletePlantilla con ID: {PlantillaId} [CRUD AUTOMÁTICO]", id);
            var result = await _PlantillaService.DeletePlantillaAsync(id, eliminadoPor);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetAllPlantilla()
        {
            _logger.LogInformation("Iniciando endpoint GetAllPlantilla [CRUD AUTOMÁTICO]");
            var result = await _PlantillaService.GetAllPlantillaAsync();
            return Ok(result);
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetWhere([FromQuery] string condicion)
        {
            _logger.LogInformation("Iniciando endpoint GetWhere con condición: {Condicion} [CRUD AUTOMÁTICO]", condicion);
            var result = await _PlantillaService.GetWhereAsync(condicion);
            return Ok(result);
        }
    }
}
