using AutoMapper;
using Domain.DomainInterfaces;
using Infraestructure.Models;
using WebApi.Application.DTO.Plantilla;
using WebApi.Application.Services.Plantilla;

namespace WebApi.Application.Services.Plantilla
{
    public class PlantillaService : IPlantillaService
    {
        private readonly ILogger<PlantillaService> _logger;
        private readonly IPlantillaRepository _PlantillaRepository;
        private readonly IEstudiosRepository _estudiosRepository;
        private readonly IMapper _mapper;
        public PlantillaService(
            ILogger<PlantillaService> logger,
            IPlantillaRepository PlantillaRepository,
            IEstudiosRepository estudiosRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _PlantillaRepository = PlantillaRepository ?? throw new ArgumentNullException(nameof(PlantillaRepository));
            _estudiosRepository = estudiosRepository ?? throw new ArgumentNullException(nameof(estudiosRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PlantillaDto> CreatePlantillaAsync(CreatePlantillaDto createPlantillaDto)
        {
            try
            {
                _logger.LogInformation("Iniciando creación de Plantilla");
                // Validar que el ExamsId existe
                if (createPlantillaDto.ExamsId <= 0)
                {
                    throw new ArgumentException("El ExamsId debe ser mayor a 0", nameof(createPlantillaDto.ExamsId));
                }

                var estudioExiste = await _estudiosRepository.GetEstudiosByIdAsync(createPlantillaDto.ExamsId);
                if (estudioExiste == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Estudio con ID: {createPlantillaDto.ExamsId}. No se puede crear el personal.");
                }

                var estudioEntity = (EstudiosEntity)estudioExiste;

                if (estudioEntity.IsDeleted)
                {
                    throw new InvalidOperationException($"El Estudio con ID: {createPlantillaDto.ExamsId} está eliminado lógicamente. No se puede crear el personal.");
                }

                var PlantillaEntity = _mapper.Map<PlantillaEntity>(createPlantillaDto);
                PlantillaEntity.IsDeleted = false;
                PlantillaEntity.CreatedAt = DateTime.UtcNow;
                var newPlantillaId = await _PlantillaRepository.CreatePlantillaAsync(PlantillaEntity);
                _logger.LogInformation("Plantilla creado con ID: {PlantillaId}", newPlantillaId);
                var newPlantilla = await _PlantillaRepository.GetPlantillaByIdAsync(newPlantillaId);
                if (newPlantilla == null)
                {
                    throw new Exception("Error al recuperar el Plantilla recién creado.");
                }

                var newPlantillaDto = _mapper.Map<PlantillaEntity>(newPlantilla);
                var PlantillaDto = _mapper.Map<PlantillaDto>(newPlantilla);
                return PlantillaDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear Plantilla");
                throw;
            }
        }

        public async Task<bool> DeletePlantillaAsync(long PlantillaId, string eliminadoPor)
        {
            try
            {
                _logger.LogInformation("Iniciando eliminación lógica de Plantilla con ID: {PlantillaId}", PlantillaId);
                if (PlantillaId <= 0)
                {
                    throw new ArgumentException("El ID del Plantilla debe ser mayor a 0", nameof(PlantillaId));
                }
                var PlantillaExist = await _PlantillaRepository.GetPlantillaByIdAsync(PlantillaId);
                var PlantillaExistente = (PlantillaEntity)PlantillaExist;
                if (PlantillaExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Plantilla con ID: {PlantillaId}");
                }
                if (PlantillaExistente.IsDeleted)
                {
                    throw new InvalidOperationException($"No se puede eliminar el Plantilla con ID: {PlantillaId} por que ya está eiminado lógicamente.");
                }
                PlantillaExistente.IsDeleted = true;
                PlantillaExistente.UpdatedAt = DateTime.UtcNow;
                PlantillaExistente.UpdatedBy = eliminadoPor;

                var result = await _PlantillaRepository.UpdatePlantillaAsync(PlantillaExistente);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo eliminar el Plantilla con ID: {PlantillaId}");
                }
                _logger.LogInformation("Estudio con ID: {PlantillaId} eliminado lógicamente", PlantillaId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el Plantilla con ID: {PlantillaId}", PlantillaId);
                throw;
            }
        }

        public async Task<IEnumerable<PlantillaDto>> GetAllPlantillaAsync()
        {
            Task<IEnumerable<PlantillaDto>> PlantillaDto;
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de todos los Plantilla");
                var Plantilla = _PlantillaRepository.GetAllPlantillaAsync().Result;
                var PlantillaList = Plantilla.Where(e => !((PlantillaEntity)e).IsDeleted).ToList();
                PlantillaDto = Task.FromResult(_mapper.Map<IEnumerable<PlantillaDto>>(PlantillaList));
                _logger.LogInformation("Plantilla obtenidos: {Count}", PlantillaList.Count());
                return await PlantillaDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los Plantilla");
                throw;
            }
        }

        public async Task<PlantillaDto> GetPlantillaByIdAsync(long PlantillaId)
        {
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de Estudio por ID: {PlantillaId}", PlantillaId);
                if (PlantillaId <= 0)
                {
                    throw new ArgumentException("El ID del Plantilla debe ser mayor a 0", nameof(PlantillaId));
                }

                var Plantilla = await _PlantillaRepository.GetPlantillaByIdAsync(PlantillaId);
                if (Plantilla == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Plantilla con ID: {PlantillaId}");
                }
                var PlantillaEntity = (PlantillaEntity)Plantilla;
                if (PlantillaEntity.IsDeleted)
                {
                    throw new InvalidOperationException($"El Plantilla con ID: {PlantillaId} está eliminado lógicamente.");
                }
                var PlantillaDto = _mapper.Map<PlantillaDto>(PlantillaEntity);
                _logger.LogInformation("Estudio obtenido con ID: {PlantillaId}", PlantillaId);
                return PlantillaDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<IEnumerable<PlantillaDto>> GetWhereAsync(string condicion)
        {
            PlantillaDto[] PlantillaDto;
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de Plantilla con condición: {condicion}", condicion);
                var Plantilla = _PlantillaRepository.GetAllPlantillaAsync().Result;
                var PlantillaList = Plantilla.Where(e => !((PlantillaEntity)e).IsDeleted).ToList();
                if (!string.IsNullOrWhiteSpace(condicion))
                {
                    PlantillaList = PlantillaList.Where(e =>
                    {
                        var entity = (PlantillaEntity)e;
                        return (entity.Name != null && entity.Name.Contains(condicion, StringComparison.OrdinalIgnoreCase)) ||
                               (entity.Description != null && entity.Description.Contains(condicion, StringComparison.OrdinalIgnoreCase));
                    }).ToList();
                }
                PlantillaDto = _mapper.Map<PlantillaDto[]>(PlantillaList);
                _logger.LogInformation("Plantilla obtenidos con condición: {Count}", PlantillaDto.Length);
                return Task.FromResult<IEnumerable<PlantillaDto>>(PlantillaDto);
            }
            catch (Exception)
            {
                _logger.LogError("Error al obtener los Plantilla con condición: {condicion}", condicion);
                throw;
            }
        }

        public async Task<PlantillaDto> UpdatePlantillaAsync(UpdatePlantillaDto updatePlantillaDto)
        {
            try
            {
                _logger.LogInformation("Iniciando actualización de Estudio con ID: {PlantillaId}", updatePlantillaDto.templatesid);
                if (updatePlantillaDto.templatesid <= 0)
                {
                    throw new ArgumentException("El ID del Plantilla debe ser mayor a 0", nameof(updatePlantillaDto.templatesid));
                }
                // Validar que el ExamsId existe
                if (updatePlantillaDto.ExamsId <= 0)
                {
                    throw new ArgumentException("El ExamsId debe ser mayor a 0", nameof(updatePlantillaDto.ExamsId));
                }

                var estudioExiste = await _estudiosRepository.GetEstudiosByIdAsync(updatePlantillaDto.ExamsId);
                if (estudioExiste == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Estudio con ID: {updatePlantillaDto.ExamsId}. No se puede actualizar el personal.");
                }

                var estudioEntity = (EstudiosEntity)estudioExiste;
                if (estudioEntity.IsDeleted)
                {
                    throw new InvalidOperationException($"El Estudio con ID: {updatePlantillaDto.ExamsId} está eliminado lógicamente. No se puede actualizar el personal.");
                }

                var PlantillaExist = await _PlantillaRepository.GetPlantillaByIdAsync(updatePlantillaDto.templatesid);
                if (PlantillaExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Estudio con ID: {updatePlantillaDto.templatesid}");
                }
                var PlantillaExistente = (PlantillaEntity)PlantillaExist;
                if (PlantillaExistente.IsDeleted)
                {
                    throw new InvalidOperationException($"No se puede actualizar el Estudio con ID: {updatePlantillaDto.templatesid} por que está eliminado lógicamente.");
                }
                var PlantillaToUpdate = _mapper.Map<PlantillaEntity>(updatePlantillaDto);
                PlantillaToUpdate.CreatedAt = PlantillaExistente.CreatedAt;
                PlantillaToUpdate.CreatedBy = PlantillaExistente.CreatedBy;
                PlantillaToUpdate.IsDeleted = false;
                var result = await _PlantillaRepository.UpdatePlantillaAsync(PlantillaToUpdate);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo actualizar el Estudio con ID: {updatePlantillaDto.templatesid}");
                }
                var PlantillaActualizado = await _PlantillaRepository.GetPlantillaByIdAsync(updatePlantillaDto.templatesid);
                if (PlantillaActualizado == null)
                {
                    throw new Exception("Error al recuperar el Plantilla recién actualizado.");
                }
                var estudioEntity2 = (PlantillaEntity)PlantillaActualizado;
                var PlantillaDto = _mapper.Map<PlantillaDto>(estudioEntity2);
                return PlantillaDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el Estudio con ID: {PlantillaId}", updatePlantillaDto.templatesid);
                throw;
            }
        }
    }
}
