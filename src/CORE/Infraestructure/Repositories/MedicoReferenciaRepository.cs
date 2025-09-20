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
    public class MedicoReferenciaRepository : BaseCrudRepository<MedicoReferenciaEntity, long>, IMedicoReferenciaRepository
    {
        private readonly IDapperWrapper _dapperWrapper;

        public MedicoReferenciaRepository(
            IConfiguration configuration,
            ILogger<MedicoReferenciaRepository> logger,
            IDapperWrapper dapperWrapper) : base(configuration, logger, "referral_doctors")
        {
            _dapperWrapper = dapperWrapper ?? throw new ArgumentNullException(nameof(dapperWrapper));
        }
        public async Task<long> CreateMedicoReferenciaAsync(object MedicoReferenciaData)
        {
            var MedicoReferenciaEntity = ConvertToMedicoReferenciaEntity(MedicoReferenciaData);
            return await CreateAsync(MedicoReferenciaEntity);
        }

        public Task<IEnumerable<object>> GetAllMedicoReferenciaAsync()
        {
            Task<IEnumerable<MedicoReferenciaEntity>> MedicoReferencia = GetAllAsync();
            return MedicoReferencia.ContinueWith(t => t.Result.Cast<object>());
        }

        public async Task<object?> GetMedicoReferenciaByIdAsync(long MedicoReferenciaId)
        {
            var MedicoReferencia = await GetByIdAsync(MedicoReferenciaId);
            return MedicoReferencia;
        }

        public async Task<bool> UpdateMedicoReferenciaAsync(object MedicoReferenciaData)
        {
            var MedicoReferenciaEntity = ConvertToMedicoReferenciaEntity(MedicoReferenciaData);
            return await UpdateAsync(MedicoReferenciaEntity);
        }

        private MedicoReferenciaEntity ConvertToMedicoReferenciaEntity(object data)
        {
            if (data is MedicoReferenciaEntity entity)
                return entity;
            var MedicoReferencia = new MedicoReferenciaEntity();
            var properties = data.GetType().GetProperties();
            var targetProperties = typeof(MedicoReferenciaEntity).GetProperties();
            foreach (var prop in properties)
            {
                var targetProp = targetProperties.FirstOrDefault(p => p.Name.Equals(prop.Name, StringComparison.OrdinalIgnoreCase)
                                                                     && p.PropertyType == prop.PropertyType);
                if (targetProp != null)
                {
                    targetProp.SetValue(MedicoReferencia, prop.GetValue(data));
                }
            }
            return MedicoReferencia;
        }
    }
}
