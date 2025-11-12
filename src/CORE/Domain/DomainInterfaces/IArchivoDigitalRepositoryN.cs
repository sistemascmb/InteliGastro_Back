using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainInterfaces
{
    public interface IArchivoDigitalRepositoryN
    {
        Task<long> CreateArchivoDigitalAsync(object ArchivoDigitalData);
        Task<object?> GetArchivoDigitalByIdAsync(long ArchivoDigitalId);
        Task<bool> UpdateArchivoDigitalAsync(object ArchivoDigitalData);
        Task<IEnumerable<object>> GetAllArchivoDigitalAsync();
        Task<IEnumerable<object>> SearchArchivoDigitalsAsync(string? value1, string? value2, string? value3);
    }
}
