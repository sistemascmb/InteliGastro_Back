using WebApi.Application.DTO.AgendaDx;
using WebApi.Application.DTO.ArchivoDigital;

namespace WebApi.Application.Services.AgendaDx
{
    public interface IAgendaDxService
    {
        Task<AgendaDxDto> CreateAgendaDxAsync(CreateAgendaDx createAgendaDxDto);
        Task<AgendaDxDto> GetAgendaDxByIdAsync(long AgendaDxId);
        Task<AgendaDxDto> UpdateAgendaDxAsync(UpdateAgendaDx updateAgendaDxDto);
        Task<bool> DeleteAgendaDxAsync(long AgendaDxId, string eliminadoPor);
        Task<IEnumerable<AgendaDxDto>> GetWhereAsync(string condicion);
        Task<IEnumerable<AgendaDxDto>> GetAllAgendaDxAsync();
        Task<IEnumerable<AgendaDxDto>> SearchAgendaDxAsync(string? value1);

    }
}
