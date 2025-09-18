using AutoMapper;
using Domain.DomainInterfaces;
using Infraestructure.Models;
using Infraestructure.Repositories;
using WebApi.Application.DTO.Preparacion;
using WebApi.Application.DTO.Personal;
using WebApi.Application.DTO.Preparacion;
using WebApi.Application.Services.Preparacion;

namespace WebApi.Application.Services.Preparacion
{
    public class PreparacionService : IPreparacionService
    {
        private readonly IPreparacionRepository _preparacionRepository;
        private readonly ILogger<PreparacionService> _logger;
        private readonly IMapper _mapper;
        public PreparacionService(
            ILogger<PreparacionService> logger,
            IPreparacionRepository preparacionRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _preparacionRepository = preparacionRepository ?? throw new ArgumentNullException(nameof(preparacionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PreparacionDto> CreatePreparacionAsync(CreatePreparacionDto createpreparacionDto)
        {
            try
            {
                _logger.LogInformation("Iniciando creación de preparacion");
                
                var preparacionEntity = _mapper.Map<PreparacionEntity>(createpreparacionDto);
                preparacionEntity.IsDeleted = false;
                preparacionEntity.CreatedAt = DateTime.UtcNow;
                var newpreparacionId = await _preparacionRepository.CreatePreparacionAsync(preparacionEntity);
                _logger.LogInformation("preparacion creado con ID: {preparacionId}", newpreparacionId);
                var newpreparacion = await _preparacionRepository.GetPreparacionByIdAsync(newpreparacionId);
                if (newpreparacion == null)
                {
                    throw new Exception("Error al recuperar el Preparacion recién creado.");
                }

                var newpreparacionDto = _mapper.Map<PreparacionEntity>(newpreparacion);
                var preparacionDto = _mapper.Map<PreparacionDto>(newpreparacion);
                return preparacionDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear preparacion");
                throw;
            }
        }

        public async Task<bool> DeletePreparacionAsync(long preparacionId, string eliminadoPor)
        {
            try
            {
                _logger.LogInformation("Iniciando eliminación lógica de Preparacion con ID: {preparacionId}", preparacionId);
                if (preparacionId <= 0)
                {
                    throw new ArgumentException("El ID del Preparacion debe ser mayor a 0", nameof(preparacionId));
                }
                var preparacionExist = await _preparacionRepository.GetPreparacionByIdAsync(preparacionId);
                var preparacionExistente = (PreparacionEntity)preparacionExist;
                if (preparacionExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Preparacion con ID: {preparacionId}");
                }
                if (preparacionExistente.IsDeleted)
                {
                    throw new InvalidOperationException($"No se puede eliminar el Preparacion con ID: {preparacionId} por que ya está eiminado lógicamente.");
                }
                preparacionExistente.IsDeleted = true;
                preparacionExistente.UpdatedAt = DateTime.UtcNow;
                preparacionExistente.UpdatedBy = eliminadoPor;

                var result = await _preparacionRepository.UpdatePreparacionAsync(preparacionExistente);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo eliminar el Preparacion con ID: {preparacionId}");
                }
                _logger.LogInformation("Preparacion con ID: {preparacionId} eliminado lógicamente", preparacionId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el Preparacion con ID: {preparacionId}", preparacionId);
                throw;
            }
        }

        public async Task<IEnumerable<PreparacionDto>> GetAllPreparacionAsync()
        {
            Task<IEnumerable<PreparacionDto>> preparacionDto;
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de todos los preparacion");
                var preparacion = _preparacionRepository.GetAllPreparacion().Result;
                var preparacionList = preparacion.Where(e => !((PreparacionEntity)e).IsDeleted).ToList();
                preparacionDto = Task.FromResult(_mapper.Map<IEnumerable<PreparacionDto>>(preparacionList));
                _logger.LogInformation("preparacion obtenidos: {Count}", preparacionList.Count());
                return await preparacionDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los preparacion");
                throw;
            }
        }

        public async Task<PreparacionDto> GetPreparacionByIdAsync(long preparacionId)
        {
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de Preparacion por ID: {preparacionId}", preparacionId);
                if (preparacionId <= 0)
                {
                    throw new ArgumentException("El ID del Preparacion debe ser mayor a 0", nameof(preparacionId));
                }

                var preparacion = await _preparacionRepository.GetPreparacionByIdAsync(preparacionId);
                if (preparacion == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Preparacion con ID: {preparacionId}");
                }
                var preparacionEntity = (PreparacionEntity)preparacion;
                if (preparacionEntity.IsDeleted)
                {
                    throw new InvalidOperationException($"El Preparacion con ID: {preparacionId} está eliminado lógicamente.");
                }
                var preparacionDto = _mapper.Map<PreparacionDto>(preparacionEntity);
                _logger.LogInformation("Preparacion obtenido con ID: {preparacionId}", preparacionId);
                return preparacionDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<IEnumerable<PreparacionDto>> GetWhereAsync(string condicion)
        {
            PreparacionDto[] preparacionDto;
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de preparacion con condición: {condicion}", condicion);
                var preparacion = _preparacionRepository.GetAllPreparacion().Result;
                var preparacionList = preparacion.Where(e => !((PreparacionEntity)e).IsDeleted).ToList();
                if (!string.IsNullOrWhiteSpace(condicion))
                {
                    preparacionList = preparacionList.Where(e =>
                    {
                        var entity = (PreparacionEntity)e;
                        return (entity.Description != null && entity.Description.Contains(condicion, StringComparison.OrdinalIgnoreCase));
                    }).ToList();
                }
                preparacionDto = _mapper.Map<PreparacionDto[]>(preparacionList);
                _logger.LogInformation("preparacion obtenidos con condición: {Count}", preparacionDto.Length);
                return Task.FromResult<IEnumerable<PreparacionDto>>(preparacionDto);
            }
            catch (Exception)
            {
                _logger.LogError("Error al obtener los preparacion con condición: {condicion}", condicion);
                throw;
            }
        }

        public async Task<PreparacionDto> UpdatePreparacionAsync(UpdatePreparacionDto updatePreparacionDto)
        {
            try
            {
                _logger.LogInformation("Iniciando actualización de Preparacion con ID: {preparacionId}", updatePreparacionDto.preparationid);
                if (updatePreparacionDto.preparationid <= 0)
                {
                    throw new ArgumentException("El ID del Preparacion debe ser mayor a 0", nameof(updatePreparacionDto.preparationid));
                }
                
                var preparacionExist = await _preparacionRepository.GetPreparacionByIdAsync(updatePreparacionDto.preparationid);
                if (preparacionExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Preparacion con ID: {updatePreparacionDto.preparationid}");
                }
                var preparacionExistente = (PreparacionEntity)preparacionExist;
                if (preparacionExistente.IsDeleted)
                {
                    throw new InvalidOperationException($"No se puede actualizar el Preparacion con ID: {updatePreparacionDto.preparationid} por que está eliminado lógicamente.");
                }
                var preparacionToUpdate = _mapper.Map<PreparacionEntity>(updatePreparacionDto);
                preparacionToUpdate.CreatedAt = preparacionExistente.CreatedAt;
                preparacionToUpdate.CreatedBy = preparacionExistente.CreatedBy;
                preparacionToUpdate.IsDeleted = false;
                var result = await _preparacionRepository.UpdatePreparacionAsync(preparacionToUpdate);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo actualizar el Preparacion con ID: {updatePreparacionDto.preparationid}");
                }
                var preparacionActualizado = await _preparacionRepository.GetPreparacionByIdAsync(updatePreparacionDto.preparationid);
                if (preparacionActualizado == null)
                {
                    throw new Exception("Error al recuperar el Preparacion recién actualizado.");
                }
                var PreparacionEntity2 = (PreparacionEntity)preparacionActualizado;
                var preparacionDto = _mapper.Map<PreparacionDto>(PreparacionEntity2);
                return preparacionDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el Preparacion con ID: {preparacionId}", updatePreparacionDto.preparationid);
                throw;
            }
        }
    }
}
