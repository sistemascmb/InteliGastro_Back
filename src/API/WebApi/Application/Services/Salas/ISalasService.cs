using WebApi.Application.DTO.Salas;

namespace WebApi.Application.Services.Salas
{
    public interface ISalasService
    {
        Task<SalasDto> CreateSalasAsync(CreateSalasDto createSalasDto);
        Task<SalasDto> GetSalasByIdAsync(long salasId);
        Task<SalasDto> UpdateSalasAsync(UpdateSalasDto updateSalasDto);
        Task<bool> DeleteSalasAsync(long salasId, string eliminadoPor);
        Task<IEnumerable<SalasDto>> GetWhereAsync(string condicion);
        Task<IEnumerable<SalasDto>> GetAllSalasAsync();
    }
}
