using AutoMapper;
using Domain.DomainInterfaces;
using Infraestructure.Models;
using WebApi.Application.DTO.SystemParameter;
using WebApi.Application.Services.SystemParameter;

namespace WebApi.Application.Services.SystemParameter
{
    public class SystemParameterService : ISystemParameterService
    {
        private readonly ISystemParameterRepository _SystemParameterRepository;
        private readonly ILogger<SystemParameterService> _logger;
        private readonly IMapper _mapper;
        public SystemParameterService(
            ILogger<SystemParameterService> logger,
            ISystemParameterRepository SystemParameterRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _SystemParameterRepository = SystemParameterRepository ?? throw new ArgumentNullException(nameof(SystemParameterRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SystemParameterDto> CreateSystemParameterAsync(CreateSystemParameterDto createSystemParameterDto)
        {
            try
            {
                _logger.LogInformation("Iniciando creación de SystemParameter");

                var SystemParameterEntity = _mapper.Map<SystemParameterEntity>(createSystemParameterDto);
                SystemParameterEntity.IsDeleted = false;
                SystemParameterEntity.CreatedAt = DateTime.UtcNow;
                var newSystemParameterId = await _SystemParameterRepository.CreateSystemParameterAsync(SystemParameterEntity);
                _logger.LogInformation("SystemParameter creado con ID: {SystemParameterId}", newSystemParameterId);
                //var newSystemParameter = await _SystemParameterRepository.GetSystemParameterByIdAsync(newSystemParameterId);
                //if (newSystemParameter == null)
                //{
                //    throw new Exception("Error al recuperar el SystemParameter recién creado.");
                //}

                var newSystemParameterDto = _mapper.Map<SystemParameterEntity>(createSystemParameterDto);
                var SystemParameterDto = _mapper.Map<SystemParameterDto>(newSystemParameterDto);
                return SystemParameterDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear SystemParameter");
                throw;
            }
        }

        public async Task<bool> DeleteSystemParameterAsync(long SystemParameterId, string eliminadoPor)
        {
            try
            {
                _logger.LogInformation("Iniciando eliminación lógica de SystemParameter con ID: {SystemParameterId}", SystemParameterId);
               
                var SystemParameterExist = await _SystemParameterRepository.GetSystemParameterByIdAsync(SystemParameterId);
                var SystemParameterExistente = (SystemParameterEntity)SystemParameterExist;
                if (SystemParameterExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el SystemParameter con ID: {SystemParameterId}");
                }
                if (SystemParameterExistente.IsDeleted)
                {
                    throw new InvalidOperationException($"No se puede eliminar el SystemParameter con ID: {SystemParameterId} por que ya está eiminado lógicamente.");
                }
                SystemParameterExistente.IsDeleted = true;
                SystemParameterExistente.UpdatedAt = DateTime.UtcNow;
                SystemParameterExistente.UpdatedBy = eliminadoPor;

                var result = await _SystemParameterRepository.UpdateSystemParameterAsync(SystemParameterExistente);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo eliminar el SystemParameter con ID: {SystemParameterId}");
                }
                _logger.LogInformation("SystemParameter con ID: {SystemParameterId} eliminado lógicamente", SystemParameterId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el SystemParameter con ID: {SystemParameterId}", SystemParameterId);
                throw;
            }
        }

        public async Task<IEnumerable<SystemParameterDto>> GetAllSystemParameterAsync()
        {
            Task<IEnumerable<SystemParameterDto>> SystemParameterDto;
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de todos los SystemParameter");
                var SystemParameter = _SystemParameterRepository.GetAllSystemParameterAsync().Result;
                var SystemParameterList = SystemParameter.Where(e => !((SystemParameterEntity)e).IsDeleted).ToList();
                SystemParameterDto = Task.FromResult(_mapper.Map<IEnumerable<SystemParameterDto>>(SystemParameterList));
                _logger.LogInformation("SystemParameter obtenidos: {Count}", SystemParameterList.Count());
                return await SystemParameterDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los SystemParameter");
                throw;
            }
        }

        public async Task<SystemParameterDto> GetSystemParameterByIdAsync(long SystemParameterId)
        {
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de SystemParameter por ID: {SystemParameterId}", SystemParameterId);
                if (SystemParameterId <= 0)
                {
                    throw new ArgumentException("El ID del SystemParameter debe ser mayor a 0", nameof(SystemParameterId));
                }

                var SystemParameter = await _SystemParameterRepository.GetSystemParameterByIdAsync(SystemParameterId);
                if (SystemParameter == null)
                {
                    throw new KeyNotFoundException($"No se encontró el SystemParameter con ID: {SystemParameterId}");
                }
                var SystemParameterEntity = (SystemParameterEntity)SystemParameter;
                if (SystemParameterEntity.IsDeleted)
                {
                    throw new InvalidOperationException($"El SystemParameter con ID: {SystemParameterId} está eliminado lógicamente.");
                }
                var SystemParameterDto = _mapper.Map<SystemParameterDto>(SystemParameterEntity);
                _logger.LogInformation("SystemParameter obtenido con ID: {SystemParameterId}", SystemParameterId);
                return SystemParameterDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<IEnumerable<SystemParameterDto>> GetWhereAsync(string condicion)
        {
            SystemParameterDto[] SystemParameterDto;
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de SystemParameter con condición: {condicion}", condicion);
                var SystemParameter = _SystemParameterRepository.GetAllSystemParameterAsync().Result;
                var SystemParameterList = SystemParameter.Where(e => !((SystemParameterEntity)e).IsDeleted).ToList();
                if (!string.IsNullOrWhiteSpace(condicion))
                {
                    SystemParameterList = SystemParameterList.Where(e =>
                    {
                        var entity = (SystemParameterEntity)e;
                        return (entity.Value1 != null && entity.Value1.Contains(condicion, StringComparison.OrdinalIgnoreCase));
                    }).ToList();
                }
                SystemParameterDto = _mapper.Map<SystemParameterDto[]>(SystemParameterList);
                _logger.LogInformation("SystemParameter obtenidos con condición: {Count}", SystemParameterDto.Length);
                return Task.FromResult<IEnumerable<SystemParameterDto>>(SystemParameterDto);
            }
            catch (Exception)
            {
                _logger.LogError("Error al obtener los SystemParameter con condición: {condicion}", condicion);
                throw;
            }
        }

        public async Task<SystemParameterDto> UpdateSystemParameterAsync(UpdateSystemParameterDto updateSystemParameterDto)
        {
            try
            {
                _logger.LogInformation("Iniciando actualización de SystemParameter con ID: {SystemParameterId}", updateSystemParameterDto.parameterid);
                if (updateSystemParameterDto.parameterid <= 0)
                {
                    throw new ArgumentException("El ID del SystemParameter debe ser mayor a 0", nameof(updateSystemParameterDto.parameterid));
                }

                var SystemParameterExist = await _SystemParameterRepository.GetSystemParameterByIdAsync(updateSystemParameterDto.parameterid);
                if (SystemParameterExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el SystemParameter con ID: {updateSystemParameterDto.parameterid}");
                }
                var SystemParameterExistente = (SystemParameterEntity)SystemParameterExist;
                if (SystemParameterExistente.IsDeleted)
                {
                    throw new InvalidOperationException($"No se puede actualizar el SystemParameter con ID: {updateSystemParameterDto.parameterid} por que está eliminado lógicamente.");
                }
                var SystemParameterToUpdate = _mapper.Map<SystemParameterEntity>(updateSystemParameterDto);
                SystemParameterToUpdate.CreatedAt = SystemParameterExistente.CreatedAt;
                SystemParameterToUpdate.CreatedBy = SystemParameterExistente.CreatedBy;
                SystemParameterToUpdate.IsDeleted = false;
                var result = await _SystemParameterRepository.UpdateSystemParameterAsync(SystemParameterToUpdate);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo actualizar el SystemParameter con ID: {updateSystemParameterDto.parameterid}");
                }
                var SystemParameterActualizado = await _SystemParameterRepository.GetSystemParameterByIdAsync(updateSystemParameterDto.parameterid);
                if (SystemParameterActualizado == null)
                {
                    throw new Exception("Error al recuperar el SystemParameter recién actualizado.");
                }
                var SystemParameterEntity2 = (SystemParameterEntity)SystemParameterActualizado;
                var SystemParameterDto = _mapper.Map<SystemParameterDto>(SystemParameterEntity2);
                return SystemParameterDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el SystemParameter con ID: {SystemParameterId}", updateSystemParameterDto.parameterid);
                throw;
            }
        }
    }
}
