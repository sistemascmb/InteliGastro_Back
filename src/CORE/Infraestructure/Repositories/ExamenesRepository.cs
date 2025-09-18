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
    public class ExamenesRepository : BaseCrudRepository<ExamenesEntity, long>, IExamenesRepository
    {
        private readonly IDapperWrapper _dapperWrapper;

        public ExamenesRepository(
            IConfiguration configuration,
            ILogger<SalasRepository> logger,
            IDapperWrapper dapperWrapper) : base(configuration, logger, "exams")
        {
            _dapperWrapper = dapperWrapper ?? throw new ArgumentNullException(nameof(dapperWrapper));
        }
        public async Task<long> CreateExamenesAsync(object examenesData)
        {
           var examenesEntity = ConvertToExamenes(examenesData);
            return await CreateAsync(examenesEntity);
        }

        public Task<IEnumerable<object>> GetAllExamenesAsync()
        {
            Task<IEnumerable<ExamenesEntity>> examenes = GetAllAsync();
            return examenes.ContinueWith(t => t.Result.Cast<object>());
        }

        public async Task<object?> GetExamenesByIdAsync(long examenesId)
        {
            var examenes = await GetByIdAsync(examenesId);
            return examenes;
        }

        public async Task<bool> UpdateExamenesAsync(object examenesData)
        {
            var examenesEntity = ConvertToExamenes(examenesData);
            return await UpdateAsync(examenesEntity);
        }

        private ExamenesEntity ConvertToExamenes(object data)
        {
            if (data is ExamenesEntity entity)
                return entity;
            var salas = new ExamenesEntity();
            var properties = data.GetType().GetProperties();
            var targetProperties = typeof(ExamenesEntity).GetProperties();
            foreach (var prop in properties)
            {
                var targetProp = targetProperties.FirstOrDefault(p => p.Name.Equals(prop.Name, StringComparison.OrdinalIgnoreCase)
                                                                     && p.PropertyType == prop.PropertyType);
                if (targetProp != null)
                {
                    targetProp.SetValue(salas, prop.GetValue(data));
                }
            }
            return salas;
        }
    }
}
