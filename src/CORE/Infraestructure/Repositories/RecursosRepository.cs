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
    public class RecursosRepository : BaseCrudRepository<RecursosEntity, long>, IRecursosRepository
    {
        private readonly IDapperWrapper _dapperWrapper;
        public RecursosRepository(
            IConfiguration configuration,
            ILogger<RecursosRepository> logger,
            IDapperWrapper dapperWrapper) : base(configuration, logger, "resources")
        {
            _dapperWrapper = dapperWrapper ?? throw new ArgumentNullException(nameof(dapperWrapper));
        }   
        public async Task<long> CreateRecursosAsync(object recursosData)
        {
            var recursosEntity = ConvertToRecursosEntity(recursosData);
            return await CreateAsync(recursosEntity);
        }

        public Task<IEnumerable<object>> GetAllRecursosAsync()
        {
            Task<IEnumerable<RecursosEntity>> recursos = GetAllAsync();
            return recursos.ContinueWith(t => t.Result.Cast<object>());
        }

        public async Task<object?> GetRecursosByIdAsync(long recursosId)
        {
            var recursos = await GetByIdAsync(recursosId);
            return recursos;
        }

        public async Task<bool> UpdateRecursosAsync(object recursosData)
        {
            var recursosEntity = ConvertToRecursosEntity(recursosData);
            return await UpdateAsync(recursosEntity);
        }

        private RecursosEntity ConvertToRecursosEntity(object data)
        {
            if (data is RecursosEntity entity)
                return entity;
            var recursos = new RecursosEntity();
            var properties = data.GetType().GetProperties();
            var targetProperties = typeof(RecursosEntity).GetProperties();
            foreach (var prop in properties)
            {
                var targetProp = targetProperties.FirstOrDefault(p => p.Name.Equals(prop.Name, StringComparison.OrdinalIgnoreCase)
                                                                     && p.PropertyType == prop.PropertyType);
                if (targetProp != null)
                {
                    targetProp.SetValue(recursos, prop.GetValue(data));
                }
            }
            return recursos;
        }
    }
}
