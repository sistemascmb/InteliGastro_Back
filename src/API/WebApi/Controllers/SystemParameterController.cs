using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DTO.SystemParameter;
using WebApi.Application.Services.SystemParameter;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SystemParameterController : Controller
    {
        private readonly ILogger<SystemParameterController> _logger;
        private readonly ISystemParameterService _SystemParameterService;

        public SystemParameterController(ILogger<SystemParameterController> logger, ISystemParameterService SystemParameterService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _SystemParameterService = SystemParameterService ?? throw new ArgumentNullException(nameof(SystemParameterService));
        }

        [HttpPost]
        [ProducesResponseType(typeof(object), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateSystemParameter([FromBody] CreateSystemParameterDto createSystemParameterDto)
        {
            _logger.LogInformation("Inicio del método CreateSystemParameter [CRUD]");
            var result = await _SystemParameterService.CreateSystemParameterAsync(createSystemParameterDto);
            return CreatedAtAction(nameof(GetSystemParameterById), new { id = result.parameterid }, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetSystemParameterById(long id)
        {
            _logger.LogInformation("Iniciando endpoint GetSystemParameterById con ID: {insuranceid} [CRUD AUTOMÁTICO]", id);
            var result = await _SystemParameterService.GetSystemParameterByIdAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateSystemParameter(long id, [FromBody] UpdateSystemParameterDto updateSystemParameterDto)
        {
            _logger.LogInformation("Iniciando endpoint UpdateSystemParameter con ID: {EstudiosId} [CRUD AUTOMÁTICO]", id);
            // Validar que el ID de la URL coincida con el ID del DTO
            if (id != updateSystemParameterDto.parameterid)
            {
                _logger.LogWarning("ID de URL ({insuranceid}) no coincide con ID del DTO ({insuranceid})", id, updateSystemParameterDto.parameterid);
                return BadRequest($"El ID de SystemParameterId ({id}) no coincide con el ID del objeto ({updateSystemParameterDto.parameterid})");
            }
            var result = await _SystemParameterService.UpdateSystemParameterAsync(updateSystemParameterDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteSystemParameter(long id, [FromQuery] string eliminadoPor)
        {
            _logger.LogInformation("Iniciando endpoint DeleteSystemParameter con ID: {insuranceid} [CRUD AUTOMÁTICO]", id);
            var result = await _SystemParameterService.DeleteSystemParameterAsync(id, eliminadoPor);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetAllSystemParameter()
        {
            _logger.LogInformation("Iniciando endpoint GetAllSystemParameter [CRUD AUTOMÁTICO]");
            var result = await _SystemParameterService.GetAllSystemParameterAsync();
            return Ok(result);
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetWhere([FromQuery] string condicion)
        {
            _logger.LogInformation("Iniciando endpoint GetWhere con condición: {Condicion} [CRUD AUTOMÁTICO]", condicion);
            var result = await _SystemParameterService.GetWhereAsync(condicion);
            return Ok(result);
        }

        [HttpGet("group/{groupId}")]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetSystemUsersbyGroupId(int groupId)
        {
            _logger.LogInformation("Iniciando endpoint GetSystemUsersbyGroupId");
            var result = await _SystemParameterService.GetSystemUsersbyGroupId(groupId);
            return Ok(result);
        }

        [HttpGet("group/{groupId}/rest")]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetSystemUsersbyGroupIdRest(int groupId)
        {
            _logger.LogInformation("Iniciando endpoint GetSystemUsersbyGroupIdRest");
            var result = await _SystemParameterService.GetSystemUsersbyGroupIdRest(groupId);
            return Ok(result);
        }

        [HttpGet("group/{groupId}/parent/{parentId}")]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetSystemUsersbyGroupIdParentId(int groupId, int parentId)
        {
            _logger.LogInformation("Iniciando endpoint GetSystemUsersbyGroupIdParentId");
            var result = await _SystemParameterService.GetSystemUsersbyGroupIdParentId(groupId, parentId);
            return Ok(result);
        }
    }
}
