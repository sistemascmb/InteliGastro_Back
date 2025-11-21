using AutoMapper;
using Domain.DomainInterfaces;
using Infraestructure.Models;
using WebApi.Application.DTO.AgendaDx;
using WebApi.Application.DTO.ArchivoDigital;
using WebApi.Application.Services.AgendaDx;

namespace WebApi.Application.Services.AgendaDx
{
    public class AgendaDxService: IAgendaDxService
    {
        private readonly ILogger<AgendaDxService> _logger;
        private readonly IAgendaDxRepository _agendaDxRepository;
        private readonly IAgendaRepository _agendaRepository;
        private readonly IMapper _mapper;
        public AgendaDxService(
            ILogger<AgendaDxService> logger,
            IAgendaDxRepository AgendaDxRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _agendaDxRepository = AgendaDxRepository ?? throw new ArgumentNullException(nameof(AgendaDxRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<AgendaDxDto> CreateAgendaDxAsync(CreateAgendaDx createAgendaDxDto)
        {
            try
            {
                _logger.LogInformation("Iniciando creación de AgendaDx");

                var AgendaDxEntity = _mapper.Map<AgendaDxEntity>(createAgendaDxDto);

                AgendaDxEntity.IsDeleted = false;
                AgendaDxEntity.CreatedAt = DateTime.Now;

                var newmedicalscheduledxid = await _agendaDxRepository.CreateAgendaDxAsync(AgendaDxEntity);
                _logger.LogInformation("AgendaDx creado con ID: {medicalscheduledxid}", newmedicalscheduledxid);
                var newAgendaDx = await _agendaDxRepository.GetAgendaDxByIdAsync(newmedicalscheduledxid);
                if (newAgendaDx == null)
                {
                    throw new Exception("Error al recuperar el AgendaDx recién creado.");
                }

                var newAgendaDxDto = _mapper.Map<AgendaDxEntity>(newAgendaDx);
                var AgendaDxDto = _mapper.Map<AgendaDxDto>(newAgendaDx);
                return AgendaDxDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear AgendaDx");
                throw;
            }
        }

        public async Task<bool> DeleteAgendaDxAsync(long medicalscheduledxid, string eliminadoPor)
        {
            try
            {
                _logger.LogInformation("Iniciando eliminación lógica de AgendaDx con ID: {medicalscheduledxid}", medicalscheduledxid);
                if (medicalscheduledxid <= 0)
                {
                    throw new ArgumentException("El ID del AgendaDx debe ser mayor a 0", nameof(medicalscheduledxid));
                }
                var AgendaDxExist = await _agendaDxRepository.GetAgendaDxByIdAsync(medicalscheduledxid);
                var AgendaDxExistente = (AgendaDxEntity)AgendaDxExist;
                if (AgendaDxExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el AgendaDx con ID: {medicalscheduledxid}");
                }
                if (AgendaDxExistente.IsDeleted)
                {
                    throw new InvalidOperationException($"No se puede eliminar el AgendaDx con ID: {medicalscheduledxid} por que ya está eiminado lógicamente.");
                }
                AgendaDxExistente.IsDeleted = true;
                AgendaDxExistente.UpdatedAt = DateTime.Now;
                AgendaDxExistente.UpdatedBy = eliminadoPor;

                var result = await _agendaDxRepository.UpdateAgendaDxAsync(AgendaDxExistente);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo eliminar el AgendaDx con ID: {medicalscheduledxid}");
                }
                _logger.LogInformation("AgendaDx con ID: {medicalscheduledxid} eliminado lógicamente", medicalscheduledxid);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el AgendaDx con ID: {medicalscheduledxid}", medicalscheduledxid);
                throw;
            }
        }

        public async Task<IEnumerable<AgendaDxDto>> GetAllAgendaDxAsync()
        {
            Task<IEnumerable<AgendaDxDto>> AgendaDxDto;
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de todos los AgendaDx");
                var AgendaDx = _agendaDxRepository.GetAllAgendaDxAsync().Result;
                var AgendaDxList = AgendaDx.Where(e => !((AgendaDxEntity)e).IsDeleted).ToList();
                AgendaDxDto = Task.FromResult(_mapper.Map<IEnumerable<AgendaDxDto>>(AgendaDxList));
                _logger.LogInformation("AgendaDx obtenidos: {Count}", AgendaDxList.Count());
                return await AgendaDxDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los AgendaDx");
                throw;
            }
        }

        public async Task<AgendaDxDto> GetAgendaDxByIdAsync(long medicalscheduledxid)
        {
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de AgendaDx por ID: {medicalscheduledxid}", medicalscheduledxid);
                if (medicalscheduledxid <= 0)
                {
                    throw new ArgumentException("El ID del AgendaDx debe ser mayor a 0", nameof(medicalscheduledxid));
                }

                var AgendaDx = await _agendaDxRepository.GetAgendaDxByIdAsync(medicalscheduledxid);
                if (AgendaDx == null)
                {
                    throw new KeyNotFoundException($"No se encontró el AgendaDx con ID: {medicalscheduledxid}");
                }
                var AgendaDxEntity = (AgendaDxEntity)AgendaDx;
                if (AgendaDxEntity.IsDeleted)
                {
                    throw new InvalidOperationException($"El AgendaDx con ID: {medicalscheduledxid} está eliminado lógicamente.");
                }
                var AgendaDxDto = _mapper.Map<AgendaDxDto>(AgendaDxEntity);
                _logger.LogInformation("AgendaDx obtenido con ID: {medicalscheduledxid}", medicalscheduledxid);
                return AgendaDxDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<IEnumerable<AgendaDxDto>> GetWhereAsync(string condicion)
        {
            AgendaDxDto[] AgendaDxDto;
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de AgendaDx con condición: {condicion}", condicion);
                var AgendaDx = _agendaDxRepository.GetAllAgendaDxAsync().Result;
                var AgendaDxList = AgendaDx.Where(e => !((AgendaDxEntity)e).IsDeleted).ToList();
                if (!string.IsNullOrWhiteSpace(condicion))
                {
                    AgendaDxList = AgendaDxList.Where(e =>
                    {
                        var entity = (AgendaDxEntity)e;
                        return (entity.Description != null && entity.Description.Contains(condicion, StringComparison.OrdinalIgnoreCase)) ;
                    }).ToList();
                }
                AgendaDxDto = _mapper.Map<AgendaDxDto[]>(AgendaDxList);
                _logger.LogInformation("AgendaDx obtenidos con condición: {Count}", AgendaDxDto.Length);
                return Task.FromResult<IEnumerable<AgendaDxDto>>(AgendaDxDto);
            }
            catch (Exception)
            {
                _logger.LogError("Error al obtener los AgendaDx con condición: {condicion}", condicion);
                throw;
            }
        }

        public async Task<AgendaDxDto> UpdateAgendaDxAsync(UpdateAgendaDx updateAgendaDxDto)
        {
            try
            {
                _logger.LogInformation("Iniciando actualización de AgendaDx con ID: {medicalscheduledxid}", updateAgendaDxDto.medicalscheduledxid);
                if (updateAgendaDxDto.medicalscheduledxid <= 0)
                {
                    throw new ArgumentException("El ID del AgendaDx debe ser mayor a 0", nameof(updateAgendaDxDto.medicalscheduledxid));
                }

                var AgendaDxExist = await _agendaDxRepository.GetAgendaDxByIdAsync(updateAgendaDxDto.medicalscheduledxid);
                if (AgendaDxExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el AgendaDx con ID: {updateAgendaDxDto.medicalscheduledxid}");
                }
                var AgendaDxExistente = (AgendaDxEntity)AgendaDxExist;
                if (AgendaDxExistente.IsDeleted)
                {
                    throw new InvalidOperationException($"No se puede actualizar el AgendaDx con ID: {updateAgendaDxDto.medicalscheduledxid} por que está eliminado lógicamente.");
                }
                var AgendaDxToUpdate = _mapper.Map<AgendaDxEntity>(updateAgendaDxDto);
                AgendaDxToUpdate.CreatedAt = AgendaDxExistente.CreatedAt;
                AgendaDxToUpdate.CreatedBy = AgendaDxExistente.CreatedBy;
                AgendaDxToUpdate.IsDeleted = false;
                AgendaDxToUpdate.UpdatedAt = DateTime.Now;


                var result = await _agendaDxRepository.UpdateAgendaDxAsync(AgendaDxToUpdate);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo actualizar el AgendaDx con ID: {updateAgendaDxDto.medicalscheduledxid}");
                }
                var AgendaDxActualizado = await _agendaDxRepository.GetAgendaDxByIdAsync(updateAgendaDxDto.medicalscheduledxid);
                if (AgendaDxActualizado == null)
                {
                    throw new Exception("Error al recuperar el AgendaDx recién actualizado.");
                }
                var estudioEntity2 = (AgendaDxEntity)AgendaDxActualizado;
                var AgendaDxDto = _mapper.Map<AgendaDxDto>(estudioEntity2);
                return AgendaDxDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el AgendaDx con ID: {medicalscheduledxid}", updateAgendaDxDto.medicalscheduledxid);
                throw;
            }
        }


        public async Task<IEnumerable<AgendaDxDto>> SearchAgendaDxsAsync(string? documentNumber)
        {
            try
            {
                _logger.LogInformation("Iniciando búsqueda de AgendaDxs con filtros");

                if (string.IsNullOrWhiteSpace(documentNumber))
                {
                    throw new ArgumentException("Debe proporcionar al menos un criterio de búsqueda");
                }

                var AgendaDxs = await _agendaDxRepository.SearchAgendaDxsAsync(documentNumber);
                var AgendaDxsDto = AgendaDxs.Select(p => _mapper.Map<AgendaDxDto>((AgendaDxEntity)p));

                _logger.LogInformation("Se encontraron {Count} AgendaDxs con los filtros especificados",
                    AgendaDxsDto.Count());

                return AgendaDxsDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar AgendaDxs con los filtros especificados");
                throw;
            }
        }

        public async Task<IEnumerable<AgendaDxDto>> SearchAgendaDxAsync(string? value1)
        {
            try
            {
                _logger.LogInformation("Iniciando búsqueda de AgendaDx con filtros");

                if (string.IsNullOrWhiteSpace(value1))
                {
                    throw new ArgumentException("Debe proporcionar al menos un criterio de búsqueda");
                }

                var results = await _agendaDxRepository.SearchAgendaDxsAsync(value1);
                var dtos = results.Select(p => _mapper.Map<AgendaDxDto>((AgendaDxEntity)p));

                _logger.LogInformation("Se encontraron {Count} AgendaDx con los filtros especificados", dtos.Count());

                return dtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar AgendaDx con los filtros especificados");
                throw;
            }
        }
    }
}
