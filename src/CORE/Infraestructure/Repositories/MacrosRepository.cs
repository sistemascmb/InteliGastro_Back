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
    public class MacrosRepository : BaseCrudRepository<MacrosEntity, long>, IMacrosRepository
    {
        private readonly IDapperWrapper _dapperWrapper;

        public MacrosRepository(
            IConfiguration configuration,
            ILogger<MacrosRepository> logger,
            IDapperWrapper dapperWrapper) : base(configuration, logger, "studies")
        {
            _dapperWrapper = dapperWrapper ?? throw new ArgumentNullException(nameof(dapperWrapper));
        }
        public async Task<long> CreateMacrosAsync(object MacrosData)
        {
            var MacrosEntity = ConvertToMacrosEntity(MacrosData);
            return await CreateAsync(MacrosEntity);
        }

        public Task<IEnumerable<object>> GetAllMacrosAsync()
        {
            Task<IEnumerable<MacrosEntity>> Macros = GetAllAsync();
            return Macros.ContinueWith(t => t.Result.Cast<object>());
        }

        public async Task<object?> GetMacrosByIdAsync(long MacrosId)
        {
            var Macros = await GetByIdAsync(MacrosId);
            return Macros;
        }

        public async Task<bool> UpdateMacrosAsync(object MacrosData)
        {
            var MacrosEntity = ConvertToMacrosEntity(MacrosData);
            return await UpdateAsync(MacrosEntity);
        }

        private MacrosEntity ConvertToMacrosEntity(object data)
        {
            if (data is MacrosEntity entity)
                return entity;
            var Macros = new MacrosEntity();
            var properties = data.GetType().GetProperties();
            var targetProperties = typeof(MacrosEntity).GetProperties();
            foreach (var prop in properties)
            {
                var targetProp = targetProperties.FirstOrDefault(p => p.Name.Equals(prop.Name, StringComparison.OrdinalIgnoreCase)
                                                                     && p.PropertyType == prop.PropertyType);
                if (targetProp != null)
                {
                    targetProp.SetValue(Macros, prop.GetValue(data));
                }
            }
            return Macros;
        }
    }
}
