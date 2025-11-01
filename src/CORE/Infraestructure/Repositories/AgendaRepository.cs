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
using Dapper;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class AgendaRepository : BaseCrudRepository<AgendaEntity, long>, IAgendaRepository
    {
        private readonly IDapperWrapper _dapperWrapper;
        public AgendaRepository(
            IConfiguration configuration,
            ILogger<AgendaRepository> logger,
            IDapperWrapper dapperWrapper) : base(configuration, logger, "medical_schedule")
        {
            _dapperWrapper = dapperWrapper ?? throw new ArgumentNullException(nameof(dapperWrapper));
        }
        public async Task<long> CreateAgendaAsync(object AgendaData)
        {
            var AgendaEntity = ConvertToAgendaEntity(AgendaData);
            return await CreateAsync(AgendaEntity);
        }

        public Task<IEnumerable<object>> GetAllAgendaAsync()
        {
            Task<IEnumerable<AgendaEntity>> Agenda = GetAllAsync();
            return Agenda.ContinueWith(t => t.Result.Cast<object>());
        }

        public async Task<object?> GetAgendaByIdAsync(long AgendaId)
        {
            var Agenda = await GetByIdAsync(AgendaId);
            return Agenda;
        }

        public async Task<bool> UpdateAgendaAsync(object AgendaData)
        {
            var AgendaEntity = ConvertToAgendaEntity(AgendaData);
            return await UpdateAsync(AgendaEntity);
        }

        private AgendaEntity ConvertToAgendaEntity(object data)
        {
            if (data is AgendaEntity entity)
                return entity;
            var Agenda = new AgendaEntity();
            var properties = data.GetType().GetProperties();
            var targetProperties = typeof(AgendaEntity).GetProperties();
            foreach (var prop in properties)
            {
                var targetProp = targetProperties.FirstOrDefault(p => p.Name.Equals(prop.Name, StringComparison.OrdinalIgnoreCase)
                                                                     && p.PropertyType == prop.PropertyType);
                if (targetProp != null)
                {
                    targetProp.SetValue(Agenda, prop.GetValue(data));
                }
            }
            return Agenda;
        }

        public async Task<IEnumerable<object>> SearchAgendaByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                _logger.LogInformation("Iniciando búsqueda de agenda por rango de fechas: {StartDate} - {EndDate}", startDate, endDate);

                var parameters = new DynamicParameters();
                parameters.Add("@StartDate", startDate.Date);
                parameters.Add("@EndDate", endDate.Date.AddDays(1));

                var whereClause = "\"AppointmentDate\" >= @StartDate AND \"AppointmentDate\" < @EndDate AND \"IsDeleted\" = false";
                _logger.LogInformation("Consulta WHERE: {WhereClause}", whereClause);

                return await GetByConditionAsync(whereClause, parameters);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar agenda por rango de fechas: {StartDate} - {EndDate}", startDate, endDate);
                throw;
            }
        }
    }
}
