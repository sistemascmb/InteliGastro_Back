using AutoMapper;
using Domain.DomainInterfaces;
using Infraestructure.Models;
using WebApi.Application.DTO.Paciente;
using WebApi.Application.Services.Paciente;

namespace WebApi.Application.Services.Paciente
{
    public class PacienteService : IPacienteService
    {
        private readonly ILogger<PacienteService> _logger;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly ICentroRepository _centroRepository;
        private readonly IMapper _mapper;
        public PacienteService(
            ILogger<PacienteService> logger,
            IPacienteRepository pacienteRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _pacienteRepository = pacienteRepository ?? throw new ArgumentNullException(nameof(pacienteRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PacienteDto> CreatePacienteAsync(CreatePacienteDto createPacienteDto)
        {
            try
            {
                _logger.LogInformation("Iniciando creación de Paciente");
                
                var PacienteEntity = _mapper.Map<PacienteEntity>(createPacienteDto);
                PacienteEntity.IsDeleted = false;
                PacienteEntity.CreatedAt = DateTime.Now;
                var newPacienteId = await _pacienteRepository.CreatePacienteAsync(PacienteEntity);
                _logger.LogInformation("Paciente creado con ID: {PacienteId}", newPacienteId);
                var newPaciente = await _pacienteRepository.GetPacienteByIdAsync(newPacienteId);
                if (newPaciente == null)
                {
                    throw new Exception("Error al recuperar el Paciente recién creado.");
                }

                var newPacienteDto = _mapper.Map<PacienteEntity>(newPaciente);
                var PacienteDto = _mapper.Map<PacienteDto>(newPaciente);
                return PacienteDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear Paciente");
                throw;
            }
        }

        public async Task<bool> DeletePacienteAsync(long PacienteId, string eliminadoPor)
        {
            try
            {
                _logger.LogInformation("Iniciando eliminación lógica de Paciente con ID: {PacienteId}", PacienteId);
                if (PacienteId <= 0)
                {
                    throw new ArgumentException("El ID del Paciente debe ser mayor a 0", nameof(PacienteId));
                }
                var PacienteExist = await _pacienteRepository.GetPacienteByIdAsync(PacienteId);
                var PacienteExistente = (PacienteEntity)PacienteExist;
                if (PacienteExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Paciente con ID: {PacienteId}");
                }
                if (PacienteExistente.IsDeleted)
                {
                    throw new InvalidOperationException($"No se puede eliminar el Paciente con ID: {PacienteId} por que ya está eiminado lógicamente.");
                }
                PacienteExistente.IsDeleted = true;
                PacienteExistente.UpdatedAt = DateTime.Now;
                PacienteExistente.UpdatedBy = eliminadoPor;

                var result = await _pacienteRepository.UpdatePacienteAsync(PacienteExistente);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo eliminar el Paciente con ID: {PacienteId}");
                }
                _logger.LogInformation("Paciente con ID: {PacienteId} eliminado lógicamente", PacienteId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el Paciente con ID: {PacienteId}", PacienteId);
                throw;
            }
        }

        public async Task<IEnumerable<PacienteDto>> GetAllPacienteAsync()
        {
            Task<IEnumerable<PacienteDto>> PacienteDto;
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de todos los Paciente");
                var Paciente = _pacienteRepository.GetAllPacienteAsync().Result;
                var PacienteList = Paciente.Where(e => !((PacienteEntity)e).IsDeleted).ToList();
                PacienteDto = Task.FromResult(_mapper.Map<IEnumerable<PacienteDto>>(PacienteList));
                _logger.LogInformation("Paciente obtenidos: {Count}", PacienteList.Count());
                return await PacienteDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los Paciente");
                throw;
            }
        }

        public async Task<PacienteDto> GetPacienteByIdAsync(long PacienteId)
        {
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de Paciente por ID: {PacienteId}", PacienteId);
                if (PacienteId <= 0)
                {
                    throw new ArgumentException("El ID del Paciente debe ser mayor a 0", nameof(PacienteId));
                }

                var Paciente = await _pacienteRepository.GetPacienteByIdAsync(PacienteId);
                if (Paciente == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Paciente con ID: {PacienteId}");
                }
                var PacienteEntity = (PacienteEntity)Paciente;
                if (PacienteEntity.IsDeleted)
                {
                    throw new InvalidOperationException($"El Paciente con ID: {PacienteId} está eliminado lógicamente.");
                }
                var PacienteDto = _mapper.Map<PacienteDto>(PacienteEntity);
                _logger.LogInformation("Paciente obtenido con ID: {PacienteId}", PacienteId);
                return PacienteDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<IEnumerable<PacienteDto>> GetWhereAsync(string condicion)
        {
            PacienteDto[] PacienteDto;
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de Paciente con condición: {condicion}", condicion);
                var Paciente = _pacienteRepository.GetAllPacienteAsync().Result;
                var PacienteList = Paciente.Where(e => !((PacienteEntity)e).IsDeleted).ToList();
                if (!string.IsNullOrWhiteSpace(condicion))
                {
                    PacienteList = PacienteList.Where(e =>
                    {
                        var entity = (PacienteEntity)e;
                        return (entity.DocumentNumber != null && entity.DocumentNumber.Contains(condicion, StringComparison.OrdinalIgnoreCase)) ||
                               (entity.Names != null && entity.Names.Contains(condicion, StringComparison.OrdinalIgnoreCase)) ||
                               (entity.LastNames != null && entity.LastNames.Contains(condicion, StringComparison.OrdinalIgnoreCase));
                    }).ToList();
                }
                PacienteDto = _mapper.Map<PacienteDto[]>(PacienteList);
                _logger.LogInformation("Paciente obtenidos con condición: {Count}", PacienteDto.Length);
                return Task.FromResult<IEnumerable<PacienteDto>>(PacienteDto);
            }
            catch (Exception)
            {
                _logger.LogError("Error al obtener los Paciente con condición: {condicion}", condicion);
                throw;
            }
        }

        public async Task<PacienteDto> UpdatePacienteAsync(UpdatePacienteDto updatePacienteDto)
        {
            try
            {
                _logger.LogInformation("Iniciando actualización de Paciente con ID: {PacienteId}", updatePacienteDto.pacientid);
                if (updatePacienteDto.pacientid <= 0)
                {
                    throw new ArgumentException("El ID del Paciente debe ser mayor a 0", nameof(updatePacienteDto.pacientid));
                }
               
                var PacienteExist = await _pacienteRepository.GetPacienteByIdAsync(updatePacienteDto.pacientid);
                if (PacienteExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Paciente con ID: {updatePacienteDto.pacientid}");
                }
                var PacienteExistente = (PacienteEntity)PacienteExist;
                if (PacienteExistente.IsDeleted)
                {
                    throw new InvalidOperationException($"No se puede actualizar el Paciente con ID: {updatePacienteDto.pacientid} por que está eliminado lógicamente.");
                }
                var PacienteToUpdate = _mapper.Map<PacienteEntity>(updatePacienteDto);
                PacienteToUpdate.CreatedAt = PacienteExistente.CreatedAt;
                PacienteToUpdate.CreatedBy = PacienteExistente.CreatedBy;
                PacienteToUpdate.IsDeleted = false;
                PacienteToUpdate.UpdatedAt = DateTime.Now;


                var result = await _pacienteRepository.UpdatePacienteAsync(PacienteToUpdate);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo actualizar el Paciente con ID: {updatePacienteDto.pacientid}");
                }
                var PacienteActualizado = await _pacienteRepository.GetPacienteByIdAsync(updatePacienteDto.pacientid);
                if (PacienteActualizado == null)
                {
                    throw new Exception("Error al recuperar el Paciente recién actualizado.");
                }
                var estudioEntity2 = (PacienteEntity)PacienteActualizado;
                var PacienteDto = _mapper.Map<PacienteDto>(estudioEntity2);
                return PacienteDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el Paciente con ID: {PacienteId}", updatePacienteDto.pacientid);
                throw;
            }
        }

        public async Task<PacienteDto?> GetByDocumentNumberAsync(string documentNumber)
        {
            try
            {
                _logger.LogInformation("Iniciando búsqueda de paciente por número de documento: {DocumentNumber}", documentNumber);
                
                if (string.IsNullOrWhiteSpace(documentNumber))
                {
                    throw new ArgumentException("El número de documento no puede estar vacío", nameof(documentNumber));
                }

                var paciente = await _pacienteRepository.GetByConditionAsync(documentNumber);
                if (paciente == null)
                {
                    _logger.LogInformation("No se encontró ningún paciente con el número de documento: {DocumentNumber}", documentNumber);
                    return null;
                }

                var pacienteEntity = (PacienteEntity)paciente;
                if (pacienteEntity.IsDeleted)
                {
                    _logger.LogInformation("El paciente con número de documento {DocumentNumber} está eliminado lógicamente", documentNumber);
                    return null;
                }

                var pacienteDto = _mapper.Map<PacienteDto>(pacienteEntity);
                _logger.LogInformation("Paciente encontrado con número de documento: {DocumentNumber}", documentNumber);
                return pacienteDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar paciente por número de documento: {DocumentNumber}", documentNumber);
                throw;
            }
        }

        public async Task<IEnumerable<PacienteDto>> SearchPacientesAsync(string? documentNumber, string? names, string? lastNames)
        {
            try
            {
                _logger.LogInformation("Iniciando búsqueda de pacientes con filtros");

                if (string.IsNullOrWhiteSpace(documentNumber) && 
                    string.IsNullOrWhiteSpace(names) && 
                    string.IsNullOrWhiteSpace(lastNames))
                {
                    throw new ArgumentException("Debe proporcionar al menos un criterio de búsqueda");
                }

                var pacientes = await _pacienteRepository.SearchPacientesAsync(documentNumber, names, lastNames);
                var pacientesDto = pacientes.Select(p => _mapper.Map<PacienteDto>((PacienteEntity)p));

                _logger.LogInformation("Se encontraron {Count} pacientes con los filtros especificados", 
                    pacientesDto.Count());

                return pacientesDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar pacientes con los filtros especificados");
                throw;
            }
        }
    }
}
