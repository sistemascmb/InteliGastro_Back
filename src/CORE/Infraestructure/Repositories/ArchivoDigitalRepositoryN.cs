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
    public class ArchivoDigitalRepositoryN : BaseCrudRepository<ArchivoDigitalEntity, long>, IArchivoDigitalRepositoryN
    {
        private readonly IDapperWrapper _dapperWrapper;
        public ArchivoDigitalRepositoryN(
            IConfiguration configuration,
            ILogger<ArchivoDigitalRepositoryN> logger,
            IDapperWrapper dapperWrapper) : base(configuration, logger, "digital_file")
        {
            _dapperWrapper = dapperWrapper ?? throw new ArgumentNullException(nameof(dapperWrapper));
        }       

        public async Task<long> CreateArchivoDigitalAsync(object ArchivoDigitalData)
        {
            var ArchivoDigitalEntity = ConvertToArchivoDigitalEntity(ArchivoDigitalData);
            return await CreateAsync(ArchivoDigitalEntity);
        }

        public Task<IEnumerable<object>> GetAllArchivoDigitalAsync()
        {
            Task<IEnumerable<ArchivoDigitalEntity>> ArchivoDigital = GetAllAsync();
            return ArchivoDigital.ContinueWith(t => t.Result.Cast<object>());
        }

        public async Task<object?> GetArchivoDigitalByIdAsync(long ArchivoDigitalId)
        {
            var ArchivoDigital = await GetByIdAsync(ArchivoDigitalId);
            return ArchivoDigital;
        }

        public async Task<bool> UpdateArchivoDigitalAsync(object ArchivoDigitalData)
        {
            var ArchivoDigitalEntity = ConvertToArchivoDigitalEntity(ArchivoDigitalData);
            return await UpdateAsync(ArchivoDigitalEntity);
        }

        private ArchivoDigitalEntity ConvertToArchivoDigitalEntity(object data)
        {
            if (data is ArchivoDigitalEntity entity)
                return entity;
            var ArchivoDigital = new ArchivoDigitalEntity();
            var properties = data.GetType().GetProperties();
            var targetProperties = typeof(ArchivoDigitalEntity).GetProperties();
            foreach (var prop in properties)
            {
                var targetProp = targetProperties.FirstOrDefault(p => p.Name.Equals(prop.Name, StringComparison.OrdinalIgnoreCase)
                                                                     && p.PropertyType == prop.PropertyType);
                if (targetProp != null)
                {
                    targetProp.SetValue(ArchivoDigital, prop.GetValue(data));
                }
            }
            return ArchivoDigital;
        }

        public async Task<IEnumerable<object>> SearchArchivoDigitalsAsync(string? value1, string? value2, string? value3)
        {
            try
            {
                _logger.LogInformation("Iniciando búsqueda de ArchivoDigitals con filtros");

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
                _logger.LogError(ex, "Error al buscar ArchivoDigitals con los filtros especificados");
                throw;
            }
        }
    }
}
