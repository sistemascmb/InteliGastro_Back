using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainInterfaces
{
    public interface IExamenesRepository
    {
        Task<long> CreateExamenesAsync(object examenesData);
        Task<object?> GetExamenesByIdAsync(long examenesId);
        Task<bool> UpdateExamenesAsync(object examenesData);

        Task<IEnumerable<object>> GetAllExamenesAsync();
    }
}
