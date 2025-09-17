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
    public class SalasRepository : BaseCrudRepository<SalasEntity, long>, ISalasRepository
    {
        private readonly IDapperWrapper _dapperWrapper;
        public SalasRepository(
            IConfiguration configuration,
            ILogger<SalasRepository> logger,
            IDapperWrapper dapperWrapper) : base(configuration, logger, "procedure_room")
        {
            _dapperWrapper = dapperWrapper ?? throw new ArgumentNullException(nameof(dapperWrapper));
        }
        public async Task<long> CreateSalasAsync(object salasData)
        {
            var salasEntity = ConvertToSalasEntity(salasData);
            return await CreateAsync(salasEntity);
        }

        public Task<IEnumerable<object>> GetAllSalasAsync()
        {
            Task<IEnumerable<SalasEntity>> salas = GetAllAsync();
            return salas.ContinueWith(t => t.Result.Cast<object>());
        }

        public async Task<object?> GetSalasByIdAsync(long salasId)
        {
            var salas = await GetByIdAsync(salasId);
            return salas;
        }

        public async Task<bool> UpdateSalasAsync(object salasData)
        {
            var salasEntity = ConvertToSalasEntity(salasData);
            return await UpdateAsync(salasEntity);
        }

        private SalasEntity ConvertToSalasEntity(object data)
        {
            if (data is SalasEntity entity)
                return entity;
            var salas = new SalasEntity();
            var properties = data.GetType().GetProperties();
            var targetProperties = typeof(SalasEntity).GetProperties();
            foreach (var prop in properties)
            {
                var targetProp = targetProperties.FirstOrDefault(p => p.Name.Equals(prop.Name, StringComparison.OrdinalIgnoreCase)
                                                                     && p.PropertyType == prop.PropertyType);
                if (targetProp != null)
                {
                    targetProp.SetValue(salas, prop.GetValue(data));
                }
            }
            return salas;
        }
    }
}
