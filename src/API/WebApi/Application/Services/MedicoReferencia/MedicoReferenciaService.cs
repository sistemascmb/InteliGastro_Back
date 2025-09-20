using AutoMapper;
using Domain.DomainInterfaces;
using Infraestructure.Models;
using Infraestructure.Repositories;
using WebApi.Application.DTO.MedicoReferencia;
using WebApi.Application.Services.MedicoReferencia;

namespace WebApi.Application.Services.MedicoReferencia
{
    public class MedicoReferenciaService : IMedicoReferenciaService
    {
        private readonly ILogger<MedicoReferenciaService> _logger;
        private readonly IMedicoReferenciaRepository _medicoReferenciaRepository;
        private readonly IMapper _mapper;

        public MedicoReferenciaService(
            ILogger<MedicoReferenciaService> logger,
            IMedicoReferenciaRepository medicoReferenciaRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _medicoReferenciaRepository = medicoReferenciaRepository ?? throw new ArgumentNullException(nameof(medicoReferenciaRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<MedicoReferenciaDto> CreateMedicoReferenciaAsync(CreateMedicoReferenciaDto createMedicoReferenciaDto)
        {
            try
            {
                _logger.LogInformation("Iniciando creación de MedicoReferencia");
                
                var MedicoReferenciaEntity = _mapper.Map<MedicoReferenciaEntity>(createMedicoReferenciaDto);
                MedicoReferenciaEntity.IsDeleted = false;
                MedicoReferenciaEntity.CreatedAt = DateTime.UtcNow;
                var newMedicoReferenciaId = await _medicoReferenciaRepository.CreateMedicoReferenciaAsync(MedicoReferenciaEntity);
                _logger.LogInformation("MedicoReferencia creado con ID: {MedicoReferenciaId}", newMedicoReferenciaId);
                var newMedicoReferencia = await _medicoReferenciaRepository.GetMedicoReferenciaByIdAsync(newMedicoReferenciaId);
                if (newMedicoReferencia == null)
                {
                    throw new Exception("Error al recuperar el MedicoReferencia recién creado.");
                }

                var newMedicoReferenciaDto = _mapper.Map<MedicoReferenciaEntity>(newMedicoReferencia);
                var MedicoReferenciaDto = _mapper.Map<MedicoReferenciaDto>(newMedicoReferencia);
                return MedicoReferenciaDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear MedicoReferencia");
                throw;
            }
        }

        public async Task<bool> DeleteMedicoReferenciaAsync(long MedicoReferenciaId, string eliminadoPor)
        {
            try
            {
                _logger.LogInformation("Iniciando eliminación lógica de MedicoReferencia con ID: {MedicoReferenciaId}", MedicoReferenciaId);
                if (MedicoReferenciaId <= 0)
                {
                    throw new ArgumentException("El ID del MedicoReferencia debe ser mayor a 0", nameof(MedicoReferenciaId));
                }
                var MedicoReferenciaExist = await _medicoReferenciaRepository.GetMedicoReferenciaByIdAsync(MedicoReferenciaId);
                var MedicoReferenciaExistente = (MedicoReferenciaEntity)MedicoReferenciaExist;
                if (MedicoReferenciaExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el MedicoReferencia con ID: {MedicoReferenciaId}");
                }
                if (MedicoReferenciaExistente.IsDeleted)
                {
                    throw new InvalidOperationException($"No se puede eliminar el MedicoReferencia con ID: {MedicoReferenciaId} por que ya está eiminado lógicamente.");
                }
                MedicoReferenciaExistente.IsDeleted = true;
                MedicoReferenciaExistente.UpdatedAt = DateTime.UtcNow;
                MedicoReferenciaExistente.UpdatedBy = eliminadoPor;

                var result = await _medicoReferenciaRepository.UpdateMedicoReferenciaAsync(MedicoReferenciaExistente);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo eliminar el MedicoReferencia con ID: {MedicoReferenciaId}");
                }
                _logger.LogInformation("MedicoReferencia con ID: {MedicoReferenciaId} eliminado lógicamente", MedicoReferenciaId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el MedicoReferencia con ID: {MedicoReferenciaId}", MedicoReferenciaId);
                throw;
            }
        }

        public async Task<IEnumerable<MedicoReferenciaDto>> GetAllMedicoReferenciaAsync()
        {
            Task<IEnumerable<MedicoReferenciaDto>> MedicoReferenciaDto;
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de todos los MedicoReferencia");
                var MedicoReferencia = _medicoReferenciaRepository.GetAllMedicoReferenciaAsync().Result;
                var MedicoReferenciaList = MedicoReferencia.Where(e => !((MedicoReferenciaEntity)e).IsDeleted).ToList();
                MedicoReferenciaDto = Task.FromResult(_mapper.Map<IEnumerable<MedicoReferenciaDto>>(MedicoReferenciaList));
                _logger.LogInformation("MedicoReferencia obtenidos: {Count}", MedicoReferenciaList.Count());
                return await MedicoReferenciaDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los MedicoReferencia");
                throw;
            }
        }

        public async Task<MedicoReferenciaDto> GetMedicoReferenciaByIdAsync(long MedicoReferenciaId)
        {
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de MedicoReferencia por ID: {MedicoReferenciaId}", MedicoReferenciaId);
                if (MedicoReferenciaId <= 0)
                {
                    throw new ArgumentException("El ID del MedicoReferencia debe ser mayor a 0", nameof(MedicoReferenciaId));
                }

                var MedicoReferencia = await _medicoReferenciaRepository.GetMedicoReferenciaByIdAsync(MedicoReferenciaId);
                if (MedicoReferencia == null)
                {
                    throw new KeyNotFoundException($"No se encontró el MedicoReferencia con ID: {MedicoReferenciaId}");
                }
                var MedicoReferenciaEntity = (MedicoReferenciaEntity)MedicoReferencia;
                if (MedicoReferenciaEntity.IsDeleted)
                {
                    throw new InvalidOperationException($"El MedicoReferencia con ID: {MedicoReferenciaId} está eliminado lógicamente.");
                }
                var MedicoReferenciaDto = _mapper.Map<MedicoReferenciaDto>(MedicoReferenciaEntity);
                _logger.LogInformation("MedicoReferencia obtenido con ID: {MedicoReferenciaId}", MedicoReferenciaId);
                return MedicoReferenciaDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<IEnumerable<MedicoReferenciaDto>> GetWhereAsync(string condicion)
        {
            MedicoReferenciaDto[] MedicoReferenciaDto;
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de MedicoReferencia con condición: {condicion}", condicion);
                var MedicoReferencia = _medicoReferenciaRepository.GetAllMedicoReferenciaAsync().Result;
                var MedicoReferenciaList = MedicoReferencia.Where(e => !((MedicoReferenciaEntity)e).IsDeleted).ToList();
                if (!string.IsNullOrWhiteSpace(condicion))
                {
                    MedicoReferenciaList = MedicoReferenciaList.Where(e =>
                    {
                        var entity = (MedicoReferenciaEntity)e;
                        return (entity.Names != null && entity.Names.Contains(condicion, StringComparison.OrdinalIgnoreCase)) ||
                               (entity.Surnames != null && entity.Surnames.Contains(condicion, StringComparison.OrdinalIgnoreCase));
                    }).ToList();
                }
                MedicoReferenciaDto = _mapper.Map<MedicoReferenciaDto[]>(MedicoReferenciaList);
                _logger.LogInformation("MedicoReferencia obtenidos con condición: {Count}", MedicoReferenciaDto.Length);
                return Task.FromResult<IEnumerable<MedicoReferenciaDto>>(MedicoReferenciaDto);
            }
            catch (Exception)
            {
                _logger.LogError("Error al obtener los MedicoReferencia con condición: {condicion}", condicion);
                throw;
            }
        }

        public async Task<MedicoReferenciaDto> UpdateMedicoReferenciaAsync(UpdateMedicoReferenciaDto updateMedicoReferenciaDto)
        {
            try
            {
                _logger.LogInformation("Iniciando actualización de MedicoReferencia con ID: {MedicoReferenciaId}", updateMedicoReferenciaDto.referraldoctorsd);
                if (updateMedicoReferenciaDto.referraldoctorsd <= 0)
                {
                    throw new ArgumentException("El ID del MedicoReferencia debe ser mayor a 0", nameof(updateMedicoReferenciaDto.referraldoctorsd));
                }
                
                var MedicoReferenciaExist = await _medicoReferenciaRepository.GetMedicoReferenciaByIdAsync(updateMedicoReferenciaDto.referraldoctorsd);
                if (MedicoReferenciaExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el MedicoReferencia con ID: {updateMedicoReferenciaDto.referraldoctorsd}");
                }
                var MedicoReferenciaExistente = (MedicoReferenciaEntity)MedicoReferenciaExist;
                if (MedicoReferenciaExistente.IsDeleted)
                {
                    throw new InvalidOperationException($"No se puede actualizar el MedicoReferencia con ID: {updateMedicoReferenciaDto.referraldoctorsd} por que está eliminado lógicamente.");
                }
                var MedicoReferenciaToUpdate = _mapper.Map<MedicoReferenciaEntity>(updateMedicoReferenciaDto);
                MedicoReferenciaToUpdate.CreatedAt = MedicoReferenciaExistente.CreatedAt;
                MedicoReferenciaToUpdate.CreatedBy = MedicoReferenciaExistente.CreatedBy;
                MedicoReferenciaToUpdate.IsDeleted = false;
                var result = await _medicoReferenciaRepository.UpdateMedicoReferenciaAsync(MedicoReferenciaToUpdate);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo actualizar el MedicoReferencia con ID: {updateMedicoReferenciaDto.referraldoctorsd}");
                }
                var MedicoReferenciaActualizado = await _medicoReferenciaRepository.GetMedicoReferenciaByIdAsync(updateMedicoReferenciaDto.referraldoctorsd);
                if (MedicoReferenciaActualizado == null)
                {
                    throw new Exception("Error al recuperar el MedicoReferencia recién actualizado.");
                }
                var estudioEntity2 = (MedicoReferenciaEntity)MedicoReferenciaActualizado;
                var MedicoReferenciaDto = _mapper.Map<MedicoReferenciaDto>(estudioEntity2);
                return MedicoReferenciaDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el MedicoReferencia con ID: {MedicoReferenciaId}", updateMedicoReferenciaDto.referraldoctorsd);
                throw;
            }
        }
    }
}
