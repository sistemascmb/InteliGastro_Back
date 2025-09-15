using WebApi.Application.DTO.Estudios;

namespace WebApi.Application.Services.Estudios
{
    public interface IEstudiosService
    {
        Task<EstudiosDto> CreateEstudiosAsync(CreateEstudiosDto createEstudiosDto);
        Task<EstudiosDto> GetEstudiosByIdAsync(long estudiosId);
        Task<EstudiosDto> UpdateEstudiosAsync(UpdateEstudiosDto updateEstudiosDto);
        Task<bool> DeleteEstudiosAsync(long estudiosId, string eliminadoPor);
        Task<IEnumerable<EstudiosDto>> GetWhereAsync(string condicion);
        Task<IEnumerable<EstudiosDto>> GetAllEstudiosAsync();
    }
}
