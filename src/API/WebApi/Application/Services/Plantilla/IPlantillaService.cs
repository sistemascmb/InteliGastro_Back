using WebApi.Application.DTO.Plantilla;

namespace WebApi.Application.Services.Plantilla
{
    public interface IPlantillaService
    {
        Task<PlantillaDto> CreatePlantillaAsync(CreatePlantillaDto createPlantillaDto);
        Task<PlantillaDto> GetPlantillaByIdAsync(long plantillaId);
        Task<PlantillaDto> UpdatePlantillaAsync(UpdatePlantillaDto updatePlantillaDto);
        Task<bool> DeletePlantillaAsync(long plantillaId, string eliminadoPor);
        Task<IEnumerable<PlantillaDto>> GetWhereAsync(string condicion);
        Task<IEnumerable<PlantillaDto>> GetAllPlantillaAsync();
    }
}
