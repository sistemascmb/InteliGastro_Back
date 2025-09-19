using WebApi.Application.DTO.Roles;

namespace WebApi.Application.Services.Roles
{
    public interface IRolesService
    {
        Task<RolesDto> CreateRolesAsync(CreateRolesDto createRolesDto);
        Task<RolesDto> GetRolesByIdAsync(long rolesId);
        Task<RolesDto> UpdateRolesAsync(UpdateRolesDto updateRolesDto);
        Task<bool> DeleteRolesAsync(long RolesId, string eliminadoPor);
        Task<IEnumerable<RolesDto>> GetWhereAsync(string condicion);
        Task<IEnumerable<RolesDto>> GetAllRolesAsync();
    }
}
