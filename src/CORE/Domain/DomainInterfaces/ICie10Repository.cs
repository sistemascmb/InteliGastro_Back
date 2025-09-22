using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainInterfaces
{
    public interface ICie10Repository
    {
        Task<long> CreateCie10Async(object Cie10Data);
        Task<object?> GetCie10ByIdAsync(long cie10Id);
        Task<bool> UpdateCie10Async(object Cie10Data);
        Task<IEnumerable<object>> GetAllCie10Async();
    }
}
