using AutoMapper;
using Domain.DomainInterfaces;
using Infraestructure.Models;
using Infraestructure.Repositories;
using WebApi.Application.DTO.Roles;
using WebApi.Application.DTO.Roles;
using WebApi.Application.Services.Roles;

namespace WebApi.Application.Services.Roles
{
    public class RolesService : IRolesService
    {
        private readonly ILogger<RolesService> _logger;
        private readonly IRolesRepository _rolesRepository;
        private readonly IMapper _mapper;
        public RolesService(
            ILogger<RolesService> logger,
            IRolesRepository rolesRepository,
            ICentroRepository centroRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _rolesRepository = rolesRepository ?? throw new ArgumentNullException(nameof(rolesRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<RolesDto> CreateRolesAsync(CreateRolesDto createRolesDto)
        {
            try
            {
                _logger.LogInformation("Iniciando creación de Roles");
                var RolesEntity = _mapper.Map<RolesEntity>(createRolesDto);
                RolesEntity.IsDeleted = false;
                RolesEntity.CreatedAt = DateTime.UtcNow;
                var newRolesId = await _rolesRepository.CreateRolesAsync(RolesEntity);
                _logger.LogInformation("Roles creado con ID: {RolesId}", newRolesId);
                var newRoles = await _rolesRepository.GetRolesByIdAsync(newRolesId);
                if (newRoles == null)
                {
                    throw new Exception("Error al recuperar el roles recién creado.");
                }

                var newRolesDto = _mapper.Map<RolesEntity>(newRoles);
                var RolesDto = _mapper.Map<RolesDto>(newRoles);
                return RolesDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear Roles");
                throw;
            }
        }

        public async Task<bool> DeleteRolesAsync(long RolesId, string eliminadoPor)
        {
            try
            {
                _logger.LogInformation("Iniciando eliminación lógica de Rol con ID: {RolesId}", RolesId);
                if (RolesId <= 0)
                {
                    throw new ArgumentException("El ID del roles debe ser mayor a 0", nameof(RolesId));
                }
                var RolesExist = await _rolesRepository.GetRolesByIdAsync(RolesId);
                var RolesExistente = (RolesEntity)RolesExist;
                if (RolesExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el roles con ID: {RolesId}");
                }
                if (RolesExistente.IsDeleted)
                {
                    throw new InvalidOperationException($"No se puede eliminar el Rol con ID: {RolesId} por que ya está eiminado lógicamente.");
                }
                RolesExistente.IsDeleted = true;
                RolesExistente.UpdatedAt = DateTime.UtcNow;
                RolesExistente.UpdatedBy = eliminadoPor;

                var result = await _rolesRepository.UpdateRolesAsync(RolesExistente);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo eliminar el Rol con ID: {RolesId}");
                }
                _logger.LogInformation("Rol con ID: {RolesId} eliminado lógicamente", RolesId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el Rol con ID: {RolesId}", RolesId);
                throw;
            }
        }

        public async Task<IEnumerable<RolesDto>> GetAllRolesAsync()
        {
            Task<IEnumerable<RolesDto>> RolesDto;
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de todos los Roles");
                var Roles = _rolesRepository.GetAllRolesAsync().Result;
                var RolesList = Roles.Where(e => !((RolesEntity)e).IsDeleted).ToList();
                RolesDto = Task.FromResult(_mapper.Map<IEnumerable<RolesDto>>(RolesList));
                _logger.LogInformation("Roles obtenidos: {Count}", RolesList.Count());
                return await RolesDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los Roles");
                throw;
            }
        }

        public async Task<RolesDto> GetRolesByIdAsync(long RolesId)
        {
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de Rol por ID: {RolesId}", RolesId);
                if (RolesId <= 0)
                {
                    throw new ArgumentException("El ID del roles debe ser mayor a 0", nameof(RolesId));
                }

                var Roles = await _rolesRepository.GetRolesByIdAsync(RolesId);
                if (Roles == null)
                {
                    throw new KeyNotFoundException($"No se encontró el roles con ID: {RolesId}");
                }
                var RolesEntity = (RolesEntity)Roles;
                if (RolesEntity.IsDeleted)
                {
                    throw new InvalidOperationException($"El roles con ID: {RolesId} está eliminado lógicamente.");
                }
                var RolesDto = _mapper.Map<RolesDto>(RolesEntity);
                _logger.LogInformation("Rol obtenido con ID: {RolesId}", RolesId);
                return RolesDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<IEnumerable<RolesDto>> GetWhereAsync(string condicion)
        {
            RolesDto[] RolesDto;
            try
            {
                _logger.LogInformation("Iniciando obtenciòn de Roles con condición: {condicion}", condicion);
                var Roles = _rolesRepository.GetAllRolesAsync().Result;
                var RolesList = Roles.Where(e => !((RolesEntity)e).IsDeleted).ToList();
                if (!string.IsNullOrWhiteSpace(condicion))
                {
                    RolesList = RolesList.Where(e =>
                    {
                        var entity = (RolesEntity)e;
                        return (entity.profile_name != null && entity.profile_name.Contains(condicion, StringComparison.OrdinalIgnoreCase)) ||
                               (entity.description != null && entity.description.Contains(condicion, StringComparison.OrdinalIgnoreCase));
                    }).ToList();
                }
                RolesDto = _mapper.Map<RolesDto[]>(RolesList);
                _logger.LogInformation("Roles obtenidos con condición: {Count}", RolesDto.Length);
                return Task.FromResult<IEnumerable<RolesDto>>(RolesDto);
            }
            catch (Exception)
            {
                _logger.LogError("Error al obtener los Roles con condición: {condicion}", condicion);
                throw;
            }
        }

        public async Task<RolesDto> UpdateRolesAsync(UpdateRolesDto updateRolesDto)
        {
            try
            {
                _logger.LogInformation("Iniciando actualización de Rol con ID: {RolesId}", updateRolesDto.profiletypeid);
                if (updateRolesDto.profiletypeid <= 0)
                {
                    throw new ArgumentException("El ID del roles debe ser mayor a 0", nameof(updateRolesDto.profiletypeid));
                }
                
                var RolesExist = await _rolesRepository.GetRolesByIdAsync(updateRolesDto.profiletypeid);
                if (RolesExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el Rol con ID: {updateRolesDto.profiletypeid}");
                }
                var RolesExistente = (RolesEntity)RolesExist;
                if (RolesExistente.IsDeleted)
                {
                    throw new InvalidOperationException($"No se puede actualizar el Rol con ID: {updateRolesDto.profiletypeid} por que está eliminado lógicamente.");
                }
                var RolesToUpdate = _mapper.Map<RolesEntity>(updateRolesDto);
                RolesToUpdate.CreatedAt = RolesExistente.CreatedAt;
                RolesToUpdate.CreatedBy = RolesExistente.CreatedBy;
                RolesToUpdate.IsDeleted = false;
                var result = await _rolesRepository.UpdateRolesAsync(RolesToUpdate);
                if (!result)
                {
                    throw new InvalidOperationException($"No se pudo actualizar el Rol con ID: {updateRolesDto.profiletypeid}");
                }
                var RolesActualizado = await _rolesRepository.GetRolesByIdAsync(updateRolesDto.profiletypeid);
                if (RolesActualizado == null)
                {
                    throw new Exception("Error al recuperar el roles recién actualizado.");
                }
                var estudioEntity2 = (RolesEntity)RolesActualizado;
                var RolesDto = _mapper.Map<RolesDto>(estudioEntity2);
                return RolesDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el Rol con ID: {RolesId}", updateRolesDto.profiletypeid);
                throw;
            }
        }
    }
}
