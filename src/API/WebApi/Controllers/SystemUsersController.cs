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

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(SystemUsersDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateSystemUsers(long id, [FromBody] UpdateSystemUsersDto updateSystemUsersDto)
        {
            _logger.LogInformation("Iniciando endpoint UpdatePersona con ID: {PersonaId} [CRUD AUTOMÁTICO]", id);

            // Validar que el ID de la URL coincida con el ID del DTO
            if (id != updateSystemUsersDto.UserId)
            {
                _logger.LogWarning("ID de URL ({UserId}) no coincide con ID del DTO ({UserId})", id, updateSystemUsersDto.UserId);
                return BadRequest($"El ID de UserId ({id}) no coincide con el ID del objeto ({updateSystemUsersDto.UserId})");
            }

            var result = await _systemUsers.UpdateSystemUsersAsync(updateSystemUsersDto);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(SystemUsersDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteSystemUsers(long id, [FromQuery] string eliminadoPor)
        {
            _logger.LogInformation("Iniciando endpoint DeleteSystemUsers con ID: {UserId} [CRUD AUTOMÁTICO - ELIMINACIÓN LÓGICA]", id);

            var result = await _systemUsers.DeleteSystemUsersAsync(id, eliminadoPor);

            return Ok(result);
        }
    }
}
