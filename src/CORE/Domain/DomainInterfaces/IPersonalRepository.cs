using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainInterfaces
{
    public interface IPersonalRepository
    {
        Task<long> CreatePersonalAsync(object personalData);
        Task<object?> GetPersonalByIdAsync(long personalId);
        Task<bool> UpdatePersonalAsync(object personalData);
        //Task<bool> DeletePersonalAsync(long personalId);
        //Task<bool> ExistsPersonalAsync(long personalId);  
    }
}
