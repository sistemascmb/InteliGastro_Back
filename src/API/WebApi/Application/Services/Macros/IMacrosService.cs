using WebApi.Application.DTO.Macros;

namespace WebApi.Application.Services.Macros
{
    public interface IMacrosService
    {
        Task<MacrosDto> CreateMacrosAsync(CreateMacrosDto createMacrosDto);
        Task<MacrosDto> GetMacrosByIdAsync(long macrosId);
        Task<MacrosDto> UpdateMacrosAsync(UpdateMacrosDto updateMacrosDto);
        Task<bool> DeleteMacrosAsync(long macrosId, string eliminadoPor);
        Task<IEnumerable<MacrosDto>> GetWhereAsync(string condicion);
        Task<IEnumerable<MacrosDto>> GetAllMacrosAsync();
    }
}
