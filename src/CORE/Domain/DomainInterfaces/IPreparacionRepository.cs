using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainInterfaces
{
    public interface IPreparacionRepository
    {
        Task<long> CreatePreparacionAsync(object preparacionDto);
        Task<object?> GetPreparacionByIdAsync(long preparacionid);
        Task<bool> UpdatePreparacionAsync(object preparacionDto);
        Task<IEnumerable<object>> GetAllPreparacion();
    }
}
