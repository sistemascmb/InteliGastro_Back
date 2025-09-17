using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainInterfaces
{
    public interface ISalasRepository
    {
        Task<long> CreateSalasAsync(object salasData);
        Task<object?> GetSalasByIdAsync(long salasId);
        Task<bool> UpdateSalasAsync(object salasData);
        Task<IEnumerable<object>> GetAllSalasAsync();
    }
}
