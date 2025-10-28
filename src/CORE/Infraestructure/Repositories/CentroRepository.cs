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
using Dapper;
using Npgsql;

namespace Infraestructure.Repositories
{
    public class CentroRepository : BaseCrudRepository<CentroEntity, long>, ICentroRepository
    {
        public CentroRepository(
            IConfiguration configuration,
            ILogger<CentroRepository> logger,
            IDapperWrapper dapperWrapper) : base(configuration,logger, "centro")
        {
            _dapperWrapper = dapperWrapper ?? throw new ArgumentNullException(nameof(dapperWrapper));
        }
        private readonly IDapperWrapper _dapperWrapper;
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

        public async Task<IEnumerable<object>> GetAllCentro()
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync();

                _logger.LogDebug("Obteniendo todos los centros con consulta personalizada");

                // Consulta SQL personalizada que maneja campos problemáticos como texto
                var sql = @"
                    SELECT 
                        ""centroid"",
                        ""Nombre"",
                        ""Descripcion"",
                        ""Abreviatura"",
                        ""InicioAtencion""::text as ""InicioAtencion"",
                        ""FinAtencion""::text as ""FinAtencion"",
                        ""Direccion"",
                        ""Telefono"",
                        ""Departamento"",
                        ""Provincia"",
                        ""Distrito"",
                        ""Pais"",
                        ""RUC"",
                        ""Status"",
                        ""CreatedAt"",
                        ""CreatedBy"",
                        ""UpdatedAt"",
                        ""UpdatedBy"",
                        ""IsDeleted""
                    FROM centro 
                    WHERE ""IsDeleted"" = false";

                var rawResult = await connection.QueryAsync<CentroEntityRaw>(sql);

                // Convertir CentroEntityRaw a CentroEntity
                var result = rawResult.Select(raw => ConvertRawToCentroEntity(raw));

                _logger.LogDebug("Se obtuvieron {Count} centros", result.Count());
                return result.Cast<object>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo todos los centros");
                throw;
            }
        }

        private CentroEntity ConvertRawToCentroEntity(CentroEntityRaw raw)
        {
            return new CentroEntity
            {
                centroid = raw.centroid,
                Nombre = raw.Nombre,
                Descripcion = raw.Descripcion,
                Abreviatura = raw.Abreviatura,
                InicioAtencion = raw.InicioAtencion,
                FinAtencion = raw.FinAtencion,
                Direccion = raw.Direccion,
                Telefono = raw.Telefono,
                Departamento = raw.Departamento,
                Provincia = raw.Provincia,
                Distrito = raw.Distrito,
                Pais = raw.Pais,
                RUC = raw.RUC,
                Status = raw.Status,
                CreatedAt = raw.CreatedAt,
                CreatedBy = raw.CreatedBy,
                UpdatedAt = raw.UpdatedAt,
                UpdatedBy = raw.UpdatedBy,
                IsDeleted = raw.IsDeleted
            };
        }

        private DateTimeOffset? TryParseTimeToDateTimeOffset(string timeValue)
        {
            if (string.IsNullOrWhiteSpace(timeValue) || timeValue == "PCMB" || timeValue.Any(char.IsLetter))
            {
                return null;
            }

            try
            {
                // Intentar parsear como TimeSpan primero
                if (TimeSpan.TryParse(timeValue, out var timeSpan))
                {
                    // Combinar con la fecha actual
                    var today = DateTime.Today;
                    var dateTime = today.Add(timeSpan);
                    return new DateTimeOffset(dateTime);
                }

                // Si no funciona como TimeSpan, intentar como DateTime
                if (DateTime.TryParse(timeValue, out var dateTime2))
                {
                    return new DateTimeOffset(dateTime2);
                }

                return null;
            }
            catch
            {
                return null;
            }
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
