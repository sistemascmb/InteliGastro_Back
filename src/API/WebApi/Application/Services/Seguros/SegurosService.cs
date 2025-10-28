using AutoMapper;
using Domain.DomainInterfaces;
using Infraestructure.Models;
using WebApi.Application.DTO.Seguros;
using WebApi.Application.DTO.Seguros;
using WebApi.Application.Services.Seguros;

namespace WebApi.Application.Services.Seguros
{
    public class SegurosService : ISegurosService
    {
        private readonly ISegurosRepository _segurosRepository;
        private readonly ILogger<SegurosService> _logger;
        private readonly IMapper _mapper;
        public SegurosService(
            ILogger<SegurosService> logger,
            ISegurosRepository segurosRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _segurosRepository = segurosRepository ?? throw new ArgumentNullException(nameof(segurosRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SegurosDto> CreateSegurosAsync(CreateSegurosDto createSegurosDto)
        {
            try
            {
                _logger.LogInformation("Iniciando creación de Seguros");
                
                var SegurosEntity = _mapper.Map<SegurosEntity>(createSegurosDto);
                SegurosEntity.IsDeleted = false;
                SegurosEntity.CreatedAt = DateTime.Now;
                var newSegurosId = await _segurosRepository.CreateSegurosAsync(SegurosEntity);
                _logger.LogInformation("Seguros creado con ID: {SegurosId}", newSegurosId);
                var newSeguros = await _segurosRepository.GetSegurosByIdAsync(newSegurosId);
                if (newSeguros == null)
                {
                    throw new Exception("Error al recuperar el Seguros recién creado.");
                }

                var newSegurosDto = _mapper.Map<SegurosEntity>(newSeguros);
                var SegurosDto = _mapper.Map<SegurosDto>(newSeguros);
                return SegurosDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear Seguros");
                throw;
            }
        }

        public async Task<bool> DeleteSegurosAsync(long SegurosId, string eliminadoPor)
        {
            try
            {
                _logger.LogInformation("Iniciando eliminación lógica de Seguros con ID: {SegurosId}", SegurosId);
                if (SegurosId <= 0)
                {
                    throw new ArgumentException("El ID del Seguros debe ser mayor a 0", nameof(SegurosId));
                }
                var SegurosExist = await _segurosRepository.GetSegurosByIdAsync(SegurosId);
                var SegurosExistente = (SegurosEntity)SegurosExist;
                if (SegurosExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Seguros con ID: {SegurosId}");
                }
                if (SegurosExistente.IsDeleted)
                {
                    throw new InvalidOperationException($"No se puede eliminar el Seguros con ID: {SegurosId} por que ya está eiminado lógicamente.");
                }
                SegurosExistente.IsDeleted = true;
                SegurosExistente.UpdatedAt = DateTime.Now;
                SegurosExistente.UpdatedBy = eliminadoPor;

                var result = await _segurosRepository.UpdateSegurosAsync(SegurosExistente);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo eliminar el Seguros con ID: {SegurosId}");
                }
                _logger.LogInformation("Seguros con ID: {SegurosId} eliminado lógicamente", SegurosId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el Seguros con ID: {SegurosId}", SegurosId);
                throw;
            }
        }

        public async Task<IEnumerable<SegurosDto>> GetAllSegurosAsync()
        {
            Task<IEnumerable<SegurosDto>> SegurosDto;
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de todos los Seguros");
                var Seguros = _segurosRepository.GetAllSegurosAsync().Result;
                var SegurosList = Seguros.Where(e => !((SegurosEntity)e).IsDeleted).ToList();
                SegurosDto = Task.FromResult(_mapper.Map<IEnumerable<SegurosDto>>(SegurosList));
                _logger.LogInformation("Seguros obtenidos: {Count}", SegurosList.Count());
                return await SegurosDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los Seguros");
                throw;
            }
        }

        public async Task<SegurosDto> GetSegurosByIdAsync(long SegurosId)
        {
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de Seguros por ID: {SegurosId}", SegurosId);
                if (SegurosId <= 0)
                {
                    throw new ArgumentException("El ID del Seguros debe ser mayor a 0", nameof(SegurosId));
                }

                var Seguros = await _segurosRepository.GetSegurosByIdAsync(SegurosId);
                if (Seguros == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Seguros con ID: {SegurosId}");
                }
                var SegurosEntity = (SegurosEntity)Seguros;
                if (SegurosEntity.IsDeleted)
                {
                    throw new InvalidOperationException($"El Seguros con ID: {SegurosId} está eliminado lógicamente.");
                }
                var SegurosDto = _mapper.Map<SegurosDto>(SegurosEntity);
                _logger.LogInformation("Seguros obtenido con ID: {SegurosId}", SegurosId);
                return SegurosDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<IEnumerable<SegurosDto>> GetWhereAsync(string condicion)
        {
            SegurosDto[] SegurosDto;
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de Seguros con condición: {condicion}", condicion);
                var Seguros = _segurosRepository.GetAllSegurosAsync().Result;
                var SegurosList = Seguros.Where(e => !((SegurosEntity)e).IsDeleted).ToList();
                if (!string.IsNullOrWhiteSpace(condicion))
                {
                    SegurosList = SegurosList.Where(e =>
                    {
                        var entity = (SegurosEntity)e;
                        return (entity.Name != null && entity.Name.Contains(condicion, StringComparison.OrdinalIgnoreCase));
                    }).ToList();
                }
                SegurosDto = _mapper.Map<SegurosDto[]>(SegurosList);
                _logger.LogInformation("Seguros obtenidos con condición: {Count}", SegurosDto.Length);
                return Task.FromResult<IEnumerable<SegurosDto>>(SegurosDto);
            }
            catch (Exception)
            {
                _logger.LogError("Error al obtener los Seguros con condición: {condicion}", condicion);
                throw;
            }
        }

        public async Task<SegurosDto> UpdateSegurosAsync(UpdateSegurosDto updateSegurosDto)
        {
            try
            {
                _logger.LogInformation("Iniciando actualización de Seguros con ID: {SegurosId}", updateSegurosDto.insuranceid);
                if (updateSegurosDto.insuranceid <= 0)
                {
                    throw new ArgumentException("El ID del Seguros debe ser mayor a 0", nameof(updateSegurosDto.insuranceid));
                }
               
                var SegurosExist = await _segurosRepository.GetSegurosByIdAsync(updateSegurosDto.insuranceid);
                if (SegurosExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Seguros con ID: {updateSegurosDto.insuranceid}");
                }
                var SegurosExistente = (SegurosEntity)SegurosExist;
                if (SegurosExistente.IsDeleted)
                {
                    throw new InvalidOperationException($"No se puede actualizar el Seguros con ID: {updateSegurosDto.insuranceid} por que está eliminado lógicamente.");
                }
                var SegurosToUpdate = _mapper.Map<SegurosEntity>(updateSegurosDto);
                SegurosToUpdate.CreatedAt = SegurosExistente.CreatedAt;
                SegurosToUpdate.CreatedBy = SegurosExistente.CreatedBy;
                SegurosToUpdate.IsDeleted = false;
				SegurosToUpdate.UpdatedAt = DateTime.Now;


				var result = await _segurosRepository.UpdateSegurosAsync(SegurosToUpdate);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo actualizar el Seguros con ID: {updateSegurosDto.insuranceid}");
                }
                var SegurosActualizado = await _segurosRepository.GetSegurosByIdAsync(updateSegurosDto.insuranceid);
                if (SegurosActualizado == null)
                {
                    throw new Exception("Error al recuperar el Seguros recién actualizado.");
                }
                var SegurosEntity2 = (SegurosEntity)SegurosActualizado;
                var SegurosDto = _mapper.Map<SegurosDto>(SegurosEntity2);
                return SegurosDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el Seguros con ID: {SegurosId}", updateSegurosDto.insuranceid);
                throw;
            }
        }
    }
}
