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
    public class RolesRepository : BaseCrudRepository<RolesEntity, long>, IRolesRepository
    {
        private readonly  IDapperWrapper _dapperWrapper;

        public RolesRepository(
            IConfiguration configuration,
            ILogger<RolesRepository> logger,
            IDapperWrapper dapperWrapper) : base(configuration, logger, "system_users_profile")
        {
            _dapperWrapper = dapperWrapper ?? throw new ArgumentNullException(nameof(dapperWrapper));
        }
        public async Task<long> CreateRolesAsync(object RolesData)
        {
            var RolesEntity = ConvertToRolesEntity(RolesData);
            return await CreateAsync(RolesEntity);
        }

        public Task<IEnumerable<object>> GetAllRolesAsync()
        {
            Task<IEnumerable<RolesEntity>> Roles = GetAllAsync();
            return Roles.ContinueWith(t => t.Result.Cast<object>());
        }

        public async Task<object?> GetRolesByIdAsync(long RolesId)
        {
            var Roles = await GetByIdAsync(RolesId);
            return Roles;
        }

        public async Task<bool> UpdateRolesAsync(object RolesData)
        {
            var RolesEntity = ConvertToRolesEntity(RolesData);
            return await UpdateAsync(RolesEntity);
        }

        private RolesEntity ConvertToRolesEntity(object data)
        {
            if (data is RolesEntity entity)
                return entity;
            var Roles = new RolesEntity();
            var properties = data.GetType().GetProperties();
            var targetProperties = typeof(RolesEntity).GetProperties();
            foreach (var prop in properties)
            {
                var targetProp = targetProperties.FirstOrDefault(p => p.Name.Equals(prop.Name, StringComparison.OrdinalIgnoreCase)
                                                                     && p.PropertyType == prop.PropertyType);
                if (targetProp != null)
                {
                    targetProp.SetValue(Roles, prop.GetValue(data));
                }
            }
            return Roles;
        }
    }
}
