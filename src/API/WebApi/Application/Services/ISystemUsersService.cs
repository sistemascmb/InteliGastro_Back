using WebApi.Application.DTO.SystemUsers;

namespace WebApi.Application.Services
{
    public interface ISystemUsersService
    {
        Task<SystemUsersDto> CreateSystemUsersAsync(CreateSystemUsersDto createSystemUsersDto);
        Task<SystemUsersDto> GetSystemUsersByIdAsync(long UserId);
        Task<SystemUsersDto> UpdateSystemUsersAsync(UpdateSystemUsersDto updateSystemUsersDto);
        Task<bool> DeleteSystemUsersAsync(long personaId, string eliminadoPor);
        Task<SystemUsersDto> GetSystemUsersCompletaAsync(long UserId);
    }
}
