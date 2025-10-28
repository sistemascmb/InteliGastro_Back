using AutoMapper;
using Domain.DomainInterfaces;
using Infraestructure.Models;
using WebApi.Application.DTO.Suministros;
using WebApi.Application.Services.Suministros;

namespace WebApi.Application.Services.Suministros
{
    public class SuministrosService : ISuministrosService
    {
        private readonly ILogger<SuministrosService> _logger;
        private readonly ISuministrosRepository _suministrosRepository;
        private readonly IMapper _mapper;
        public SuministrosService(
            ILogger<SuministrosService> logger,
            ISuministrosRepository suministrosRepository,
            ICentroRepository centroRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _suministrosRepository = suministrosRepository ?? throw new ArgumentNullException(nameof(suministrosRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SuministrosDto> CreateSuministrosAsync(CreateSuministrosDto createSuministrosDto)
        {
            try
            {
                _logger.LogInformation("Iniciando creación de Suministros");
                
                var SuministrosEntity = _mapper.Map<SuministrosEntity>(createSuministrosDto);
                SuministrosEntity.IsDeleted = false;
                SuministrosEntity.CreatedAt = DateTime.Now;
                var newSuministrosId = await _suministrosRepository.CreateSuministrosAsync(SuministrosEntity);
                _logger.LogInformation("Suministros creado con ID: {SuministrosId}", newSuministrosId);
                var newSuministros = await _suministrosRepository.GetSuministrosByIdAsync(newSuministrosId);
                if (newSuministros == null)
                {
                    throw new Exception("Error al recuperar el Suministro recién creado.");
                }

                var newSuministrosDto = _mapper.Map<SuministrosEntity>(newSuministros);
                var SuministrosDto = _mapper.Map<SuministrosDto>(newSuministros);
                return SuministrosDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear Suministros");
                throw;
            }
        }

        public async Task<bool> DeleteSuministrosAsync(long SuministrosId, string eliminadoPor)
        {
            try
            {
                _logger.LogInformation("Iniciando eliminación lógica de Suministro con ID: {SuministrosId}", SuministrosId);
                if (SuministrosId <= 0)
                {
                    throw new ArgumentException("El ID del Suministro debe ser mayor a 0", nameof(SuministrosId));
                }
                var SuministrosExist = await _suministrosRepository.GetSuministrosByIdAsync(SuministrosId);
                var SuministrosExistente = (SuministrosEntity)SuministrosExist;
                if (SuministrosExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Suministro con ID: {SuministrosId}");
                }
                if (SuministrosExistente.IsDeleted)
                {
                    throw new InvalidOperationException($"No se puede eliminar el Suministro con ID: {SuministrosId} por que ya está eiminado lógicamente.");
                }
                SuministrosExistente.IsDeleted = true;
                SuministrosExistente.UpdatedAt = DateTime.Now;
                SuministrosExistente.UpdatedBy = eliminadoPor;

                var result = await _suministrosRepository.UpdateSuministrosAsync(SuministrosExistente);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo eliminar el Suministro con ID: {SuministrosId}");
                }
                _logger.LogInformation("Suministro con ID: {SuministrosId} eliminado lógicamente", SuministrosId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el Suministro con ID: {SuministrosId}", SuministrosId);
                throw;
            }
        }

        public async Task<IEnumerable<SuministrosDto>> GetAllSuministrosAsync()
        {
            Task<IEnumerable<SuministrosDto>> SuministrosDto;
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de todos los Suministros");
                var Suministros = _suministrosRepository.GetAllSuministrosAsync().Result;
                var SuministrosList = Suministros.Where(e => !((SuministrosEntity)e).IsDeleted).ToList();
                SuministrosDto = Task.FromResult(_mapper.Map<IEnumerable<SuministrosDto>>(SuministrosList));
                _logger.LogInformation("Suministros obtenidos: {Count}", SuministrosList.Count());
                return await SuministrosDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los Suministros");
                throw;
            }
        }

        public async Task<SuministrosDto> GetSuministrosByIdAsync(long SuministrosId)
        {
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de Suministro por ID: {SuministrosId}", SuministrosId);
                if (SuministrosId <= 0)
                {
                    throw new ArgumentException("El ID del Suministro debe ser mayor a 0", nameof(SuministrosId));
                }

                var Suministros = await _suministrosRepository.GetSuministrosByIdAsync(SuministrosId);
                if (Suministros == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Suministro con ID: {SuministrosId}");
                }
                var SuministrosEntity = (SuministrosEntity)Suministros;
                if (SuministrosEntity.IsDeleted)
                {
                    throw new InvalidOperationException($"El Suministro con ID: {SuministrosId} está eliminado lógicamente.");
                }
                var SuministrosDto = _mapper.Map<SuministrosDto>(SuministrosEntity);
                _logger.LogInformation("Suministro obtenido con ID: {SuministrosId}", SuministrosId);
                return SuministrosDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<IEnumerable<SuministrosDto>> GetWhereAsync(string condicion)
        {
            SuministrosDto[] SuministrosDto;
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de Suministros con condición: {condicion}", condicion);
                var Suministros = _suministrosRepository.GetAllSuministrosAsync().Result;
                var SuministrosList = Suministros.Where(e => !((SuministrosEntity)e).IsDeleted).ToList();
                if (!string.IsNullOrWhiteSpace(condicion))
                {
                    SuministrosList = SuministrosList.Where(e =>
                    {
                        var entity = (SuministrosEntity)e;
                        return (entity.Name != null && entity.Name.Contains(condicion, StringComparison.OrdinalIgnoreCase));
                    }).ToList();
                }
                SuministrosDto = _mapper.Map<SuministrosDto[]>(SuministrosList);
                _logger.LogInformation("Suministros obtenidos con condición: {Count}", SuministrosDto.Length);
                return Task.FromResult<IEnumerable<SuministrosDto>>(SuministrosDto);
            }
            catch (Exception)
            {
                _logger.LogError("Error al obtener los Suministros con condición: {condicion}", condicion);
                throw;
            }
        }

        public async Task<SuministrosDto> UpdateSuministrosAsync(UpdateSuministrosDto updateSuministrosDto)
        {
            try
            {
                _logger.LogInformation("Iniciando actualización de Suministro con ID: {SuministrosId}", updateSuministrosDto.provisionid);
                if (updateSuministrosDto.provisionid <= 0)
                {
                    throw new ArgumentException("El ID del Suministro debe ser mayor a 0", nameof(updateSuministrosDto.provisionid));
                }
                
                var SuministrosExist = await _suministrosRepository.GetSuministrosByIdAsync(updateSuministrosDto.provisionid);
                if (SuministrosExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Suministro con ID: {updateSuministrosDto.provisionid}");
                }
                var SuministrosExistente = (SuministrosEntity)SuministrosExist;
                if (SuministrosExistente.IsDeleted)
                {
                    throw new InvalidOperationException($"No se puede actualizar el Suministro con ID: {updateSuministrosDto.provisionid} por que está eliminado lógicamente.");
                }
                var SuministrosToUpdate = _mapper.Map<SuministrosEntity>(updateSuministrosDto);
                SuministrosToUpdate.CreatedAt = SuministrosExistente.CreatedAt;
                SuministrosToUpdate.CreatedBy = SuministrosExistente.CreatedBy;
                SuministrosToUpdate.IsDeleted = false;
                SuministrosToUpdate.UpdatedAt = DateTime.Now;

                var result = await _suministrosRepository.UpdateSuministrosAsync(SuministrosToUpdate);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo actualizar el Suministro con ID: {updateSuministrosDto.provisionid}");
                }
                var SuministrosActualizado = await _suministrosRepository.GetSuministrosByIdAsync(updateSuministrosDto.provisionid);
                if (SuministrosActualizado == null)
                {
                    throw new Exception("Error al recuperar el Suministro recién actualizado.");
                }
                var estudioEntity2 = (SuministrosEntity)SuministrosActualizado;
                var SuministrosDto = _mapper.Map<SuministrosDto>(estudioEntity2);
                return SuministrosDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el Suministro con ID: {SuministrosId}", updateSuministrosDto.provisionid);
                throw;
            }
        }
    }
}
