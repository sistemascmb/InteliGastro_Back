using Dapper;
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
    public class AgendaDxRepository : BaseCrudRepository<AgendaDxEntity, long>, IAgendaDxRepository
    {
        private readonly IDapperWrapper _dapperWrapper;
        public AgendaDxRepository(
            IConfiguration configuration,
            ILogger<AgendaDxRepository> logger,
            IDapperWrapper dapperWrapper) : base(configuration, logger, "medical_schedule_dx")
        {
            _dapperWrapper = dapperWrapper ?? throw new ArgumentNullException(nameof(dapperWrapper));
        }

        public async Task<long> CreateAgendaDxAsync(object AgendaDxData)
        {
            var AgendaDxEntity = ConvertToAgendaDxEntity(AgendaDxData);
            return await CreateAsync(AgendaDxEntity);
        }

        public Task<IEnumerable<object>> GetAllAgendaDxAsync()
        {
            Task<IEnumerable<AgendaDxEntity>> AgendaDx = GetAllAsync();
            return AgendaDx.ContinueWith(t => t.Result.Cast<object>());
        }

        public async Task<object?> GetAgendaDxByIdAsync(long AgendaDxId)
        {
            var AgendaDx = await GetByIdAsync(AgendaDxId);
            return AgendaDx;
        }

        public async Task<bool> UpdateAgendaDxAsync(object AgendaDxData)
        {
            var AgendaDxEntity = ConvertToAgendaDxEntity(AgendaDxData);
            return await UpdateAsync(AgendaDxEntity);
        }

        private AgendaDxEntity ConvertToAgendaDxEntity(object data)
        {
            if (data is AgendaDxEntity entity)
                return entity;
            var AgendaDx = new AgendaDxEntity();
            var properties = data.GetType().GetProperties();
            var targetProperties = typeof(AgendaDxEntity).GetProperties();
            foreach (var prop in properties)
            {
                var targetProp = targetProperties.FirstOrDefault(p => p.Name.Equals(prop.Name, StringComparison.OrdinalIgnoreCase)
                                                                     && p.PropertyType == prop.PropertyType);
                if (targetProp != null)
                {
                    targetProp.SetValue(AgendaDx, prop.GetValue(data));
                }
            }
            return AgendaDx;
        }

        public async Task<IEnumerable<object>> SearchAgendaDxsAsync(string? value1)
        {
            try
            {
                _logger.LogInformation("Iniciando búsqueda de AgendaDxs con filtros");

                var whereConditions = new List<string>();
                var parameters = new DynamicParameters();

                if (!string.IsNullOrWhiteSpace(value1))
                {
                    if (int.TryParse(value1, out var medicalScheduleId))
                    {
                        whereConditions.Add("\"Medical_ScheduleId\" = @Medical_ScheduleId");
                        parameters.Add("Medical_ScheduleId", medicalScheduleId);
                    }
                    else
                    {
                        _logger.LogWarning("Filtro Medical_ScheduleId no numérico: {value}", value1);
                    }
                }

                whereConditions.Add("\"IsDeleted\" = false");

                string whereClause;
                if (whereConditions.Count > 1)
                {
                    var filters = whereConditions.Take(whereConditions.Count - 1);
                    whereClause = $"({string.Join(" OR ", filters)}) AND {whereConditions.Last()}";
                }
                else
                {
                    whereClause = whereConditions.Last();
                }

                return await GetByConditionAsync(whereClause, parameters);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar AgendaDxs con los filtros especificados");
                throw;
            }
        }
    }
}
