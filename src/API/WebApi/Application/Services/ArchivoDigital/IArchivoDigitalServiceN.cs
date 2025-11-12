using WebApi.Application.DTO.ArchivoDigital;
using WebApi.Application.DTO.ArchivoDigital;

namespace WebApi.Application.Services.ArchivoDigital
{
    public interface IArchivoDigitalServiceN
    {
        Task<ArchivoDigitalDto> CreateArchivoDigitalAsync(CreateArchivoDigitalDto createArchivoDigitalDto);
        Task<ArchivoDigitalDto> GetArchivoDigitalByIdAsync(long ArchivoDigitalId);
        Task<ArchivoDigitalDto> UpdateArchivoDigitalAsync(UpdateArchivoDigitalDto updateArchivoDigitalDto);
        Task<bool> DeleteArchivoDigitalAsync(long ArchivoDigitalId, string eliminadoPor);
        Task<IEnumerable<ArchivoDigitalDto>> GetWhereAsync(string condicion);
        Task<IEnumerable<ArchivoDigitalDto>> GetAllArchivoDigitalAsync();
        Task<IEnumerable<ArchivoDigitalDto>> SearchArchivoDigitalsAsync(string? value1, string? value2, string? value3);
    }
}
