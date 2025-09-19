using WebApi.Application.DTO.Seguros;
using WebApi.Application.DTO.Seguros;

namespace WebApi.Application.Services.Seguros
{
    public interface ISegurosService
    {
        Task<SegurosDto> CreateSegurosAsync(CreateSegurosDto createSegurosDto);
        Task<SegurosDto> GetSegurosByIdAsync(long id);
        Task<SegurosDto> UpdateSegurosAsync(UpdateSegurosDto updateSegurosDto);
        Task<bool> DeleteSegurosAsync(long Segurosid, string eliminadoPor);
        Task<IEnumerable<SegurosDto>> GetWhereAsync(string conddicion);
        Task<IEnumerable<SegurosDto>> GetAllSegurosAsync();
    }
}
