using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainInterfaces
{
    public interface IEstudiosRepository
    {
        Task<long> CreateEstudiosAsync(object estudiosData);
        Task<object?> GetEstudiosByIdAsync(long estudiosId);
        Task<bool> UpdateEstudiosAsync(object estudiosData);
        Task<IEnumerable<object>> GetAllEstudiosAsync();
    }
}
