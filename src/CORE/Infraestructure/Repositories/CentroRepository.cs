using Domain.DomainInterfaces;
using Infraestructure.Models;
using Infraestructure.Persistence;
using Infraestructure.Repositories.Base;
using Infraestructure.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class CentroRepository : BaseCrudRepository<CentroEntity, long>, ICentroRepository
    {
        private readonly IDapperWrapper _dapperWrapper;

        public CentroRepository(
            IConfiguration configuration,
            ILogger<CentroRepository> logger,
            IDapperWrapper dapperWrapper) : base(configuration,logger, "centro")
        {
            _dapperWrapper = dapperWrapper ?? throw new ArgumentNullException(nameof(dapperWrapper));
        }
        
        public async Task<long> CreateCentroAsync(object centroData)
        {
            var centroEntity = ConvertToCentroEntity(centroData);
            // Convertir DateTimeOffset a UTC para PostgreSQL
            DateTimeOffsetHelper.ConvertDateTimeOffsetsToUtc(centroEntity);
            return await CreateAsync(centroEntity);

        }      
        public async Task<object?> GetCentroByIdAsync(long centroId)
        {
            var centro = await GetByIdAsync(centroId);
            return centro;
        }

        public Task<bool> UpdateCentroAsync(object centroData)
        {
            var centroEntity = ConvertToCentroEntity(centroData);
            // Convertir DateTimeOffset a UTC para PostgreSQL
            DateTimeOffsetHelper.ConvertDateTimeOffsetsToUtc(centroEntity);
            return UpdateAsync(centroEntity);
        }
        // ===== MÉTODOS AUXILIARES =====
        //METODO: Implementar AutoMapper o similar para mapeo automático
        private CentroEntity ConvertToCentroEntity(object data)
        {
            if (data is CentroEntity entity)
                return entity;
            
            var centro = new CentroEntity();

            var properties = data.GetType().GetProperties();
            var targetProperties = typeof(CentroEntity).GetProperties();

            foreach (var prop in properties)
            {
                var targetProp = targetProperties.FirstOrDefault(tp => tp.Name.Equals(prop.Name, StringComparison.OrdinalIgnoreCase));
                if (targetProp != null && targetProp.CanWrite)
                {
                    var value = prop.GetValue(data);
                    if (value != null)
                    {
                        targetProp.SetValue(centro, value);
                    }
                }
            }

            return centro;
        }
    }
}
