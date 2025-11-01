using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainInterfaces
{
    public interface IPacienteRepository
    {
        Task<long> CreatePacienteAsync(object PacienteData);
        Task<object?> GetPacienteByIdAsync(long pacienteId);
        Task<bool> UpdatePacienteAsync(object PacienteData);
        Task<IEnumerable<object>> GetAllPacienteAsync();
        Task<object?> GetByConditionAsync(string documentNumber);
        Task<IEnumerable<object>> SearchPacientesAsync(string? documentNumber, string? names, string? lastNames);
    }
}
