using WebApi.Application.DTO.MedicoReferencia;

namespace WebApi.Application.Services.MedicoReferencia
{
    public interface IMedicoReferenciaService
    {
        Task<MedicoReferenciaDto> CreateMedicoReferenciaAsync(CreateMedicoReferenciaDto createMedicoReferenciaDto);
        Task<MedicoReferenciaDto> GetMedicoReferenciaByIdAsync(long medicoReferenciaId);
        Task<MedicoReferenciaDto> UpdateMedicoReferenciaAsync(UpdateMedicoReferenciaDto updateMedicoReferenciaDto);
        Task<bool> DeleteMedicoReferenciaAsync(long medicoReferenciaId, string eliminadoPor);
        Task<IEnumerable<MedicoReferenciaDto>> GetWhereAsync(string condicion);
        Task<IEnumerable<MedicoReferenciaDto>> GetAllMedicoReferenciaAsync();
    }
}
