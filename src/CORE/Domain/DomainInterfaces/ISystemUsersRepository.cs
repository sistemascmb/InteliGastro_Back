using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainInterfaces
{
    public interface ISystemUsersRepository
    {
        Task<long> CreateSystemUsersAsync(object systemUsersData);
        Task<object?> GetSystemUsersByIdAsync(long UserId);
        Task<bool> UpdateSystemUsersAsync(object systemUsersData);
        Task<bool> DeleteSystemUsersAsync(long UserId);
        Task<bool> ExistsSystemUsersAsync(long UserId);
        Task<IEnumerable<object>> GetSystemUsersWhereAsync(string whereClause, object? parameters = null);
        Task<dynamic?> GetSystemUsersCompletaAsync(long UserId);
        Task<bool> ExistsAsync(int id);

        Task<IEnumerable<object>> GetAllSystemUsersAsync();

    }
}
