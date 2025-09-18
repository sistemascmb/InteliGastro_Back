using AutoMapper;
using Domain.DomainInterfaces;
using Infraestructure.Models;
using Infraestructure.Persistence;
using Infraestructure.Repositories;
using System.Net;
using WebApi.Application.DTO.Salas;

namespace WebApi.Application.Services.Salas
{
    public class SalasService : ISalasService
    {
        private readonly ILogger<SalasService> _logger;
        private readonly ISalasRepository _salasRepository;
        private readonly ICentroRepository _centroRepository;
        private readonly IMapper _mapper;
        public SalasService(
            ILogger<SalasService> logger,
            ISalasRepository salasRepository,
            ICentroRepository centroRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _salasRepository = salasRepository ?? throw new ArgumentNullException(nameof(salasRepository));
            _centroRepository = centroRepository ?? throw new ArgumentNullException(nameof(centroRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<SalasDto> CreateSalasAsync(CreateSalasDto createSalasDto)
        {
            try
            {
                _logger.LogInformation("Iniciando creación de salas.");
                if (createSalasDto.CentroId <= 0)
                {
                    throw new ArgumentException("El CentroId debe ser mayor a 0", nameof(createSalasDto.CentroId));
                }

                var centroExiste = await _centroRepository.GetCentroByIdAsync(createSalasDto.CentroId);
                if (centroExiste == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Centro con ID: {createSalasDto.CentroId}. No se puede crear el personal.");
                }

                var centroEntity = (CentroEntity)centroExiste;

                if (centroEntity.IsDeleted)
                {
                    throw new InvalidOperationException($"El Centro con ID: {createSalasDto.CentroId} está eliminado lógicamente. No se puede crear el personal.");
                }

                var salasEntity = _mapper.Map<SalasEntity>(createSalasDto);
                salasEntity.IsDeleted = false; // Asegurarse de que IsDeleted esté en false al crear
                salasEntity.CreatedAt = DateTime.UtcNow; // Establecer la fecha de creación
                var newSalasId = await _salasRepository.CreateSalasAsync(salasEntity);
                _logger.LogInformation("Sala creada con ID: {SalasId}", newSalasId);
                var newSalas = await _salasRepository.GetSalasByIdAsync(newSalasId);
                if (newSalas == null)
                {
                    throw new Exception("Error al recuperar la sala recién creada.");
                }

                var salasEntity2 = (SalasEntity)newSalas;
                var newSalasDto = _mapper.Map<SalasDto>(salasEntity2);

                return newSalasDto;
            }
            catch (Exception)
            {
                _logger.LogError("Error al crear la sala.");
                throw;
            }
        }

        public async Task<bool> DeleteSalasAsync(long salasId, string eliminadoPor)
        {
            try
            {
                _logger.LogInformation("Iniciando eliminación lógica de sala con ID: {SalasId}", salasId);
                if (salasId <= 0)
                {
                    throw new ArgumentException("El ID de la sala debe ser maypr a 0.", nameof(salasId));
                }

                var salasExiste = await _salasRepository.GetSalasByIdAsync(salasId);
                var salasExistentes = (SalasEntity)salasExiste;
                if (salasExiste == null)
                {
                    throw new KeyNotFoundException($"No se encontró la sala con ID: {salasId}. No se puede eliminar.");
                }

                if (salasExistentes.IsDeleted)
                {
                    throw new InvalidOperationException($"La sala con ID: {salasId} ya está eliminada lógicamente.");
                }

                salasExistentes.IsDeleted = true;
                salasExistentes.UpdatedAt = DateTime.UtcNow;
                salasExistentes.UpdatedBy = eliminadoPor;

                var result = await _salasRepository.UpdateSalasAsync(salasExistentes);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo eliminar la Sala con ID: {salasId}.");
                }
                _logger.LogInformation("Sala con ID: {SalasId} eliminada lógicamente.", salasId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el Estudio con ID: {salasId}", salasId);
                throw;
            }
            
        }

        public async Task<IEnumerable<SalasDto>> GetAllSalasAsync()
        {
            Task<IEnumerable<SalasDto>> salasDto;
            try
            {
                _logger.LogInformation("Recuperando todas las salas no eliminadas lógicamente.");
                var salas = await _salasRepository.GetAllSalasAsync();
                var salasList = salas.Where(s => !((SalasEntity)s).IsDeleted).ToList();
                salasDto = Task.FromResult(_mapper.Map<IEnumerable<SalasDto>>(salasList));
                _logger.LogInformation("Salas recuperadas: {Count}", salasList.Count);
                return await salasDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al recuperar las salas.");
                throw;
            }
        }

        public async Task<SalasDto> GetSalasByIdAsync(long salasId)
        {
            try
            {
                _logger.LogInformation("Iniciando obtención de Salas por ID: {salasId}", salasId);
                if (salasId <= 0)
                {
                    throw new ArgumentException("El ID de la sala debe ser mayor a 0.", nameof(salasId));
                }
                var salas = await _salasRepository.GetSalasByIdAsync(salasId);
                if (salas == null || ((SalasEntity)salas).IsDeleted)
                {
                    throw new KeyNotFoundException($"No se encontró la sala con ID: {salasId} o está eliminada lógicamente.");
                }
                var salasDto = _mapper.Map<SalasDto>(salas);
                _logger.LogInformation("Sala recuperada con ID: {SalasId}", salasId);
                return salasDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al recuperar las salas.");
                throw;
            }
        }

        public Task<IEnumerable<SalasDto>> GetWhereAsync(string condicion)
        {
            SalasDto[] salasDto;
            try
            {
                _logger.LogInformation("Iniciando obtención de salas con condición: {Condicion}", condicion);
                var salas = _salasRepository.GetAllSalasAsync().Result;
                var salasList = salas.Where(s => !((SalasEntity)s).IsDeleted).ToList();
                if (!string.IsNullOrWhiteSpace(condicion))
                {
                    // Aquí puedes implementar la lógica para filtrar según la condición
                    salasList = salasList.Where(s =>
                    {
                        var entity = (SalasEntity)s;
                        return (entity.Name != null && entity.Name.Contains(condicion, StringComparison.OrdinalIgnoreCase)) ||
                               (entity.Description != null && entity.Description.Contains(condicion, StringComparison.OrdinalIgnoreCase));
                    }).ToList();
                }   
                salasDto = _mapper.Map<SalasDto[]>(salasList);
                _logger.LogInformation("Salas recuperadas con condición: {Count}", salasDto.Length);
                return Task.FromResult((IEnumerable<SalasDto>)salasDto);
            }
            catch (Exception)
            {
                _logger.LogError("Error al recuperar las salas con condición: {Condicion}", condicion);
                throw;
            }
        }

        public async Task<SalasDto> UpdateSalasAsync(UpdateSalasDto updateSalasDto)
        {
            try
            {
                _logger.LogInformation("Iniciando actualizacón de Sala con ID: {SalasId}", updateSalasDto.procedureroomid);
                if (updateSalasDto.procedureroomid <= 0)
                {
                    throw new ArgumentException("El ID de la sala debe ser mayor a 0.", nameof(updateSalasDto.procedureroomid));
                }
                // Validar que el CentroId existe
                if (updateSalasDto.CentroId <= 0)
                {
                    throw new ArgumentException("El CentroId debe ser mayor a 0", nameof(updateSalasDto.CentroId));
                }

                var centroExiste = await _centroRepository.GetCentroByIdAsync(updateSalasDto.CentroId);
                if (centroExiste == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Centro con ID: {updateSalasDto.CentroId}. No se puede actualizar el personal.");
                }

                var centroEntity = (CentroEntity)centroExiste;
                if (centroEntity.IsDeleted)
                {
                    throw new InvalidOperationException($"El Centro con ID: {updateSalasDto.CentroId} está eliminado lógicamente. No se puede actualizar el personal.");
                }

                var salasExist = await _salasRepository.GetSalasByIdAsync(updateSalasDto.procedureroomid);
                if (salasExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró la sala con ID: {updateSalasDto.procedureroomid}. No se puede actualizar.");
                }
                var salasExistente = (SalasEntity)salasExist;
                if (salasExistente.IsDeleted)
                {
                    throw new InvalidOperationException($"La sala con ID: {updateSalasDto.procedureroomid} está eliminada lógicamente. No se puede actualizar.");
                }
                var salasToUpdate = _mapper.Map<SalasEntity>(updateSalasDto);
                salasToUpdate.CreatedAt = salasExistente.CreatedAt; // Mantener la fecha de creación original
                salasToUpdate.CreatedBy = salasExistente.CreatedBy; // Actualizar la fecha de modificación
                salasToUpdate.IsDeleted = false; // Mantener el estado de eliminación original

                var result = await _salasRepository.UpdateSalasAsync(salasToUpdate);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo actualizar la sala con ID: {updateSalasDto.procedureroomid}.");
                }

                var updatedSalas = await _salasRepository.GetSalasByIdAsync(updateSalasDto.procedureroomid);
                if (updatedSalas == null)
                {
                    throw new Exception("Error al recuperar la sala recién actualizada.");
                }

                var salasEntity = (SalasEntity)updatedSalas;
                var salasDto = _mapper.Map<SalasDto>(salasEntity);
                return salasDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la sala con ID: {SalasId}", updateSalasDto.procedureroomid);
                throw;
            }
        }
    }
}
