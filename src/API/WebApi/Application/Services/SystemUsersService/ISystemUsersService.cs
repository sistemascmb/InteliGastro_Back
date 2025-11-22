using WebApi.Application.DTO.Recursos;
using WebApi.Application.DTO.SystemUsers;

namespace WebApi.Application.Services.SystemUsersService
{
    public interface ISystemUsersService
    {
        Task<SystemUsersDto> CreateSystemUsersAsync(CreateSystemUsersDto createSystemUsersDto);
        Task<SystemUsersDto> GetSystemUsersByIdAsync(long UserId);
        Task<SystemUsersDto> UpdateSystemUsersAsync(UpdateSystemUsersDto updateSystemUsersDto);
        Task<bool> DeleteSystemUsersAsync(long personaId, string eliminadoPor);
        Task<SystemUsersDto> GetSystemUsersCompletaAsync(long UserId);

        Task<IEnumerable<SystemUsersDto>> GetWhereAsync(string condicion);
        Task<IEnumerable<SystemUsersDto>> GetAllSystemUsersAsync();

        // Login
        Task<SystemUsersLoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);


    }
}
