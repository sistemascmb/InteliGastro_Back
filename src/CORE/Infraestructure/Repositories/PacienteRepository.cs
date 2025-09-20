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
    public class PacienteRepository : BaseCrudRepository<PacienteEntity, long>, IPacienteRepository
    {
        private readonly IDapperWrapper _dapperWrapper;
        public PacienteRepository(
            IConfiguration configuration,
            ILogger<PacienteRepository> logger,
            IDapperWrapper dapperWrapper) : base(configuration, logger, "studies")
        {
            _dapperWrapper = dapperWrapper ?? throw new ArgumentNullException(nameof(dapperWrapper));
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
    }
}
