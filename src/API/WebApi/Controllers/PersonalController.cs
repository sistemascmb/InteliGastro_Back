using Microsoft.AspNetCore.Mvc;
using WebApi.Application.DTO.Personal;
using WebApi.Application.Services.Personal;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PersonalController : Controller
    {
        private readonly ILogger<PersonalController> _logger;
        private readonly IPersonalService _personalService;
        public PersonalController(ILogger<PersonalController> logger, IPersonalService personalService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _personalService = personalService ?? throw new ArgumentNullException(nameof(personalService));
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(object), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreatePersonal([FromBody] CreatePersonalDto createPersonalDto)
        {
            _logger.LogInformation("Inicio del método CreatePersonal [CRUD]");
            var result = await _personalService.CreatePersonalAsync(createPersonalDto);
            return CreatedAtAction(nameof(GetPersonalById), new { id = result.personalid }, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetPersonalById(long id)
        {
            _logger.LogInformation("Iniciando endpoint GetPersonalById con ID: {PersonalId} [CRUD AUTOMÁTICO]", id);
            var result = await _personalService.GetPersonalByIdAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdatePersonal(long id, [FromBody] UpdatePersonalDto updatePersonalDto)
        {
            _logger.LogInformation("Iniciando endpoint UpdatePersonal con ID: {PersonalId} [CRUD AUTOMÁTICO]", id);
            // Validar que el ID de la URL coincida con el ID del DTO
            if (id != updatePersonalDto.personalid)
            {
                _logger.LogWarning("ID de URL ({PersonalId}) no coincide con ID del DTO ({PersonalId})", id, updatePersonalDto.personalid);
                return BadRequest($"El ID de PersonalId ({id}) no coincide con el ID del objeto ({updatePersonalDto.personalid})");
            }
            var result = await _personalService.UpdatePersonalAsync(updatePersonalDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeletePersonal(long id, [FromQuery] string eliminadoPor)
        {
            _logger.LogInformation("Iniciando endpoint DeletePersonal con ID: {PersonalId} [CRUD AUTOMÁTICO]", id);
            var result = await _personalService.DeletePersonalAsync(id, eliminadoPor);
            return Ok(result);
        }
    }
}
