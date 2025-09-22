using AutoMapper;
using Domain.DomainInterfaces;
using Infraestructure.Models;
using WebApi.Application.DTO.Cie10;
using WebApi.Application.Services.Cie10;

namespace WebApi.Application.Services.Cie10
{
    public class Cie10Service : ICie10Service
    {
        private readonly ILogger<Cie10Service> _logger;
        private readonly ICie10Repository _cie10Repository;
        private readonly ICentroRepository _centroRepository;
        private readonly IMapper _mapper;
        public Cie10Service(
            ILogger<Cie10Service> logger,
            ICie10Repository cie10Repository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cie10Repository = cie10Repository ?? throw new ArgumentNullException(nameof(cie10Repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Cie10Dto> CreateCie10Async(CreateCie10Dto createCie10Dto)
        {
            try
            {
                _logger.LogInformation("Iniciando creación de Cie10");
                
                var Cie10Entity = _mapper.Map<Cie10Entity>(createCie10Dto);
                Cie10Entity.IsDeleted = false;
                Cie10Entity.CreatedAt = DateTime.UtcNow;
                var newCie10Id = await _cie10Repository.CreateCie10Async(Cie10Entity);
                _logger.LogInformation("Cie10 creado con ID: {Cie10Id}", newCie10Id);
                var newCie10 = await _cie10Repository.GetCie10ByIdAsync(newCie10Id);
                if (newCie10 == null)
                {
                    throw new Exception("Error al recuperar el Cie10 recién creado.");
                }

                var newCie10Dto = _mapper.Map<Cie10Entity>(newCie10);
                var Cie10Dto = _mapper.Map<Cie10Dto>(newCie10);
                return Cie10Dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear Cie10");
                throw;
            }
        }

        public async Task<bool> DeleteCie10Async(long Cie10Id, string eliminadoPor)
        {
            try
            {
                _logger.LogInformation("Iniciando eliminación lógica de Cie10 con ID: {Cie10Id}", Cie10Id);
                if (Cie10Id <= 0)
                {
                    throw new ArgumentException("El ID del Cie10 debe ser mayor a 0", nameof(Cie10Id));
                }
                var Cie10Exist = await _cie10Repository.GetCie10ByIdAsync(Cie10Id);
                var Cie10Existente = (Cie10Entity)Cie10Exist;
                if (Cie10Exist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Cie10 con ID: {Cie10Id}");
                }
                if (Cie10Existente.IsDeleted)
                {
                    throw new InvalidOperationException($"No se puede eliminar el Cie10 con ID: {Cie10Id} por que ya está eiminado lógicamente.");
                }
                Cie10Existente.IsDeleted = true;
                Cie10Existente.UpdatedAt = DateTime.UtcNow;
                Cie10Existente.UpdatedBy = eliminadoPor;

                var result = await _cie10Repository.UpdateCie10Async(Cie10Existente);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo eliminar el Cie10 con ID: {Cie10Id}");
                }
                _logger.LogInformation("Cie10 con ID: {Cie10Id} eliminado lógicamente", Cie10Id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el Cie10 con ID: {Cie10Id}", Cie10Id);
                throw;
            }
        }

        public async Task<IEnumerable<Cie10Dto>> GetAllCie10Async()
        {
            Task<IEnumerable<Cie10Dto>> Cie10Dto;
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de todos los Cie10");
                var Cie10 = _cie10Repository.GetAllCie10Async().Result;
                var Cie10List = Cie10.Where(e => !((Cie10Entity)e).IsDeleted).ToList();
                Cie10Dto = Task.FromResult(_mapper.Map<IEnumerable<Cie10Dto>>(Cie10List));
                _logger.LogInformation("Cie10 obtenidos: {Count}", Cie10List.Count());
                return await Cie10Dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los Cie10");
                throw;
            }
        }

        public async Task<Cie10Dto> GetCie10ByIdAsync(long Cie10Id)
        {
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de Cie10 por ID: {Cie10Id}", Cie10Id);
                if (Cie10Id <= 0)
                {
                    throw new ArgumentException("El ID del Cie10 debe ser mayor a 0", nameof(Cie10Id));
                }

                var Cie10 = await _cie10Repository.GetCie10ByIdAsync(Cie10Id);
                if (Cie10 == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Cie10 con ID: {Cie10Id}");
                }
                var Cie10Entity = (Cie10Entity)Cie10;
                if (Cie10Entity.IsDeleted)
                {
                    throw new InvalidOperationException($"El Cie10 con ID: {Cie10Id} está eliminado lógicamente.");
                }
                var Cie10Dto = _mapper.Map<Cie10Dto>(Cie10Entity);
                _logger.LogInformation("Cie10 obtenido con ID: {Cie10Id}", Cie10Id);
                return Cie10Dto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<IEnumerable<Cie10Dto>> GetWhereAsync(string condicion)
        {
            Cie10Dto[] Cie10Dto;
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de Cie10 con condición: {condicion}", condicion);
                var Cie10 = _cie10Repository.GetAllCie10Async().Result;
                var Cie10List = Cie10.Where(e => !((Cie10Entity)e).IsDeleted).ToList();
                if (!string.IsNullOrWhiteSpace(condicion))
                {
                    Cie10List = Cie10List.Where(e =>
                    {
                        var entity = (Cie10Entity)e;
                        return (entity.Code != null && entity.Code.Contains(condicion, StringComparison.OrdinalIgnoreCase)) ||
                               (entity.Description != null && entity.Description.Contains(condicion, StringComparison.OrdinalIgnoreCase));
                    }).ToList();
                }
                Cie10Dto = _mapper.Map<Cie10Dto[]>(Cie10List);
                _logger.LogInformation("Cie10 obtenidos con condición: {Count}", Cie10Dto.Length);
                return Task.FromResult<IEnumerable<Cie10Dto>>(Cie10Dto);
            }
            catch (Exception)
            {
                _logger.LogError("Error al obtener los Cie10 con condición: {condicion}", condicion);
                throw;
            }
        }

        public async Task<Cie10Dto> UpdateCie10Async(UpdateCie10Dto updateCie10Dto)
        {
            try
            {
                _logger.LogInformation("Iniciando actualización de Cie10 con ID: {Cie10Id}", updateCie10Dto.cieid);
                if (updateCie10Dto.cieid <= 0)
                {
                    throw new ArgumentException("El ID del Cie10 debe ser mayor a 0", nameof(updateCie10Dto.cieid));
                }
                
                var Cie10Exist = await _cie10Repository.GetCie10ByIdAsync(updateCie10Dto.cieid);
                if (Cie10Exist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Cie10 con ID: {updateCie10Dto.cieid}");
                }
                var Cie10Existente = (Cie10Entity)Cie10Exist;
                if (Cie10Existente.IsDeleted)
                {
                    throw new InvalidOperationException($"No se puede actualizar el Cie10 con ID: {updateCie10Dto.cieid} por que está eliminado lógicamente.");
                }
                var Cie10ToUpdate = _mapper.Map<Cie10Entity>(updateCie10Dto);
                Cie10ToUpdate.CreatedAt = Cie10Existente.CreatedAt;
                Cie10ToUpdate.CreatedBy = Cie10Existente.CreatedBy;
                Cie10ToUpdate.IsDeleted = false;
                var result = await _cie10Repository.UpdateCie10Async(Cie10ToUpdate);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo actualizar el Cie10 con ID: {updateCie10Dto.cieid}");
                }
                var Cie10Actualizado = await _cie10Repository.GetCie10ByIdAsync(updateCie10Dto.cieid);
                if (Cie10Actualizado == null)
                {
                    throw new Exception("Error al recuperar el Cie10 recién actualizado.");
                }
                var estudioEntity2 = (Cie10Entity)Cie10Actualizado;
                var Cie10Dto = _mapper.Map<Cie10Dto>(estudioEntity2);
                return Cie10Dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el Cie10 con ID: {Cie10Id}", updateCie10Dto.cieid);
                throw;
            }
        }
    }
}
