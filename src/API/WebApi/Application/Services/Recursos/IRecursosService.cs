using WebApi.Application.DTO.Recursos;

namespace WebApi.Application.Services.Recursos
{
    public interface IRecursosService
    {
        Task<RecursosDto> CreateRecursosAsync(CreateRecursosDto createRecursosDto);
        Task<RecursosDto> GetRecursosByIdAsync(long recursosId);
        Task<RecursosDto> UpdateRecursosAsync(UpdateRecursosDto updateRecursosDto);
        Task<bool> DeleteRecursosAsync(long recursosId, string eliminadoPor);
        Task<IEnumerable<RecursosDto>> GetWhereAsync(string condicion);
        Task<IEnumerable<RecursosDto>> GetAllRecursosAsync();

    }
}
