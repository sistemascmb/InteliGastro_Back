using AutoMapper;
using Domain.DomainInterfaces;
using Infraestructure.Models;
using Infraestructure.Repositories;
using WebApi.Application.DTO.Recursos;
using WebApi.Application.DTO.SystemUsers;
using WebApi.Application.Exceptions;

namespace WebApi.Application.Services.SystemUsersService
{
    public class SystemUsersService : ISystemUsersService
    {
        private readonly ISystemUsersRepository _systemUsersRepository;
        private readonly IRolesRepository _rolesRepository;
        private readonly ILogger<SystemUsersService> _logger;
        private readonly IMapper _mapper;

        public SystemUsersService(ISystemUsersRepository systemUsersRepository, IRolesRepository rolesRepository, ILogger<SystemUsersService> logger, IMapper mapper)
        {
            _systemUsersRepository = systemUsersRepository ?? throw new ArgumentNullException(nameof(systemUsersRepository));
            _rolesRepository = rolesRepository ?? throw new ArgumentNullException(nameof(rolesRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SystemUsersDto> CreateSystemUsersAsync(CreateSystemUsersDto createSystemUsersDto)
        {
            _logger.LogInformation("Iniciando creación de SystemUser [CRUD AUTOMÀTICO]");

            // Usar AutoMapper para convertir CreatePersonaDto a PersonaEntity
            var systemUserEntity = _mapper.Map<SystemUsersEntity>(createSystemUsersDto);

            systemUserEntity.CreatedAt = DateTime.Now;
            systemUserEntity.IsDeleted = false;

            var systemUserId = _systemUsersRepository.CreateSystemUsersAsync(systemUserEntity);

            _logger.LogInformation("SystemUser creado exitosamente con ID: {SystemUserId}", systemUserId);

            //obtener SystemUser creado
            var systemUsercreado = await _systemUsersRepository.GetSystemUsersByIdAsync(await systemUserId);
            if (systemUsercreado == null)
            {
                throw new InvalidOperationException($"No se puedo obtener el SystemUserCreado con ID: {systemUserId}");
            }
            var systemUserEntity2= (SystemUsersEntity)systemUsercreado;
            var systemUserDto = _mapper.Map<SystemUsersDto>(systemUserEntity2);
            return systemUserDto;
        }

        public async Task<bool> DeleteSystemUsersAsync(long UserId, string eliminadoPor)
        {
            _logger.LogInformation("Iniciando eliminación lógica de SystemUser ID: {UserId} [CRUD AUTOMÁTICO]", UserId);
            //validador
            if (UserId <= 0)
            {
                throw new ArgumentException("El ID de la persona debe ser mayor a 0", nameof(UserId));
            }

            //verificar si existe system_users
            var systemUserExist = _systemUsersRepository.GetSystemUsersByIdAsync(UserId).Result;    
            var systemUserExistente = (SystemUsersEntity)systemUserExist;
            if (systemUserExist == null)
            {
                throw new KeyNotFoundException($"No se encontró el SystemUsers con ID: {UserId}");
            }

            //verificarmos que no estè eliminado lògicamente
            if (systemUserExistente.IsDeleted)
            {
                throw new InvalidOperationException($"No se puede eliminar el SystemUsers con ID: {UserId} porque está eliminado lógicamente.");
            }

            //usar crud del repositorio
            systemUserExistente.IsDeleted = true;
            systemUserExistente.UpdatedAt = DateTime.Now;
            systemUserExistente.UpdatedBy = eliminadoPor;

            //if (eliminadoPor.HasValue)
            //{
            //    systemUserExistente.UpdatedBy = eliminadoPor.Value;
            //}

            var eliminacionExitosa = await _systemUsersRepository.UpdateSystemUsersAsync(systemUserExistente);

            if (!eliminacionExitosa)
            {
                throw new InvalidOperationException($"No se pudo eliminar lógicamente el SystemUsers con ID: {UserId}");
            }

            _logger.LogInformation("SystemUsers con ID: {UserId} eliminado lógicamente exitosamente.", UserId);

            return true;
        }

        public async Task<SystemUsersDto> GetSystemUsersByIdAsync(long UserId)
        {
            try
            {
                _logger.LogInformation("Iniciando obtención de SystemUser por ID: {UserId} [CRUD AUTOMÀTICO]", UserId);
                //VALIDAR ID
                if (UserId <= 0)
                {
                    throw new ArgumentException("El ID de la persona debe ser mayor a 0", nameof(UserId));
                }
                //Usar CRUD al repositorio
                var systeumUserResult = await _systemUsersRepository.GetSystemUsersByIdAsync(UserId);

                if (systeumUserResult == null)
                {
                    throw new KeyNotFoundException($"No se encontro el SystemUser con el Id {UserId}.");
                }

                //Convierte objeto a entidad y luego usa AutoMapper
                var personaEntity = (SystemUsersEntity)systeumUserResult;
                var systemUsersDto = _mapper.Map<SystemUsersDto>(personaEntity);

                _logger.LogInformation("SystemUser con ID: {UserId} obtenido exitosamente.", UserId);

                return systemUsersDto;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<SystemUsersDto> GetSystemUsersCompletaAsync(long UserId)
        {
            throw new NotImplementedException();
        }

        public async Task<SystemUsersDto> UpdateSystemUsersAsync(UpdateSystemUsersDto updateSystemUsersDto)
        {
            try
            {
                _logger.LogInformation("Iniciando actualización de SystemUser  ID: {PersonaId} [CRUD AUTOMÁTICO]", updateSystemUsersDto.UserId);

                // Validar DataAnnotations del DTO
                //ValidateDataAnnotations(updatePersonaDto);

                // Normalizar datos
                //updatePersonaDto.Normalize();

                // Validar reglas de negocio específicas
                //var validationErrors = updatePersonaDto.GetValidationErrors();
                //if (validationErrors.Any())
                //{
                //    var errorMessage = string.Join("; ", validationErrors);
                //    _logger.LogWarning("Errores de validación de reglas de negocio: {Errors}", errorMessage);
                //    throw new ArgumentException(errorMessage);
                //}

                //verificar si existe system_users
                var systemUserExist = _systemUsersRepository.GetSystemUsersByIdAsync(updateSystemUsersDto.UserId).Result;
                if (systemUserExist == null)
                {
                    throw new KeyNotFoundException($"No se encontró el SystemUsers con ID: {updateSystemUsersDto.UserId}");
                }

                var systemUserExistente = (SystemUsersEntity)systemUserExist;

                //verificarmos que no estè eliminado lògicamente
                if (systemUserExistente.IsDeleted)
                {
                    throw new InvalidOperationException($"No se puede actualizar el SystemUsers con ID: {updateSystemUsersDto.UserId} porque está eliminado lógicamente.");
                }

                var systemUserEntity = _mapper.Map<SystemUsersEntity>(updateSystemUsersDto);
                systemUserEntity.CreatedAt = systemUserExistente.CreatedAt;
                systemUserEntity.CreatedBy = systemUserExistente.CreatedBy;
                systemUserEntity.IsDeleted = false;
                systemUserEntity.UpdatedAt = DateTime.Now;

                //systemUserEntity.UpdatedAt = DateTime.Now;
                //systemUserEntity.UpdatedBy = systemUserExistente.UpdatedBy; //suponiendo que viene en el dto

                //usar crud del repositorio
                var actualizacionExitosa = await _systemUsersRepository.UpdateSystemUsersAsync(systemUserEntity);

                if (!actualizacionExitosa)
                {
                    throw new InvalidOperationException($"No se pudo actualizar el SystemUsers con ID: {updateSystemUsersDto.UserId}");
                }

                _logger.LogInformation("SystemUsers con ID: {UserId} actualizado exitosamente.", updateSystemUsersDto.UserId);

                //obtener SystemUsers actualizado
                var systemUserActualizado = await _systemUsersRepository.GetSystemUsersByIdAsync(updateSystemUsersDto.UserId);
                if (systemUserActualizado == null)
                {
                    throw new InvalidOperationException($"No se puedo obtener el SystemUsers actualizado con ID: {updateSystemUsersDto.UserId}");
                }

                var systemUserEntity2 = (SystemUsersEntity)systemUserActualizado;
                var systemUserDto = _mapper.Map<SystemUsersDto>(systemUserEntity2);

                return systemUserDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar SystemUsers ID: {UserId}", updateSystemUsersDto.UserId);
                throw;
            }
        }

        public Task<IEnumerable<SystemUsersDto>> GetWhereAsync(string condicion)
        {
            SystemUsersDto[] systemUsersDtos;
            try
            {
                _logger.LogInformation("Iniciando obtención de recursos con condición: {condicion}", condicion);
                var systemUsers = _systemUsersRepository.GetAllSystemUsersAsync().Result;
                var systemUsersList = systemUsers.Where(s => !((SystemUsersEntity)s).IsDeleted).ToList();
                if (!string.IsNullOrWhiteSpace(condicion))
                {
                    systemUsersList = systemUsersList.Where(s =>
                    {
                        var entity = (SystemUsersEntity)s;
                        return (entity.Usuario != null && entity.Usuario.Contains(condicion, StringComparison.OrdinalIgnoreCase));
                    }).ToList();
                }
                systemUsersDtos = _mapper.Map<SystemUsersDto[]>(systemUsersList);
                _logger.LogInformation("Recursos recuperados con condición: {Count}", systemUsersDtos.Length);
                return Task.FromResult((IEnumerable<SystemUsersDto>)systemUsersDtos);
            }
            catch (Exception)
            {
                _logger.LogError("Error al recuperar los recursos con condición: {Condicion}", condicion);
                throw;
            }
        }

        public async Task<IEnumerable<SystemUsersDto>> GetAllSystemUsersAsync()
        {
            Task<IEnumerable<SystemUsersDto>> systemUsersDtos;
            try
            {
                _logger.LogInformation("Iniciando obtención de todos los SystemUsers.");
                var systemUsers = await _systemUsersRepository.GetAllSystemUsersAsync();
                var systemUsersList = systemUsers.Where(s => !((SystemUsersEntity)s).IsDeleted).ToList();
                systemUsersDtos = Task.FromResult(_mapper.Map<IEnumerable<SystemUsersDto>>(systemUsersList));
                _logger.LogInformation("SystemUsers obtenidos: {Count}", systemUsersList.Count);
                return await systemUsersDtos;
            }
            catch (Exception)
            {
                _logger.LogError("Error al obtener los SystemUsers.");
                throw;
            }
        }

        public async Task<SystemUsersLoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            try
            {
                if (loginRequestDto == null)
                {
                    throw new ArgumentNullException(nameof(loginRequestDto));
                }

                _logger.LogInformation("Iniciando login para usuario: {Usuario}", loginRequestDto.Usuario);

                if (string.IsNullOrWhiteSpace(loginRequestDto.Usuario))
                {
                    throw new ArgumentException("El campo 'usuario' es obligatorio.");
                }
                if (string.IsNullOrWhiteSpace(loginRequestDto.Contraseña))
                {
                    throw new ArgumentException("El campo 'contraseña' es obligatorio.");
                }

                var userObj = await _systemUsersRepository.GetByCredentialsAsync(loginRequestDto.Usuario, loginRequestDto.Contraseña);
                if (userObj == null)
                {
                    throw new UnauthorizedAccessException("Credenciales inválidas.");
                }

                var userEntity = (SystemUsersEntity)userObj;

                if (userEntity.IsDeleted || !userEntity.Estado)
                {
                    throw new ForbiddenException("Usuario inactivo o eliminado.");
                }

                long profileId = userEntity.profiletypeid ?? 0;
                string profileName = string.Empty;

                if (profileId > 0)
                {
                    var roleObj = await _rolesRepository.GetRolesByIdAsync(profileId);
                    if (roleObj == null)
                    {
                        throw new KeyNotFoundException($"No se encontró el rol con ID: {profileId}.");
                    }

                    var roleEntity = (RolesEntity)roleObj;
                    if (roleEntity.IsDeleted)
                    {
                        throw new InvalidOperationException($"El rol con ID: {profileId} está eliminado lógicamente.");
                    }

                    profileName = roleEntity.profile_name;
                }
                else
                {
                    profileName = "Sin rol";
                }

                var response = new SystemUsersLoginResponseDto
                {
                    userid = userEntity.userid,
                    Usuario = userEntity.Usuario,
                    profiletypeid = profileId,
                    profile_name = profileName
                };

                _logger.LogInformation("Login exitoso para usuario: {Usuario}", loginRequestDto.Usuario);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en login para usuario: {Usuario}", loginRequestDto?.Usuario);
                throw;
            }
        }

    }
}
