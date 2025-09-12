using AutoMapper;
using Domain.DomainInterfaces;
using Infraestructure.Models;
using WebApi.Application.DTO.SystemUsers;

namespace WebApi.Application.Services
{
    public class SystemUsersService : ISystemUsersService
    {
        private readonly ISystemUsersRepository _systemUsersRepository;
        private readonly ILogger<SystemUsersService> _logger;
        private readonly IMapper _mapper;

        public SystemUsersService(ISystemUsersRepository systemUsersRepository, ILogger<SystemUsersService> logger, IMapper mapper)
        {
            _systemUsersRepository = systemUsersRepository ?? throw new ArgumentNullException(nameof(systemUsersRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SystemUsersDto> CreateSystemUsersAsync(CreateSystemUsersDto createSystemUsersDto)
        {
            _logger.LogInformation("Iniciando creación de SystemUser [CRUD AUTOMÀTICO]");

            // Usar AutoMapper para convertir CreatePersonaDto a PersonaEntity
            var systemUserEntity = _mapper.Map<SystemUsersEntity>(createSystemUsersDto);

            systemUserEntity.FechaCreacion = DateTime.UtcNow;
            systemUserEntity.CreatedAt = DateTime.UtcNow;
            systemUserEntity.RegistroEliminado = false;

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

        public Task<bool> DeleteSystemUsersAsync(long personaId, long? eliminadoPor = null)
        {
            throw new NotImplementedException();
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

        public Task<SystemUsersDto> UpdateSystemUsersAsync(UpdateSystemUsersDto updateSystemUsersDto)
        {
            throw new NotImplementedException();
        }
    }
}
