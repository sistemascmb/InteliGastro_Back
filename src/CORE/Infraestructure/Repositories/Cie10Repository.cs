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
    public class Cie10Repository : BaseCrudRepository<Cie10Entity, long>, ICie10Repository
    {
        private readonly IDapperWrapper _dapperWrapper;
        public Cie10Repository(
            IConfiguration configuration,
            ILogger<Cie10Repository> logger,
            IDapperWrapper dapperWrapper) : base(configuration, logger, "cie10")
        {
            _dapperWrapper = dapperWrapper ?? throw new ArgumentNullException(nameof(dapperWrapper));
        }
        public async Task<long> CreateCie10Async(object Cie10Data)
        {
            var Cie10Entity = ConvertToCie10Entity(Cie10Data);
            return await CreateAsync(Cie10Entity);
        }

        public Task<IEnumerable<object>> GetAllCie10Async()
        {
            Task<IEnumerable<Cie10Entity>> Cie10 = GetAllAsync();
            return Cie10.ContinueWith(t => t.Result.Cast<object>());
        }

        public async Task<object?> GetCie10ByIdAsync(long Cie10Id)
        {
            var Cie10 = await GetByIdAsync(Cie10Id);
            return Cie10;
        }

        public async Task<bool> UpdateCie10Async(object Cie10Data)
        {
            var Cie10Entity = ConvertToCie10Entity(Cie10Data);
            return await UpdateAsync(Cie10Entity);
        }

        private Cie10Entity ConvertToCie10Entity(object data)
        {
            if (data is Cie10Entity entity)
                return entity;
            var Cie10 = new Cie10Entity();
            var properties = data.GetType().GetProperties();
            var targetProperties = typeof(Cie10Entity).GetProperties();
            foreach (var prop in properties)
            {
                var targetProp = targetProperties.FirstOrDefault(p => p.Name.Equals(prop.Name, StringComparison.OrdinalIgnoreCase)
                                                                     && p.PropertyType == prop.PropertyType);
                if (targetProp != null)
                {
                    targetProp.SetValue(Cie10, prop.GetValue(data));
                }
            }
            return Cie10;
        }
    }
}
