using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainInterfaces
{
    public interface IMacrosRepository
    {
        Task<long> CreateMacrosAsync(object MacrosData);
        Task<object?> GetMacrosByIdAsync(long macrosId);
        Task<bool> UpdateMacrosAsync(object MacrosData);
        Task<IEnumerable<object>> GetAllMacrosAsync();
    }
}
