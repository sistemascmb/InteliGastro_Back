using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DTO.Roles;
using WebApi.Application.Services.Roles;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class RolesController : Controller
    {
        private readonly ILogger<RolesController> _logger;
        private readonly IRolesService _RolesService;
        public RolesController(ILogger<RolesController> logger, IRolesService RolesService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _RolesService = RolesService ?? throw new ArgumentNullException(nameof(RolesService));
        }
        [HttpPost]
        [ProducesResponseType(typeof(object), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateRoles([FromBody] CreateRolesDto createRolesDto)
        {
            _logger.LogInformation("Inicio del método CreateRoles [CRUD]");
            var result = await _RolesService.CreateRolesAsync(createRolesDto);
            return CreatedAtAction(nameof(GetRolesById), new { id = result.profiletypeid }, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetRolesById(long id)
        {
            _logger.LogInformation("Iniciando endpoint GetRolesById con ID: {RolesId} [CRUD AUTOMÁTICO]", id);
            var result = await _RolesService.GetRolesByIdAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateRoles(long id, [FromBody] UpdateRolesDto updateRolesDto)
        {
            _logger.LogInformation("Iniciando endpoint UpdateRoles con ID: {RolesId} [CRUD AUTOMÁTICO]", id);
            // Validar que el ID de la URL coincida con el ID del DTO
            if (id != updateRolesDto.profiletypeid)
            {
                _logger.LogWarning("ID de URL ({RolesId}) no coincide con ID del DTO ({RolesId})", id, updateRolesDto.profiletypeid);
                return BadRequest($"El ID de RolesId ({id}) no coincide con el ID del objeto ({updateRolesDto.profiletypeid})");
            }
            var result = await _RolesService.UpdateRolesAsync(updateRolesDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteRoles(long id, [FromQuery] string eliminadoPor)
        {
            _logger.LogInformation("Iniciando endpoint DeleteRoles con ID: {RolesId} [CRUD AUTOMÁTICO]", id);
            var result = await _RolesService.DeleteRolesAsync(id, eliminadoPor);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetAllRoles()
        {
            _logger.LogInformation("Iniciando endpoint GetAllRoles [CRUD AUTOMÁTICO]");
            var result = await _RolesService.GetAllRolesAsync();
            return Ok(result);
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetWhere([FromQuery] string condicion)
        {
            _logger.LogInformation("Iniciando endpoint GetWhere con condición: {Condicion} [CRUD AUTOMÁTICO]", condicion);
            var result = await _RolesService.GetWhereAsync(condicion);
            return Ok(result);
        }
    }
}
