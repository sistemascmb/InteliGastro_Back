using AutoMapper;
using Domain.DomainInterfaces;
using Infraestructure.Models;
using Infraestructure.Repositories;
using WebApi.Application.DTO.Macros;
using WebApi.Application.Services.Macros;

namespace WebApi.Application.Services.Macros
{
    public class MacrosService : IMacrosService
    {
        private readonly ILogger<MacrosService> _logger;
        private readonly IMacrosRepository _macrosRepository;
        private readonly IPersonalRepository _personalRepository;

        private readonly IMapper _mapper;
        public MacrosService(
            ILogger<MacrosService> logger,
            IMacrosRepository macrosRepository,
            IPersonalRepository personalRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _macrosRepository = macrosRepository ?? throw new ArgumentNullException(nameof(macrosRepository));
            _personalRepository = personalRepository ?? throw new ArgumentNullException(nameof(personalRepository));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<MacrosDto> CreateMacrosAsync(CreateMacrosDto createMacrosDto)
        {
            try
            {
                _logger.LogInformation("Iniciando creación de Macros");
                
                var MacrosEntity = _mapper.Map<MacrosEntity>(createMacrosDto);
                MacrosEntity.IsDeleted = false;
                MacrosEntity.CreatedAt = DateTime.Now;
                var newMacrosId = await _macrosRepository.CreateMacrosAsync(MacrosEntity);
                _logger.LogInformation("Macros creado con ID: {MacrosId}", newMacrosId);
                var newMacros = await _macrosRepository.GetMacrosByIdAsync(newMacrosId);
                if (newMacros == null)
                {
                    throw new Exception("Error al recuperar el macros recién creado.");
                }

                var newMacrosDto = _mapper.Map<MacrosEntity>(newMacros);
                var MacrosDto = _mapper.Map<MacrosDto>(newMacros);
                return MacrosDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear Macros");
                throw;
            }
        }

        public async Task<bool> DeleteMacrosAsync(long MacrosId, string eliminadoPor)
        {
            try
            {
                _logger.LogInformation("Iniciando eliminación lógica de Macros con ID: {MacrosId}", MacrosId);
                if (MacrosId <= 0)
                {
                    throw new ArgumentException("El ID del macros debe ser mayor a 0", nameof(MacrosId));
                }
                var MacrosExist = await _macrosRepository.GetMacrosByIdAsync(MacrosId);
                var MacrosExistente = (MacrosEntity)MacrosExist;
                if (MacrosExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el macros con ID: {MacrosId}");
                }
                if (MacrosExistente.IsDeleted)
                {
                    throw new InvalidOperationException($"No se puede eliminar el Macros con ID: {MacrosId} por que ya está eiminado lógicamente.");
                }
                MacrosExistente.IsDeleted = true;
                MacrosExistente.UpdatedAt = DateTime.Now;
                MacrosExistente.UpdatedBy = eliminadoPor;

                var result = await _macrosRepository.UpdateMacrosAsync(MacrosExistente);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo eliminar el Macros con ID: {MacrosId}");
                }
                _logger.LogInformation("Macros con ID: {MacrosId} eliminado lógicamente", MacrosId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el Macros con ID: {MacrosId}", MacrosId);
                throw;
            }
        }

        public async Task<IEnumerable<MacrosDto>> GetAllMacrosAsync()
        {
            Task<IEnumerable<MacrosDto>> MacrosDto;
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de todos los Macros");
                var Macros = _macrosRepository.GetAllMacrosAsync().Result;
                var MacrosList = Macros.Where(e => !((MacrosEntity)e).IsDeleted).ToList();
                MacrosDto = Task.FromResult(_mapper.Map<IEnumerable<MacrosDto>>(MacrosList));
                _logger.LogInformation("Macros obtenidos: {Count}", MacrosList.Count());
                return await MacrosDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los Macros");
                throw;
            }
        }

        public async Task<MacrosDto> GetMacrosByIdAsync(long MacrosId)
        {
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de Macros por ID: {MacrosId}", MacrosId);
                if (MacrosId <= 0)
                {
                    throw new ArgumentException("El ID del macros debe ser mayor a 0", nameof(MacrosId));
                }

                var Macros = await _macrosRepository.GetMacrosByIdAsync(MacrosId);
                if (Macros == null)
                {
                    throw new KeyNotFoundException($"No se encontró el macros con ID: {MacrosId}");
                }
                var MacrosEntity = (MacrosEntity)Macros;
                if (MacrosEntity.IsDeleted)
                {
                    throw new InvalidOperationException($"El macros con ID: {MacrosId} está eliminado lógicamente.");
                }
                var MacrosDto = _mapper.Map<MacrosDto>(MacrosEntity);
                _logger.LogInformation("Macros obtenido con ID: {MacrosId}", MacrosId);
                return MacrosDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<IEnumerable<MacrosDto>> GetWhereAsync(string condicion)
        {
            MacrosDto[] MacrosDto;
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de Macros con condición: {condicion}", condicion);
                var Macros = _macrosRepository.GetAllMacrosAsync().Result;
                var MacrosList = Macros.Where(e => !((MacrosEntity)e).IsDeleted).ToList();
                if (!string.IsNullOrWhiteSpace(condicion))
                {
                    MacrosList = MacrosList.Where(e =>
                    {
                        var entity = (MacrosEntity)e;
                        return (entity.Name != null && entity.Name.Contains(condicion, StringComparison.OrdinalIgnoreCase)) ||
                               (entity.Description != null && entity.Description.Contains(condicion, StringComparison.OrdinalIgnoreCase));
                    }).ToList();
                }
                MacrosDto = _mapper.Map<MacrosDto[]>(MacrosList);
                _logger.LogInformation("Macros obtenidos con condición: {Count}", MacrosDto.Length);
                return Task.FromResult<IEnumerable<MacrosDto>>(MacrosDto);
            }
            catch (Exception)
            {
                _logger.LogError("Error al obtener los Macros con condición: {condicion}", condicion);
                throw;
            }
        }

        public async Task<MacrosDto> UpdateMacrosAsync(UpdateMacrosDto updateMacrosDto)
        {
            try
            {
                _logger.LogInformation("Iniciando actualización de Macros con ID: {MacrosId}", updateMacrosDto.macrosid);
                if (updateMacrosDto.macrosid <= 0)
                {
                    throw new ArgumentException("El ID del macros debe ser mayor a 0", nameof(updateMacrosDto.macrosid));
                }
                
                var MacrosExist = await _macrosRepository.GetMacrosByIdAsync(updateMacrosDto.macrosid);
                if (MacrosExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Macros con ID: {updateMacrosDto.macrosid}");
                }
                var MacrosExistente = (MacrosEntity)MacrosExist;
                if (MacrosExistente.IsDeleted)
                {
                    throw new InvalidOperationException($"No se puede actualizar el Macros con ID: {updateMacrosDto.macrosid} por que está eliminado lógicamente.");
                }
                var MacrosToUpdate = _mapper.Map<MacrosEntity>(updateMacrosDto);
                MacrosToUpdate.CreatedAt = MacrosExistente.CreatedAt;
                MacrosToUpdate.CreatedBy = MacrosExistente.CreatedBy;
                MacrosToUpdate.IsDeleted = false;
                MacrosToUpdate.UpdatedAt = DateTime.Now;


                var result = await _macrosRepository.UpdateMacrosAsync(MacrosToUpdate);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo actualizar el Macros con ID: {updateMacrosDto.macrosid}");
                }
                var MacrosActualizado = await _macrosRepository.GetMacrosByIdAsync(updateMacrosDto.macrosid);
                if (MacrosActualizado == null)
                {
                    throw new Exception("Error al recuperar el macros recién actualizado.");
                }
                var estudioEntity2 = (MacrosEntity)MacrosActualizado;
                var MacrosDto = _mapper.Map<MacrosDto>(estudioEntity2);
                return MacrosDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el Macros con ID: {MacrosId}", updateMacrosDto.macrosid);
                throw;
            }
        }
    }
}
