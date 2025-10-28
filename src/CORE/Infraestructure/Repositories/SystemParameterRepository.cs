using Dapper;
using Domain.DomainInterfaces;
using Infraestructure.Models;
using Infraestructure.Persistence;
using Infraestructure.Repositories.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace Infraestructure.Repositories
{
    public class SystemParameterRepository : BaseCrudRepository<SystemParameterEntity, long>, ISystemParameterRepository
    {
        private readonly IDapperWrapper _dapperWrapper;
        public SystemParameterRepository(
            IConfiguration configuration,
            ILogger<SystemParameterRepository> logger,
            IDapperWrapper dapperWrapper) : base(configuration, logger, "system_parameter")
        {
            _dapperWrapper = dapperWrapper ?? throw new ArgumentNullException(nameof(dapperWrapper));
        }
        public async Task<long> CreateSystemParameterAsync(object SystemParameterData)
        {
            var SystemParameterEntity = ConvertToSystemParameterEntity(SystemParameterData);
            return await CreateAsync(SystemParameterEntity);
        }

        public async Task<bool> DeleteSystemParameterAsync(long SystemParameterId, string eliminadoPor)
        {
            var SystemParameter = await GetByIdAsync(SystemParameterId);
            if (SystemParameter == null)
            {
                return false;
            }

            var SystemParameterEntity = (SystemParameterEntity)SystemParameter;
            SystemParameterEntity.IsDeleted = true;
            SystemParameterEntity.UpdatedBy = eliminadoPor;
            SystemParameterEntity.UpdatedAt = DateTime.Now;

            return await UpdateAsync(SystemParameterEntity);
        }

        public async Task<IEnumerable<object>> GetAllSystemParameterAsync()
        {
            var SystemParameter = await GetAllAsync();
            return SystemParameter.Cast<object>();
        }

        public async Task<object?> GetSystemParameterByIdAsync(long SystemParameterId)
        {
            var SystemParameter = await GetByIdAsync(SystemParameterId);
            return SystemParameter;
        }

        public async Task<bool> UpdateSystemParameterAsync(object SystemParameterData)
        {
            var SystemParameterEntity = ConvertToSystemParameterEntity(SystemParameterData);
            return await UpdateAsync(SystemParameterEntity);
        }

        private static SystemParameterEntity ConvertToSystemParameterEntity(object SystemParameterData)
        {
            if (SystemParameterData is SystemParameterEntity SystemParameterEntity)
            {
                return SystemParameterEntity;
            }

            throw new ArgumentException("El objeto proporcionado no es del tipo SystemParameterEntity", nameof(SystemParameterData));
        }
    }
}
