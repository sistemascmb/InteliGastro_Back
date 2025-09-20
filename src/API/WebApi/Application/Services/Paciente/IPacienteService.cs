using WebApi.Application.DTO.Paciente;

namespace WebApi.Application.Services.Paciente
{
    public interface IPacienteService
    {
        Task<PacienteDto> CreatePacienteAsync(CreatePacienteDto createPacienteDto);
        Task<PacienteDto> GetPacienteByIdAsync(long pacienteId);
        Task<PacienteDto> UpdatePacienteAsync(UpdatePacienteDto updatePacienteDto);
        Task<bool> DeletePacienteAsync(long pacienteId, string eliminadoPor);
        Task<IEnumerable<PacienteDto>> GetWhereAsync(string condicion);
        Task<IEnumerable<PacienteDto>> GetAllPacienteAsync();
    }
}
