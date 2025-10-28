using AutoMapper;
using Domain.DomainInterfaces;
using Infraestructure.Models;
using Infraestructure.Persistence;
using Infraestructure.Repositories;
using WebApi.Application.DTO.Estudios;
using WebApi.Application.DTO.Examenes;
using WebApi.Application.DTO.Salas;
using WebApi.Application.Services.Salas;

namespace WebApi.Application.Services.Examenes
{
    public class ExamenesService : IExamenesService
    {
        private readonly ILogger<ExamenesService> _logger;
        private readonly IExamenesRepository _examenesRepository;
        private readonly IMapper _mapper;
        public ExamenesService(ILogger<ExamenesService> logger,
            IExamenesRepository examenesRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _examenesRepository = examenesRepository ?? throw new ArgumentNullException(nameof(examenesRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        public async Task<ExamenesDto> CreateSalasAsync(CreateExamenesDto createExamenesDto)
        {
            try
            {
                _logger.LogInformation("Iniciando creación de examenes.");

                var examenesEntity = _mapper.Map<ExamenesEntity>(createExamenesDto);
                examenesEntity.IsDeleted = false; // Asegurarse de que IsDeleted esté en false al crear
                examenesEntity.CreatedAt = DateTime.Now; // Establecer la fecha de creación
                var newExamenId = await _examenesRepository.CreateExamenesAsync(examenesEntity);
                _logger.LogInformation("Examen creado con ID: {newExamenId}", newExamenId);
                var newExamenes = await _examenesRepository.GetExamenesByIdAsync(newExamenId);
                if (newExamenes == null)
                {
                    throw new Exception("Error al recuperar el examen recién creado.");
                }

                var examenesEntity2 = (ExamenesEntity)newExamenes;
                var newExamenesDto = _mapper.Map<ExamenesDto>(examenesEntity2);

                return newExamenesDto;
            }
            catch (Exception)
            {
                _logger.LogError("Error al crear el examen.");
                throw;
            }
        }

        public async Task<bool> DeleteExamenesAsync(long id, string eliminadoPor)
        {
            try
            {
                _logger.LogInformation("Iniciando eliminación lógica de Examen con ID: {newExamenId}", id);
                if (id <= 0)
                {
                    throw new ArgumentException("El ID del examen debe ser mayor a 0", nameof(id));
                }
                var examenesExist = await _examenesRepository.GetExamenesByIdAsync(id);
                var examenesExistente = (ExamenesEntity)examenesExist;
                if (examenesExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el examen con ID: {id}");
                }
                if (examenesExistente.IsDeleted)
                {
                    throw new InvalidOperationException($"No se puede eliminar el Examen con ID: {id} por que ya está eiminado lógicamente.");
                }
                examenesExistente.IsDeleted = true;
                examenesExistente.UpdatedAt = DateTime.Now;
                examenesExistente.UpdatedBy = eliminadoPor;

                var result = await _examenesRepository.UpdateExamenesAsync(examenesExistente);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo eliminar el Examen con ID: {id}");
                }
                _logger.LogInformation("Examen con ID: {id} eliminado lógicamente", id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el Examen con ID: {id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<ExamenesDto>> GetAllExamenesAsync()
        {
            Task<IEnumerable<ExamenesDto>> examenesDto;
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de todos los examenes");
                var examenes = _examenesRepository.GetAllExamenesAsync().Result;
                var examenesList = examenes.Where(e => !((ExamenesEntity)e).IsDeleted).ToList();
                examenesDto = Task.FromResult(_mapper.Map<IEnumerable<ExamenesDto>>(examenesList));
                _logger.LogInformation("Estudios obtenidos: {Count}", examenesList.Count());
                return await examenesDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los examenes.");
                throw;
            }
        }

        public async Task<ExamenesDto> GetExamenesByIdAsync(long id)
        {
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de Examen por ID: {estudiosId}", id);
                if (id <= 0)
                {
                    throw new ArgumentException("El ID del examen debe ser mayor a 0", nameof(id));
                }

                var examenes = await _examenesRepository.GetExamenesByIdAsync(id);
                if (examenes == null)
                {
                    throw new KeyNotFoundException($"No se encontró el examen con ID: {id}");
                }
                var examenesEntity = (ExamenesEntity)examenes;
                if (examenesEntity.IsDeleted)
                {
                    throw new InvalidOperationException($"El examen con ID: {id} está eliminado lógicamente.");
                }
                var examenesDto = _mapper.Map<ExamenesDto>(examenesEntity);
                _logger.LogInformation("Examen obtenido con ID: {id}", id);
                return examenesDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<IEnumerable<ExamenesDto>> GetWhereAsync(string condicion)
        {
            ExamenesDto[] examenesDto;
            try
            {
                _logger.LogInformation("Iniciando obtención de examenes con condición: {condicion}", condicion);
                var examenes = _examenesRepository.GetAllExamenesAsync().Result;
                var examenesList = examenes.Where(e => !((ExamenesEntity)e).IsDeleted).ToList();
                if (!string.IsNullOrWhiteSpace(condicion))
                {
                    examenesList = examenesList.Where(e =>
                    {
                        var entity = (ExamenesEntity)e;
                        return (entity.Description != null && entity.Description.Contains(condicion, StringComparison.OrdinalIgnoreCase));
                    }).ToList();
                }
                examenesDto = _mapper.Map<ExamenesDto[]>(examenesList);
                _logger.LogInformation("Examenes obtenidos con condición: {Count}", examenesDto.Length);
                return Task.FromResult<IEnumerable<ExamenesDto>>(examenesDto);
            }
            catch (Exception)
            {
                _logger.LogError("Error al obtener los estudios con condición: {condicion}", condicion);
                throw;
            }
        }

        public async Task<ExamenesDto> UpdateExamenesAsync(UpdateExamenesDto updateExamenesDto)
        {
            try
            {
                _logger.LogInformation("Iniciando actualización de Examen con ID: {estudiosId}", updateExamenesDto.examsid);
                if (updateExamenesDto.examsid <= 0)
                {
                    throw new ArgumentException("El ID del examen debe ser mayor a 0", nameof(updateExamenesDto.examsid));
                }
                var examenesExist = await _examenesRepository.GetExamenesByIdAsync(updateExamenesDto.examsid);
                if (examenesExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Examen con ID: {updateExamenesDto.examsid}");
                }
                var examenesExistente = (ExamenesEntity)examenesExist;
                if (examenesExistente.IsDeleted)
                {
                    throw new InvalidOperationException($"No se puede actualizar el Examen con ID: {updateExamenesDto.examsid} por que está eliminado lógicamente.");
                }
                var examenesToUpdate = _mapper.Map<ExamenesEntity>(updateExamenesDto);
                examenesToUpdate.CreatedAt = examenesExistente.CreatedAt;
                examenesToUpdate.CreatedBy = examenesExistente.CreatedBy;
                examenesToUpdate.IsDeleted = false;
                examenesToUpdate.UpdatedAt = DateTime.Now;


                var result = await _examenesRepository.UpdateExamenesAsync(examenesToUpdate);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo actualizar el Examen con ID: {updateExamenesDto.examsid}");
                }
                var examenActualizado = await _examenesRepository.GetExamenesByIdAsync(updateExamenesDto.examsid);
                if (examenActualizado == null)
                {
                    throw new Exception("Error al recuperar el examen recién actualizado.");
                }
                var examenEntity2 = (ExamenesEntity)examenActualizado;
                var examenDto = _mapper.Map<ExamenesDto>(examenEntity2);
                return examenDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el Examen con ID: {examsid}", updateExamenesDto.examsid);
                throw;
            }
        }
    }
}
