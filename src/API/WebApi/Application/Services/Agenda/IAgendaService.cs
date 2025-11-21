using WebApi.Application.DTO.Agenda;

namespace WebApi.Application.Services.Agenda
{
    public interface IAgendaService
    {
        Task<AgendaDto> CreateAgendaAsync(CreateAgendaDto createAgendaDto);
        Task<AgendaDto> GetAgendaByIdAsync(long agendaId);
        Task<AgendaDto> UpdateAgendaAsync(UpdateAgendaDto updateAgendaDto);
        Task<bool> DeleteAgendaAsync(long agendaId, string eliminadoPor);
        Task<IEnumerable<AgendaDto>> GetWhereAsync(string condicion);
        Task<IEnumerable<AgendaDto>> GetAllAgendaAsync();
        Task<IEnumerable<AgendaDto>> SearchAgendaByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<AgendaSearchResultDto>> SearchAgendaStudiesAsync(long? medicalscheduleid, string? names, string? lastNames, string? dni);
    }
}
