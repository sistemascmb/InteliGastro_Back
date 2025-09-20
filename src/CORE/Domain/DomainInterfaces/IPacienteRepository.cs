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
    }
}
