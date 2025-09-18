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
    public class PreparacionRepository : BaseCrudRepository<PreparacionEntity, long>, IPreparacionRepository
    {
        private readonly IDapperWrapper _dapperWrapper;

        public PreparacionRepository(
                    IConfiguration configuration,
                    ILogger<PreparacionRepository> logger,
                    IDapperWrapper dapperWrapper) : base(configuration, logger, "preparation")
        {
            _dapperWrapper = dapperWrapper ?? throw new ArgumentNullException(nameof(dapperWrapper));
        }
        public async Task<long> CreatePreparacionAsync(object preparacionDto)
        {
            var preparacionEntity = ConvertToPreparacionEntity(preparacionDto);
            return await CreateAsync(preparacionEntity);
        }

        public Task<IEnumerable<object>> GetAllPreparacion()
        {
            Task<IEnumerable<PreparacionEntity>> preparacion = GetAllAsync();
            return preparacion.ContinueWith(x => x.Result.Cast<object>());
        }

        public async Task<object?> GetPreparacionByIdAsync(long preparacionid)
        {
            var preparacion = await GetByIdAsync(preparacionid);
            return preparacion;
        }

        public async Task<bool> UpdatePreparacionAsync(object preparacionDto)
        {
            var preparacionEntity = ConvertToPreparacionEntity(preparacionDto);
            return await UpdateAsync(preparacionEntity);
        }

        private PreparacionEntity ConvertToPreparacionEntity(object data)
        {
            if (data is PreparacionEntity entity)
                return entity;
            var estudios = new PreparacionEntity();
            var properties = data.GetType().GetProperties();
            var targetProperties = typeof(PreparacionEntity).GetProperties();
            foreach (var prop in properties)
            {
                var targetProp = targetProperties.FirstOrDefault(p => p.Name.Equals(prop.Name, StringComparison.OrdinalIgnoreCase)
                                                                     && p.PropertyType == prop.PropertyType);
                if (targetProp != null)
                {
                    targetProp.SetValue(estudios, prop.GetValue(data));
                }
            }
            return estudios;
        }
    }
}
