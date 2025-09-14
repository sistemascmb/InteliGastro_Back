using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainInterfaces
{
    public interface ICentroRepository
    {
        Task<long> CreateCentroAsync(object centroData);
        Task<object?> GetCentroByIdAsync(long centroId);
        Task<bool> UpdateCentroAsync(object centroData);
        //Task<bool> DeleteCentroAsync(long centroId);
        //Task<bool> ExistsCentroAsync(long centroId);
        
    }
}
