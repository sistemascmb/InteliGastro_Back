using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainInterfaces
{
    public interface IRolesRepository
    {
        Task<long> CreateRolesAsync(object rolesData);
        Task<object?> GetRolesByIdAsync(long rolesId);
        Task<bool> UpdateRolesAsync(object rolesData);
        Task<IEnumerable<object>> GetAllRolesAsync();
    }
}
