using WebApi.Application.DTO.Centro;

namespace WebApi.Application.Services.Centro
{
    public interface ICentroService
    {
        Task<CentroDto> CreateCentroAsync(CreateCentroDto createCentroDto);
        Task<CentroDto> GetCentroByIdAsync(long centroId);
        Task<CentroDto> UpdateCentroAsync(UpdateCentroDto updateCentroDto);
        Task<bool> DeleteCentroAsync(long centroId, string eliminadoPor);
    }
}
