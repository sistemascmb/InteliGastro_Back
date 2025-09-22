using WebApi.Application.DTO.Cie10;

namespace WebApi.Application.Services.Cie10
{
    public interface ICie10Service
    {
        Task<Cie10Dto> CreateCie10Async(CreateCie10Dto createCie10Dto);
        Task<Cie10Dto> GetCie10ByIdAsync(long cie10Id);
        Task<Cie10Dto> UpdateCie10Async(UpdateCie10Dto updateCie10Dto);
        Task<bool> DeleteCie10Async(long cie10Id, string eliminadoPor);
        Task<IEnumerable<Cie10Dto>> GetWhereAsync(string condicion);
        Task<IEnumerable<Cie10Dto>> GetAllCie10Async();
    }
}
