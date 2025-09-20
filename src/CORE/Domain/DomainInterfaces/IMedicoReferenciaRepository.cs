using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainInterfaces
{
    public interface IMedicoReferenciaRepository
    {
        Task<long> CreateMedicoReferenciaAsync(object MedicoReferenciaData);
        Task<object?> GetMedicoReferenciaByIdAsync(long medicoReferenciaId);
        Task<bool> UpdateMedicoReferenciaAsync(object MedicoReferenciaData);
        Task<IEnumerable<object>> GetAllMedicoReferenciaAsync();
    }
}
