using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainInterfaces
{
    public interface IPlantillaRepository
    {
        Task<long> CreatePlantillaAsync(object plantillaData);
        Task<object?> GetPlantillaByIdAsync(long plantillaId);
        Task<bool> UpdatePlantillaAsync(object plantillaData);
        Task<IEnumerable<object>> GetAllPlantillaAsync();
    }
}
