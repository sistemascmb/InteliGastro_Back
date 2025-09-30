using Dapper;
using Domain.DomainInterfaces;
using Infraestructure.Models;
using Infraestructure.Persistence;
using Infraestructure.Repositories.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Infraestructure.Repositories
{
    public class SystemUsersRepository : BaseCrudRepository<SystemUsersEntity, long>, ISystemUsersRepository
    {
        

        private readonly IDapperWrapper _dapperWrapper;

        public SystemUsersRepository(
            IConfiguration configuration,
            ILogger<SystemUsersRepository> logger,
            IDapperWrapper dapperWrapper)
            : base(configuration, logger, "system_users")
        {
            _dapperWrapper = dapperWrapper ?? throw new ArgumentNullException(nameof(dapperWrapper));
        }

        public async Task<long> CreateSystemUsersAsync(object systemUsersData)
        {
            var systemUsersEntity = ConvertToSystemUsersEntity(systemUsersData);
            return await CreateAsync(systemUsersEntity);
        }

        public Task<bool> DeleteSystemUsersAsync(long UserId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsSystemUsersAsync(long UserId)
        {
            throw new NotImplementedException();
        }

        public async Task<object?> GetSystemUsersByIdAsync(long UserId)
        {
            var sytemUsers = await GetByIdAsync(UserId);
            return sytemUsers;
        }

        public Task<dynamic?> GetSystemUsersCompletaAsync(long UserId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<object>> GetSystemUsersWhereAsync(string whereClause, object? parameters = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateSystemUsersAsync(object systemUsersData)
        {
            var systemUsersEntity = ConvertToSystemUsersEntity(systemUsersData);
            return UpdateAsync(systemUsersEntity);
        }

        public Task<IEnumerable<object>> GetAllSystemUsersAsync()
        {
            Task<IEnumerable<SystemUsersEntity>> SystemUsers = GetAllAsync();
            return SystemUsers.ContinueWith(t => t.Result.Cast<object>());
        }

        // ===== MÉTODOS AUXILIARES =====
        //METODO: Implementar AutoMapper o similar para mapeo automático
        private SystemUsersEntity ConvertToSystemUsersEntity(object data)
        {
            if (data is SystemUsersEntity entity)
                return entity;

            // Aquí podrías implementar un mapper más sofisticado
            // Por ahora, manejo básico para demostración
            var systemUsers = new SystemUsersEntity();

            var properties = data.GetType().GetProperties();
            var targetProperties = typeof(SystemUsersEntity).GetProperties();

            foreach (var prop in properties)
            {
                var targetProp = targetProperties.FirstOrDefault(tp => tp.Name.Equals(prop.Name, StringComparison.OrdinalIgnoreCase));
                if (targetProp != null && targetProp.CanWrite)
                {
                    var value = prop.GetValue(data);
                    if (value != null)
                    {
                        targetProp.SetValue(systemUsers, value);
                    }
                }
            }

            return systemUsers;
        }
    }
}
