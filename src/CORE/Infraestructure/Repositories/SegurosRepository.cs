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
    public class SegurosRepository : BaseCrudRepository<SegurosEntity, long>, ISegurosRepository
    {
        private readonly IDapperWrapper _dapperWrapper;
        public SegurosRepository(
            IConfiguration configuration,
            ILogger<SegurosRepository> logger,
            IDapperWrapper dapperWrapper) : base(configuration, logger, "insurance")
        {
            _dapperWrapper = dapperWrapper ?? throw new ArgumentNullException(nameof(dapperWrapper));
        }
        public async Task<long> CreateSegurosAsync(object SegurosData)
        {
            var SegurosEntity = ConvertToSegurosEntity(SegurosData);
            return await CreateAsync(SegurosEntity);
        }

        public Task<IEnumerable<object>> GetAllSegurosAsync()
        {
            Task<IEnumerable<SegurosEntity>> Seguros = GetAllAsync();
            return Seguros.ContinueWith(t => t.Result.Cast<object>());
        }

        public async Task<object?> GetSegurosByIdAsync(long SegurosId)
        {
            var Seguros = await GetByIdAsync(SegurosId);
            return Seguros;
        }

        public async Task<bool> UpdateSegurosAsync(object SegurosData)
        {
            var SegurosEntity = ConvertToSegurosEntity(SegurosData);
            return await UpdateAsync(SegurosEntity);
        }

        private SegurosEntity ConvertToSegurosEntity(object data)
        {
            if (data is SegurosEntity entity)
                return entity;
            var Seguros = new SegurosEntity();
            var properties = data.GetType().GetProperties();
            var targetProperties = typeof(SegurosEntity).GetProperties();
            foreach (var prop in properties)
            {
                var targetProp = targetProperties.FirstOrDefault(p => p.Name.Equals(prop.Name, StringComparison.OrdinalIgnoreCase)
                                                                     && p.PropertyType == prop.PropertyType);
                if (targetProp != null)
                {
                    targetProp.SetValue(Seguros, prop.GetValue(data));
                }
            }
            return Seguros;
        }
    }
}
