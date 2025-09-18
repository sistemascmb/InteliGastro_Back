using WebApi.Application.DTO.Examenes;

namespace WebApi.Application.Services.Examenes
{
    public interface IExamenesService
    {
        Task<ExamenesDto> CreateSalasAsync(CreateExamenesDto createExamenesDto);
        Task<ExamenesDto> GetExamenesByIdAsync(long id);
        Task<ExamenesDto> UpdateExamenesAsync(UpdateExamenesDto updateExamenesDto);
        Task<bool> DeleteExamenesAsync(long id, string eliminadoPor);
        Task<IEnumerable<ExamenesDto>> GetWhereAsync(string condicion);
        Task<IEnumerable<ExamenesDto>> GetAllExamenesAsync();
    }
}
