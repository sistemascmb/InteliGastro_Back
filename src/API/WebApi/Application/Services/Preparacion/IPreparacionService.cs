using WebApi.Application.DTO.Preparacion;

namespace WebApi.Application.Services.Preparacion
{
    public interface IPreparacionService
    {
        Task<PreparacionDto> CreatePreparacionAsync(CreatePreparacionDto createPreparacionDto);
        Task<PreparacionDto> GetPreparacionByIdAsync(long id);
        Task<PreparacionDto> UpdatePreparacionAsync(UpdatePreparacionDto updatePreparacionDto);
        Task<bool> DeletePreparacionAsync(long preparacionid, string eliminadoPor);
        Task<IEnumerable<PreparacionDto>> GetWhereAsync(string conddicion);
        Task<IEnumerable<PreparacionDto>> GetAllPreparacionAsync();
    }
}
