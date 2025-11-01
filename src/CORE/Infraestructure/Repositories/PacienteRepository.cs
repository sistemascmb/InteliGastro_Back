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

namespace Infraestructure.Repositories
{
    public class PacienteRepository : BaseCrudRepository<PacienteEntity, long>, IPacienteRepository
    {
        private readonly IDapperWrapper _dapperWrapper;
        public PacienteRepository(
            IConfiguration configuration,
            ILogger<PacienteRepository> logger,
            IDapperWrapper dapperWrapper) : base(configuration, logger, "pacient")
        {
            _dapperWrapper = dapperWrapper ?? throw new ArgumentNullException(nameof(dapperWrapper));
        }

        public async Task<object?> GetByConditionAsync(string documentNumber)
        {
            try
            {
                _logger.LogInformation("Buscando paciente por número de documento: {DocumentNumber}", documentNumber);
                
                var whereClause = "\"DocumentNumber\" = @DocumentNumber AND \"IsDeleted\" = false";
                var parameters = new { DocumentNumber = documentNumber };
                
                var result = await GetSingleByConditionAsync(whereClause, parameters);
                
                _logger.LogInformation("Resultado de la búsqueda para el número de documento {DocumentNumber}: {Found}", 
                    documentNumber, result != null ? "Encontrado" : "No encontrado");
                
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar paciente por número de documento: {DocumentNumber}", documentNumber);
                throw;
            }
        }

        public async Task<long> CreatePacienteAsync(object PacienteData)
        {
            var PacienteEntity = ConvertToPacienteEntity(PacienteData);
            return await CreateAsync(PacienteEntity);
        }

        public Task<IEnumerable<object>> GetAllPacienteAsync()
        {
            Task<IEnumerable<PacienteEntity>> Paciente = GetAllAsync();
            return Paciente.ContinueWith(t => t.Result.Cast<object>());
        }

        public async Task<object?> GetPacienteByIdAsync(long PacienteId)
        {
            var Paciente = await GetByIdAsync(PacienteId);
            return Paciente;
        }

        public async Task<bool> UpdatePacienteAsync(object PacienteData)
        {
            var PacienteEntity = ConvertToPacienteEntity(PacienteData);
            return await UpdateAsync(PacienteEntity);
        }

        private PacienteEntity ConvertToPacienteEntity(object data)
        {
            if (data is PacienteEntity entity)
                return entity;
            var Paciente = new PacienteEntity();
            var properties = data.GetType().GetProperties();
            var targetProperties = typeof(PacienteEntity).GetProperties();
            foreach (var prop in properties)
            {
                var targetProp = targetProperties.FirstOrDefault(p => p.Name.Equals(prop.Name, StringComparison.OrdinalIgnoreCase)
                                                                     && p.PropertyType == prop.PropertyType);
                if (targetProp != null)
                {
                    targetProp.SetValue(Paciente, prop.GetValue(data));
                }
            }
            return Paciente;
        }

        public async Task<IEnumerable<object>> SearchPacientesAsync(string? documentNumber, string? names, string? lastNames)
        {
            try
            {
                _logger.LogInformation("Iniciando búsqueda de pacientes con filtros");

                var whereConditions = new List<string>();
                var parameters = new DynamicParameters();

                if (!string.IsNullOrWhiteSpace(documentNumber))
                {
                    whereConditions.Add("\"DocumentNumber\" ILIKE @DocumentNumber");
                    parameters.Add("DocumentNumber", $"%{documentNumber}%");
                }

                if (!string.IsNullOrWhiteSpace(names))
                {
                    whereConditions.Add("\"Names\" ILIKE @Names");
                    parameters.Add("Names", $"%{names}%");
                }

                if (!string.IsNullOrWhiteSpace(lastNames))
                {
                    whereConditions.Add("\"LastNames\" ILIKE @LastNames");
                    parameters.Add("LastNames", $"%{lastNames}%");
                }

                whereConditions.Add("\"IsDeleted\" = false");

                var whereClause = $"({string.Join(" OR ", whereConditions.Take(whereConditions.Count - 1))}) AND {whereConditions.Last()}";

                return await GetByConditionAsync(whereClause, parameters);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar pacientes con los filtros especificados");
                throw;
            }
        }
    }
}
