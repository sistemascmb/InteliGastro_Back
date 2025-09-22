using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainInterfaces
{
    public interface ISuministrosRepository
    {
        Task<long> CreateSuministrosAsync(object SuministrosData);
        Task<object?> GetSuministrosByIdAsync(long suministrosId);
        Task<bool> UpdateSuministrosAsync(object SuministrosData);
        Task<IEnumerable<object>> GetAllSuministrosAsync();
    }
}
