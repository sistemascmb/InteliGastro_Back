using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainInterfaces
{
    public interface IAgendaRepository
    {
        Task<long> CreateAgendaAsync(object AgendaData);
        Task<object?> GetAgendaByIdAsync(long AgendaId);
        Task<bool> UpdateAgendaAsync(object AgendaData);
        Task<IEnumerable<object>> GetAllAgendaAsync();
        Task<IEnumerable<object>> SearchAgendaByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<object>> SearchAgendaByFiltersAsync(long? medicalscheduleid, IEnumerable<int>? pacientIds);
    }
}
