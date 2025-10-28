using AutoMapper;
using Domain.DomainInterfaces;
using Infraestructure.Models;
using WebApi.Application.DTO.Recursos;
using WebApi.Application.DTO.Salas;

namespace WebApi.Application.Services.Recursos
{
    public class RecursosService : IRecursosService
    {
        private readonly ILogger<RecursosService> _logger;
        private readonly IRecursosRepository _recursosRepository;
        private readonly IMapper _mapper;
        private readonly ICentroRepository _centroRepository;
        public RecursosService(ILogger<RecursosService> logger, IRecursosRepository recursosRepository, IMapper mapper, ICentroRepository centroRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _recursosRepository = recursosRepository ?? throw new ArgumentNullException(nameof(recursosRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _centroRepository = centroRepository ?? throw new ArgumentNullException(nameof(centroRepository));
        }   

        public async Task<RecursosDto> CreateRecursosAsync(CreateRecursosDto createRecursosDto)
        {
            try
            {
                _logger.LogInformation("Iniciando creación de Recursos.");
                if (createRecursosDto.CentroId <= 0)
                {
                    throw new ArgumentException("El CentroId debe ser mayor a 0", nameof(createRecursosDto.CentroId));
                }

                var centroExiste = await _centroRepository.GetCentroByIdAsync(createRecursosDto.CentroId);
                if (centroExiste == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Centro con ID: {createRecursosDto.CentroId}. No se puede crear el personal.");
                }

                var centroEntity = (CentroEntity)centroExiste;

                if (centroEntity.IsDeleted)
                {
                    throw new InvalidOperationException($"El Centro con ID: {createRecursosDto.CentroId} está eliminado lógicamente. No se puede crear el personal.");
                }

                var recursosEntity = _mapper.Map<RecursosEntity>(createRecursosDto);    
                recursosEntity.IsDeleted = false; // Asegurar que el nuevo recurso no esté marcado como eliminado
                recursosEntity.CreatedAt = DateTime.Now; // Establecer la fecha de creación

                var newId = await _recursosRepository.CreateRecursosAsync(recursosEntity);
                _logger.LogInformation("Recurso creado con ID: {NewId}", newId);
                var newRecursos = await _recursosRepository.GetRecursosByIdAsync(newId);
                if (newRecursos == null)
                {
                    throw new Exception("Error al recuperar el recurso recién creado.");
                }
                var recursosEntityResult = (RecursosEntity)newRecursos;
                var recursosDtoResult = _mapper.Map<RecursosDto>(recursosEntityResult);

                return recursosDtoResult;

            }
            catch (Exception)
            {
                _logger.LogError("Error al crear el recurso.");
                throw;
            }
        }

        public async Task<bool> DeleteRecursosAsync(long recursosId, string eliminadoPor)
        {
            try
            {
                _logger.LogInformation("Iniciando eliminación lógica de Recurso con ID: {recursosId}", recursosId);
                if (recursosId <= 0)
                {
                    throw new ArgumentException("El ID del recurso debe ser mayor a 0", nameof(recursosId));
                }

                var recursosExiste = await _recursosRepository.GetRecursosByIdAsync(recursosId);

                var recursosExistentes = (RecursosEntity)recursosExiste;
                if (recursosExiste == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Recurso con ID: {recursosId}. No se puede eliminar.");
                }

                if (recursosExistentes.IsDeleted)
                {
                    throw new InvalidOperationException($"El Recurso con ID: {recursosId} ya está eliminada lógicamente.");
                }

                recursosExistentes.IsDeleted = true;
                recursosExistentes.UpdatedAt = DateTime.Now;
                recursosExistentes.UpdatedBy = eliminadoPor;

                var result = await _recursosRepository.UpdateRecursosAsync(recursosExistentes);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo eliminar Recurso con ID: {recursosId}");
                }

                _logger.LogInformation("Recurso con ID: {recursosId} eliminado lógicamente por {eliminadoPor}", recursosId, eliminadoPor);
                
                return result;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<RecursosDto>> GetAllRecursosAsync()
        {
            Task<IEnumerable<RecursosDto>> recursosDtos;
            try
            {
                _logger.LogInformation("Iniciando obtención de todos los Recursos.");
                var recursos = await _recursosRepository.GetAllRecursosAsync();
                var recursosList = recursos.Where(s => !((RecursosEntity)s).IsDeleted).ToList();
                recursosDtos = Task.FromResult(_mapper.Map<IEnumerable<RecursosDto>>(recursosList));
                _logger.LogInformation("Recursos obtenidos: {Count}", recursosList.Count);
                return await recursosDtos;
            }
            catch (Exception)
            {
                _logger.LogError("Error al obtener los Recursos.");
                throw;
            }
        }

        public async Task<RecursosDto> GetRecursosByIdAsync(long recursosId)
        {
            try
            {
                _logger.LogInformation("Iniciando obtención de Recursos por ID: {recursosId}", recursosId);
                if (recursosId <= 0)
                {
                    throw new ArgumentException("El ID del recurso debe ser mayor a 0", nameof(recursosId));
                }

                var recursos = await _recursosRepository.GetRecursosByIdAsync(recursosId);

                if (recursos == null || ((RecursosEntity)recursos).IsDeleted)
                {
                    throw new KeyNotFoundException($"No se encontró el recurso con ID: {recursosId} o está eliminada lógicamente.");
                }

                var recursoDto = _mapper.Map<RecursosDto>(recursos);
                _logger.LogInformation("Recurso recuperado con ID: {recursosId}", recursosId);
                return recursoDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al recuperar los recursos.");
                throw;
            }
        }

        public Task<IEnumerable<RecursosDto>> GetWhereAsync(string condicion)
        {
            RecursosDto[] recursosDtos;
            try
            {
                _logger.LogInformation("Iniciando obtención de recursos con condición: {condicion}", condicion);
                var recursos = _recursosRepository.GetAllRecursosAsync().Result;
                var recursosList = recursos.Where(s => !((RecursosEntity)s).IsDeleted).ToList();
                if (!string.IsNullOrWhiteSpace(condicion))
                {
                    recursosList = recursosList.Where(s =>
                    {
                        var entity = (RecursosEntity)s;
                        return (entity.Name != null && entity.Name.Contains(condicion, StringComparison.OrdinalIgnoreCase)) ||
                        (entity.Description != null && entity.Description.Contains(condicion,StringComparison.OrdinalIgnoreCase));
                    }).ToList();
                }
                recursosDtos = _mapper.Map<RecursosDto[]>(recursosList);
                _logger.LogInformation("Recursos recuperados con condición: {Count}", recursosDtos.Length);
                return Task.FromResult((IEnumerable<RecursosDto>)recursosDtos);
            }
            catch (Exception)
            {
                _logger.LogError("Error al recuperar los recursos con condición: {Condicion}", condicion);
                throw;
            }
        }

        public async Task<RecursosDto> UpdateRecursosAsync(UpdateRecursosDto updateRecursosDto)
        {
            try
            {
                _logger.LogInformation("Iniciando actualizacón de Sala con ID: {SalasId}", updateRecursosDto.resourcesid);
                if (updateRecursosDto.resourcesid <= 0)
                {
                    throw new ArgumentException("El ID del recurso debe ser mayor a 0.", nameof(updateRecursosDto.resourcesid));
                }
                // Validar que el CentroId existe
                if (updateRecursosDto.CentroId <= 0)
                {
                    throw new ArgumentException("El CentroId debe ser mayor a 0", nameof(updateRecursosDto.CentroId));
                }

                var centroExiste = await _centroRepository.GetCentroByIdAsync(updateRecursosDto.CentroId);
                if (centroExiste == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Centro con ID: {updateRecursosDto.CentroId}. No se puede actualizar el personal.");
                }

                var centroEntity = (CentroEntity)centroExiste;
                if (centroEntity.IsDeleted)
                {
                    throw new InvalidOperationException($"El Centro con ID: {updateRecursosDto.CentroId} está eliminado lógicamente. No se puede actualizar el personal.");
                }

                var recursoExist = await _recursosRepository.GetRecursosByIdAsync(updateRecursosDto.resourcesid);
                if (recursoExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró la sala con ID: {updateRecursosDto.resourcesid}. No se puede Actualizar ");
                }

                var recursoExistente = (RecursosEntity)recursoExist;
                if (recursoExistente.IsDeleted)
                {
                    throw new InvalidOperationException($"El recurso con ID: {updateRecursosDto.resourcesid} está eliminada lógicamente. No se puede actualizar.");
                }

                var recursoToUpdate = _mapper.Map<RecursosEntity>(updateRecursosDto);
                recursoToUpdate.CreatedAt = recursoExistente.CreatedAt;
                recursoToUpdate.CreatedBy = recursoExistente.CreatedBy;
                recursoToUpdate.IsDeleted = false;
                recursoToUpdate.UpdatedAt = DateTime.Now;

                var result = await _recursosRepository.UpdateRecursosAsync(recursoToUpdate);

                if(!result)
                {
                    throw new InvalidOperationException($"No se pudo actualizar el recurso con ID: {updateRecursosDto.resourcesid}.");
                }

                var updateRecurso = await _recursosRepository.GetRecursosByIdAsync(updateRecursosDto.resourcesid);
                if (updateRecurso == null)
                {
                    throw new Exception("Error al recuperar el recurso recién actualizado.");
                }

                var recursoEntity = (RecursosEntity)updateRecurso;
                var recursoDto = _mapper.Map<RecursosDto>(recursoEntity);
                return recursoDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el recurso con ID: {SalasId}", updateRecursosDto.resourcesid);
                throw;
            }
        }
    }
}
