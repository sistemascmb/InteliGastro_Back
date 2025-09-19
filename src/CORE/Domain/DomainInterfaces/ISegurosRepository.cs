using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainInterfaces
{
    public interface ISegurosRepository
    {
        Task<long> CreateSegurosAsync(object segurosData);
        Task<object?> GetSegurosByIdAsync(long segurosId);
        Task<bool> UpdateSegurosAsync(object segurosData);
        Task<IEnumerable<object>> GetAllSegurosAsync();
    }
}
