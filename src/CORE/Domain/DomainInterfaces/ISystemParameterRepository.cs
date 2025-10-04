using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainInterfaces
{
    public interface ISystemParameterRepository
    {
        Task<long> CreateSystemParameterAsync(object SystemParameterData);
        Task<object?> GetSystemParameterByIdAsync(long systemParameterId);
        Task<bool> UpdateSystemParameterAsync(object SystemParameterData);
        Task<IEnumerable<object>> GetAllSystemParameterAsync();
    }
}
