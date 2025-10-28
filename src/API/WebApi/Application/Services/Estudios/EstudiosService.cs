using AutoMapper;
using Domain.DomainInterfaces;
using Infraestructure.Models;
using WebApi.Application.DTO.Estudios;
using WebApi.Application.DTO.Personal;

namespace WebApi.Application.Services.Estudios
{
    public class EstudiosService : IEstudiosService
    {
        private readonly ILogger<EstudiosService> _logger;
        private readonly IEstudiosRepository _estudiosRepository;
        private readonly ICentroRepository _centroRepository;
        private readonly IMapper _mapper;
        public EstudiosService(
            ILogger<EstudiosService> logger,
            IEstudiosRepository estudiosRepository,
            ICentroRepository centroRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _estudiosRepository = estudiosRepository ?? throw new ArgumentNullException(nameof(estudiosRepository));
            _centroRepository = centroRepository ?? throw new ArgumentNullException(nameof(centroRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<EstudiosDto> CreateEstudiosAsync(CreateEstudiosDto createEstudiosDto)
        {
            try
            {
                _logger.LogInformation("Iniciando creación de estudios");
                // Validar que el CentroId existe
                if (createEstudiosDto.CentroId <= 0)
                {
                    throw new ArgumentException("El CentroId debe ser mayor a 0", nameof(createEstudiosDto.CentroId));
                }

                var centroExiste = await _centroRepository.GetCentroByIdAsync(createEstudiosDto.CentroId);
                if (centroExiste == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Centro con ID: {createEstudiosDto.CentroId}. No se puede crear el personal.");
                }

                var centroEntity = (CentroEntity)centroExiste;

                if (centroEntity.IsDeleted)
                {
                    throw new InvalidOperationException($"El Centro con ID: {createEstudiosDto.CentroId} está eliminado lógicamente. No se puede crear el personal.");
                }

                var estudiosEntity = _mapper.Map<EstudiosEntity>(createEstudiosDto);
                estudiosEntity.IsDeleted = false;
                estudiosEntity.CreatedAt = DateTime.Now;
                var newEstudiosId = await _estudiosRepository.CreateEstudiosAsync(estudiosEntity);
                _logger.LogInformation("Estudios creado con ID: {EstudiosId}", newEstudiosId);
                var newEstudios = await _estudiosRepository.GetEstudiosByIdAsync(newEstudiosId);
                if (newEstudios == null)
                {
                    throw new Exception("Error al recuperar el estudio recién creado.");
                }

                var newEstudiosDto = _mapper.Map<EstudiosEntity>(newEstudios);
                var estudiosDto = _mapper.Map<EstudiosDto>(newEstudios);
                return estudiosDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear estudios");
                throw;
            }
        }

        public async Task<bool> DeleteEstudiosAsync(long estudiosId, string eliminadoPor)
        {
            try
            {
                _logger.LogInformation("Iniciando eliminación lógica de Estudio con ID: {estudiosId}", estudiosId);
                if (estudiosId <= 0)
                {
                    throw new ArgumentException("El ID del estudio debe ser mayor a 0", nameof(estudiosId));
                }
                var estudiosExist = await _estudiosRepository.GetEstudiosByIdAsync(estudiosId);
                var estudiosExistente = (EstudiosEntity)estudiosExist;
                if (estudiosExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el estudio con ID: {estudiosId}");
                }
                if (estudiosExistente.IsDeleted)
                {
                    throw new InvalidOperationException($"No se puede eliminar el Estudio con ID: {estudiosId} por que ya está eiminado lógicamente.");
                }
                estudiosExistente.IsDeleted = true;
                estudiosExistente.UpdatedAt = DateTime.Now;
                estudiosExistente.UpdatedBy = eliminadoPor;

                var result = await _estudiosRepository.UpdateEstudiosAsync(estudiosExistente);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo eliminar el Estudio con ID: {estudiosId}");
                }
                _logger.LogInformation("Estudio con ID: {estudiosId} eliminado lógicamente", estudiosId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el Estudio con ID: {estudiosId}", estudiosId);
                throw;
            }
        }

        public async Task<IEnumerable<EstudiosDto>> GetAllEstudiosAsync()
        {
            Task<IEnumerable<EstudiosDto>> estudiosDto;
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de todos los estudios");
                var estudios = _estudiosRepository.GetAllEstudiosAsync().Result;
                var estudiosList = estudios.Where(e => !((EstudiosEntity)e).IsDeleted).ToList();
                estudiosDto = Task.FromResult(_mapper.Map<IEnumerable<EstudiosDto>>(estudiosList));
                _logger.LogInformation("Estudios obtenidos: {Count}", estudiosList.Count());
                return await estudiosDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los estudios");
                throw;
            }
        }

        public async Task<EstudiosDto> GetEstudiosByIdAsync(long estudiosId)
        {
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de Estudio por ID: {estudiosId}", estudiosId);
                if (estudiosId <= 0)
                {
                    throw new ArgumentException("El ID del estudio debe ser mayor a 0", nameof(estudiosId));
                }

                var estudios = await _estudiosRepository.GetEstudiosByIdAsync(estudiosId);
                if (estudios == null)
                {
                    throw new KeyNotFoundException($"No se encontró el estudio con ID: {estudiosId}");
                }
                var estudiosEntity = (EstudiosEntity)estudios;
                if (estudiosEntity.IsDeleted)
                {
                    throw new InvalidOperationException($"El estudio con ID: {estudiosId} está eliminado lógicamente.");
                }
                var estudiosDto = _mapper.Map<EstudiosDto>(estudiosEntity);
                _logger.LogInformation("Estudio obtenido con ID: {estudiosId}", estudiosId);
                return estudiosDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<IEnumerable<EstudiosDto>> GetWhereAsync(string condicion)
        {
            EstudiosDto[] estudiosDto;
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de estudios con condición: {condicion}", condicion);
                var estudios =  _estudiosRepository.GetAllEstudiosAsync().Result;
                var estudiosList = estudios.Where(e => !((EstudiosEntity)e).IsDeleted).ToList();
                if (!string.IsNullOrWhiteSpace(condicion))
                {
                    estudiosList = estudiosList.Where(e =>
                    {
                        var entity = (EstudiosEntity)e;
                        return (entity.Name != null && entity.Name.Contains(condicion, StringComparison.OrdinalIgnoreCase)) ||
                               (entity.Description != null && entity.Description.Contains(condicion, StringComparison.OrdinalIgnoreCase));
                    }).ToList();
                }
                estudiosDto = _mapper.Map<EstudiosDto[]>(estudiosList);
                _logger.LogInformation("Estudios obtenidos con condición: {Count}", estudiosDto.Length);
                return Task.FromResult<IEnumerable<EstudiosDto>>(estudiosDto);
            }
            catch (Exception)
            {
                _logger.LogError("Error al obtener los estudios con condición: {condicion}", condicion);
                throw;
            }
        }

        public async Task<EstudiosDto> UpdateEstudiosAsync(UpdateEstudiosDto updateEstudiosDto)
        {
            try
            {
                _logger.LogInformation("Iniciando actualización de Estudio con ID: {estudiosId}", updateEstudiosDto.studiesid);
                if (updateEstudiosDto.studiesid <= 0)
                {
                    throw new ArgumentException("El ID del estudio debe ser mayor a 0", nameof(updateEstudiosDto.studiesid));
                }
                // Validar que el CentroId existe
                if (updateEstudiosDto.CentroId <= 0)
                {
                    throw new ArgumentException("El CentroId debe ser mayor a 0", nameof(updateEstudiosDto.CentroId));
                }

                var centroExiste = await _centroRepository.GetCentroByIdAsync(updateEstudiosDto.CentroId);
                if (centroExiste == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Centro con ID: {updateEstudiosDto.CentroId}. No se puede actualizar el personal.");
                }

                var centroEntity = (CentroEntity)centroExiste;
                if (centroEntity.IsDeleted)
                {
                    throw new InvalidOperationException($"El Centro con ID: {updateEstudiosDto.CentroId} está eliminado lógicamente. No se puede actualizar el personal.");
                }

                var estudiosExist = await _estudiosRepository.GetEstudiosByIdAsync(updateEstudiosDto.studiesid);
                if (estudiosExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Estudio con ID: {updateEstudiosDto.studiesid}");
                }
                var estudiosExistente = (EstudiosEntity)estudiosExist;
                if (estudiosExistente.IsDeleted)
                {
                    throw new InvalidOperationException($"No se puede actualizar el Estudio con ID: {updateEstudiosDto.studiesid} por que está eliminado lógicamente.");
                }
                var estudiosToUpdate = _mapper.Map<EstudiosEntity>(updateEstudiosDto);
                estudiosToUpdate.CreatedAt = estudiosExistente.CreatedAt;
                estudiosToUpdate.CreatedBy = estudiosExistente.CreatedBy;
                estudiosToUpdate.IsDeleted = false;
                estudiosToUpdate.UpdatedAt = DateTime.Now;


                var result = await _estudiosRepository.UpdateEstudiosAsync(estudiosToUpdate);   
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo actualizar el Estudio con ID: {updateEstudiosDto.studiesid}");
                }
                var estudiosActualizado = await _estudiosRepository.GetEstudiosByIdAsync(updateEstudiosDto.studiesid);
                if (estudiosActualizado == null)
                {
                    throw new Exception("Error al recuperar el estudio recién actualizado.");
                }
                var estudioEntity2 = (EstudiosEntity)estudiosActualizado;
                var estudiosDto = _mapper.Map<EstudiosDto>(estudioEntity2);
                return estudiosDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el Estudio con ID: {estudiosId}", updateEstudiosDto.studiesid);
                throw;
            }
        }
    }
}
