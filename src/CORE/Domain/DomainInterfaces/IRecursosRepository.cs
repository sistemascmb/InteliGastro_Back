using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainInterfaces
{
    public interface IRecursosRepository
    {
        Task<long> CreateRecursosAsync(object recursosData);
        Task<object?> GetRecursosByIdAsync(long recursosId);
        Task<bool> UpdateRecursosAsync(object recursosData);
        Task<IEnumerable<object>> GetAllRecursosAsync();    
    }
}
