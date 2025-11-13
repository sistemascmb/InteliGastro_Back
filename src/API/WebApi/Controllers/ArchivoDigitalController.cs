using Domain.Entities;
using Domain.RepositoriesInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Application.DTO.ArchivoDigital;
using WebApi.Application.DTO.ArchivoDigital;
using WebApi.Application.DTO.ArchivoDigital;
using WebApi.Application.Services;
using WebApi.Application.Services.ArchivoDigital;
using WebApi.Application.Services.ArchivoDigital;

namespace WebApi.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]_ArchivoDigitalService

    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ArchivoDigitalController : ControllerBase
    {
        ///
        private readonly IArchivoDigitalService _archivodigital;
        ///
        private readonly ILogger<ArchivoDigitalController> _logger;
        private readonly IArchivoDigitalServiceN _ArchivoDigitalService;
        public ArchivoDigitalController(IArchivoDigitalService archivodigital, ILogger<ArchivoDigitalController> logger, IArchivoDigitalServiceN ArchivoDigitalService)
        {
            ///
            _archivodigital = archivodigital;
            ///
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _ArchivoDigitalService = ArchivoDigitalService ?? throw new ArgumentNullException(nameof(ArchivoDigitalService));
        }

        //
        [HttpPost]
        [Route("ArchivoDigital")]
        public async Task<IActionResult> CreateArchivoDigitalLaboratorioAsyncc([FromBody] ArchivoDigitalRequestDto request) 
        {
            var resultado = await _archivodigital.CreateArchivoDigitalLaboratorioAsyncc(request);
            return StatusCode(201, resultado);
        }
        //

        [HttpPost]
        [ProducesResponseType(typeof(object), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateArchivoDigital([FromBody] CreateArchivoDigitalDto createArchivoDigitalDto)
        {
            _logger.LogInformation("Inicio del método CreateArchivoDigital [CRUD]");
            try
            {
                var result = await _ArchivoDigitalService.CreateArchivoDigitalAsync(createArchivoDigitalDto);
                _logger.LogInformation("ArchivoDigital creado correctamente con ID: {DigitalFileId}", result.digitalfileid);
                return CreatedAtAction(nameof(GetArchivoDigitalById), new { id = result.digitalfileid }, result);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Error de validación al crear ArchivoDigital");
                return BadRequest(BuildErrorResponse(ex));
            }
            catch (AggregateException ex)
            {
                _logger.LogError(ex, "Error agregado al crear ArchivoDigital");
                return StatusCode(StatusCodes.Status500InternalServerError, BuildErrorResponse(ex));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al crear ArchivoDigital");
                return StatusCode(StatusCodes.Status500InternalServerError, BuildErrorResponse(ex));
            }
        }

        private object BuildErrorResponse(Exception ex)
        {
            var innerMessages = new List<string>();
            var current = ex.InnerException;
            while (current != null)
            {
                innerMessages.Add(current.Message);
                current = current.InnerException;
            }

            return new
            {
                traceId = HttpContext.TraceIdentifier,
                path = HttpContext?.Request?.Path.Value,
                message = ex.Message,
                type = ex.GetType().FullName,
                source = ex.Source,
                stackTrace = ex.StackTrace,
                innerMessages
            };
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetArchivoDigitalById(long id)
        {
            _logger.LogInformation("Iniciando endpoint GetArchivoDigitalById con ID: {digitalfileid} [CRUD AUTOMÁTICO]", id);
            var result = await _ArchivoDigitalService.GetArchivoDigitalByIdAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateArchivoDigital(long id, [FromBody] UpdateArchivoDigitalDto updateArchivoDigitalDto)
        {
            _logger.LogInformation("Iniciando endpoint UpdateArchivoDigital con ID: {digitalfileid} [CRUD AUTOMÁTICO]", id);
            // Validar que el ID de la URL coincida con el ID del DTO
            if (id != updateArchivoDigitalDto.digitalfileid)
            {
                _logger.LogWarning("ID de URL ({digitalfileid}) no coincide con ID del DTO ({digitalfileid})", id, updateArchivoDigitalDto.digitalfileid);
                return BadRequest($"El ID de digitalfileid ({id}) no coincide con el ID del objeto ({updateArchivoDigitalDto.digitalfileid})");
            }
            var result = await _ArchivoDigitalService.UpdateArchivoDigitalAsync(updateArchivoDigitalDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteArchivoDigital(long id, [FromQuery] string eliminadoPor)
        {
            _logger.LogInformation("Iniciando endpoint DeleteArchivoDigital con ID: {digitalfileid} [CRUD AUTOMÁTICO]", id);
            var result = await _ArchivoDigitalService.DeleteArchivoDigitalAsync(id, eliminadoPor);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetAllArchivoDigital()
        {
            _logger.LogInformation("Iniciando endpoint GetAllArchivoDigital [CRUD AUTOMÁTICO]");
            var result = await _ArchivoDigitalService.GetAllArchivoDigitalAsync();
            return Ok(result);
        }

        [HttpGet("where")]
        [ProducesResponseType(typeof(IEnumerable<object>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> GetWhere([FromQuery] string condicion)
        {
            _logger.LogInformation("Iniciando endpoint GetWhere con condición: {Condicion} [CRUD AUTOMÁTICO]", condicion);
            var result = await _ArchivoDigitalService.GetWhereAsync(condicion);
            return Ok(result);
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<ArchivoDigitalDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ArchivoDigitalDto>>> SearchArchivoDigitals(
            [FromQuery] string? value1,
            [FromQuery] string? value2,
            [FromQuery] string? value3)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(value1) &&
                    string.IsNullOrWhiteSpace(value2) &&
                    string.IsNullOrWhiteSpace(value3))
                {
                    return BadRequest("Debe proporcionar al menos un criterio de búsqueda (value1, value2 o value3)");
                }

                var ArchivoDigitals = await _ArchivoDigitalService.SearchArchivoDigitalsAsync(value1, value2, value3);
                return Ok(ArchivoDigitals);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar ArchivoDigitals con los filtros especificados");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno del servidor");
            }
        }



    }
}
