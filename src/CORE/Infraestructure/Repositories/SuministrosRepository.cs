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
    public class SuministrosRepository : BaseCrudRepository<SuministrosEntity, long>, ISuministrosRepository
    {
        private readonly IDapperWrapper _dapperWrapper;
        public SuministrosRepository(
            IConfiguration configuration,
            ILogger<SuministrosRepository> logger,
            IDapperWrapper dapperWrapper) : base(configuration, logger, "provision")
        {
            _dapperWrapper = dapperWrapper ?? throw new ArgumentNullException(nameof(dapperWrapper));
        }
        public async Task<long> CreateSuministrosAsync(object SuministrosData)
        {
            var SuministrosEntity = ConvertToSuministrosEntity(SuministrosData);
            return await CreateAsync(SuministrosEntity);
        }

        public Task<IEnumerable<object>> GetAllSuministrosAsync()
        {
            Task<IEnumerable<SuministrosEntity>> Suministros = GetAllAsync();
            return Suministros.ContinueWith(t => t.Result.Cast<object>());
        }

        public async Task<object?> GetSuministrosByIdAsync(long SuministrosId)
        {
            var Suministros = await GetByIdAsync(SuministrosId);
            return Suministros;
        }

        public async Task<bool> UpdateSuministrosAsync(object SuministrosData)
        {
            var SuministrosEntity = ConvertToSuministrosEntity(SuministrosData);
            return await UpdateAsync(SuministrosEntity);
        }

        private SuministrosEntity ConvertToSuministrosEntity(object data)
        {
            if (data is SuministrosEntity entity)
                return entity;
            var Suministros = new SuministrosEntity();
            var properties = data.GetType().GetProperties();
            var targetProperties = typeof(SuministrosEntity).GetProperties();
            foreach (var prop in properties)
            {
                var targetProp = targetProperties.FirstOrDefault(p => p.Name.Equals(prop.Name, StringComparison.OrdinalIgnoreCase)
                                                                     && p.PropertyType == prop.PropertyType);
                if (targetProp != null)
                {
                    targetProp.SetValue(Suministros, prop.GetValue(data));
                }
            }
            return Suministros;
        }
    }
}
