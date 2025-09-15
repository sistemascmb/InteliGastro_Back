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
    public class EstudiosRepository : BaseCrudRepository<EstudiosEntity, long>, IEstudiosRepository
    {
        private readonly IDapperWrapper _dapperWrapper;
        public EstudiosRepository(
            IConfiguration configuration,
            ILogger<EstudiosRepository> logger,
            IDapperWrapper dapperWrapper) : base(configuration, logger, "studies")
        {
            _dapperWrapper = dapperWrapper ?? throw new ArgumentNullException(nameof(dapperWrapper));
        }
        public async Task<long> CreateEstudiosAsync(object estudiosData)
        {
            var estudiosEntity = ConvertToEstudiosEntity(estudiosData);
            return await CreateAsync(estudiosEntity);
        }

        public Task<IEnumerable<object>> GetAllEstudiosAsync()
        {
            Task<IEnumerable<EstudiosEntity>> estudios = GetAllAsync();
            return estudios.ContinueWith(t => t.Result.Cast<object>());
        }

        public async Task<object?> GetEstudiosByIdAsync(long estudiosId)
        {
            var estudios = await GetByIdAsync(estudiosId);
            return estudios;
        }

        public async Task<bool> UpdateEstudiosAsync(object estudiosData)
        {
            var estudiosEntity = ConvertToEstudiosEntity(estudiosData);
            return await UpdateAsync(estudiosEntity);
        }

        private EstudiosEntity ConvertToEstudiosEntity(object data)
        {
            if (data is EstudiosEntity entity)
                return entity;
            var estudios = new EstudiosEntity();
            var properties = data.GetType().GetProperties();
            var targetProperties = typeof(EstudiosEntity).GetProperties();
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
