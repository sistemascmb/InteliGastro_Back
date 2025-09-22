using WebApi.Application.DTO.Suministros;

namespace WebApi.Application.Services.Suministros
{
    public interface ISuministrosService
    {
        Task<SuministrosDto> CreateSuministrosAsync(CreateSuministrosDto createSuministrosDto);
        Task<SuministrosDto> GetSuministrosByIdAsync(long suministrosId);
        Task<SuministrosDto> UpdateSuministrosAsync(UpdateSuministrosDto updateSuministrosDto);
        Task<bool> DeleteSuministrosAsync(long suministrosId, string eliminadoPor);
        Task<IEnumerable<SuministrosDto>> GetWhereAsync(string condicion);
        Task<IEnumerable<SuministrosDto>> GetAllSuministrosAsync();
    }
}
