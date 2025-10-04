using WebApi.Application.DTO.SystemParameter;

namespace WebApi.Application.Services.SystemParameter
{
    public interface ISystemParameterService
    {
        Task<SystemParameterDto> CreateSystemParameterAsync(CreateSystemParameterDto createSystemParameterDto);
        Task<SystemParameterDto> GetSystemParameterByIdAsync(long id);
        Task<SystemParameterDto> UpdateSystemParameterAsync(UpdateSystemParameterDto updateSystemParameterDto);
        Task<bool> DeleteSystemParameterAsync(long SystemParameterid, string eliminadoPor);
        Task<IEnumerable<SystemParameterDto>> GetWhereAsync(string conddicion);
        Task<IEnumerable<SystemParameterDto>> GetAllSystemParameterAsync();
    }
}
