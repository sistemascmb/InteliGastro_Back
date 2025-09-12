using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DTO.SystemUsers;
using WebApi.Application.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SystemUsersController : ControllerBase
    {
        private readonly ISystemUsersService _systemUsers;
        private readonly ILogger<SystemUsersController> _logger;

        public SystemUsersController(ISystemUsersService systemUsers, ILogger<SystemUsersController> logger)
        {
            _systemUsers = systemUsers ?? throw new ArgumentNullException(nameof(systemUsers));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        [HttpPost]
        [ProducesResponseType(typeof(SystemUsersDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateSystemUsers([FromBody] CreateSystemUsersDto createSystemUsersDto)
        {
            _logger.LogInformation("Inicio del método CreateSystemUsers [CRUD]");
            var result = await _systemUsers.CreateSystemUsersAsync(createSystemUsersDto);

            return CreatedAtAction(nameof(GetSystemUsersById), new { id = result.userid }, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SystemUsersDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetSystemUsersById(long id)
        {
            _logger.LogInformation("Iniciando endpoint GetSystemUsersById con ID: {UserId} [CRUD AUTOMÁTICO]", id);

            var result = await _systemUsers.GetSystemUsersByIdAsync(id);

            return Ok(result);
        }
    }
}
