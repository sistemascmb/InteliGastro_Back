using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainInterfaces
{
    public interface IAgendaDxRepository
    {
        Task<long> CreateAgendaDxAsync(object AgendaDxData);
        Task<object?> GetAgendaDxByIdAsync(long AgendaDxId);
        Task<bool> UpdateAgendaDxAsync(object AgendaDxData);
        Task<IEnumerable<object>> GetAllAgendaDxAsync();
        Task<IEnumerable<object>> SearchAgendaDxsAsync(string? value1);
    }
}
