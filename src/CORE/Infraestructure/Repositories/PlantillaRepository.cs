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
    public class PlantillaRepository : BaseCrudRepository<PlantillaEntity, long>, IPlantillaRepository 
    {
        private readonly IDapperWrapper _dapperWrapper;
        public PlantillaRepository(
            IConfiguration configuration,
            ILogger<PlantillaRepository> logger,
            IDapperWrapper dapperWrapper) : base(configuration, logger, "templates")
        {
            _dapperWrapper = dapperWrapper ?? throw new ArgumentNullException(nameof(dapperWrapper));
        }
        public async Task<long> CreatePlantillaAsync(object PlantillaData)
        {
            var PlantillaEntity = ConvertToPlantillaEntity(PlantillaData);
            return await CreateAsync(PlantillaEntity);
        }

        public Task<IEnumerable<object>> GetAllPlantillaAsync()
        {
            Task<IEnumerable<PlantillaEntity>> Plantilla = GetAllAsync();
            return Plantilla.ContinueWith(t => t.Result.Cast<object>());
        }

        public async Task<object?> GetPlantillaByIdAsync(long plantillaId)
        {
            var Plantilla = await GetByIdAsync(plantillaId);
            return Plantilla;
        }

        public async Task<bool> UpdatePlantillaAsync(object PlantillaData)
        {
            var PlantillaEntity = ConvertToPlantillaEntity(PlantillaData);
            return await UpdateAsync(PlantillaEntity);
        }

        private PlantillaEntity ConvertToPlantillaEntity(object data)
        {
            if (data is PlantillaEntity entity)
                return entity;
            var Plantilla = new PlantillaEntity();
            var properties = data.GetType().GetProperties();
            var targetProperties = typeof(PlantillaEntity).GetProperties();
            foreach (var prop in properties)
            {
                var targetProp = targetProperties.FirstOrDefault(p => p.Name.Equals(prop.Name, StringComparison.OrdinalIgnoreCase)
                                                                     && p.PropertyType == prop.PropertyType);
                if (targetProp != null)
                {
                    targetProp.SetValue(Plantilla, prop.GetValue(data));
                }
            }
            return Plantilla;
        }
    }
}
