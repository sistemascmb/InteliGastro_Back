using Domain.DomainInterfaces;
using Infraestructure.Models;
using Infraestructure.Persistence;
using Infraestructure.Repositories.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class PersonalRepository : BaseCrudRepository<PersonalEntity, long>, IPersonalRepository
    {
        private readonly IDapperWrapper _dapperWrapper;
        public PersonalRepository(
            IConfiguration configuration,
            ILogger<PersonalRepository> logger,
            IDapperWrapper dapperWrapper) : base(configuration, logger, "personal")
        {
            _dapperWrapper = dapperWrapper ?? throw new ArgumentNullException(nameof(dapperWrapper));
        }
        public async Task<long> CreatePersonalAsync(object personalData)
        {
            var personalEntity = ConvertToPersonalEntity(personalData);
            return await CreateAsync(personalEntity);
        }

        public async Task<object?> GetPersonalByIdAsync(long personalId)
        {
            var personal = await GetByIdAsync(personalId);
            return personal;
        }

        public async Task<bool> UpdatePersonalAsync(object personalData)
        {
            var personalEntity = ConvertToPersonalEntity(personalData);
            return await UpdateAsync(personalEntity);
        }

        //
        private PersonalEntity ConvertToPersonalEntity(object data)
        {
            if (data is PersonalEntity entity)
                return entity;
            var personal = new PersonalEntity();
            var properties = data.GetType().GetProperties();
            var targetProperties = typeof(PersonalEntity).GetProperties();
            foreach (var prop in properties)
            {
                var targetProp = targetProperties.FirstOrDefault(p => p.Name.Equals(prop.Name, StringComparison.OrdinalIgnoreCase)
                                                                     && p.PropertyType == prop.PropertyType);
                if (targetProp != null && targetProp.CanWrite)
                {
                    targetProp.SetValue(personal, prop.GetValue(data));
                }
            }
            return personal;
        }
    }
}
