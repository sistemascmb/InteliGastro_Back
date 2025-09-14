using Dapper;
using Dapper.Contrib.Extensions;
using Npgsql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Infraestructure.Helpers;

namespace Infraestructure.Repositories.Base
{
    public abstract class BaseCrudRepository<TEntity, TKey> : IBaseCrudRepository<TEntity, TKey>
        where TEntity : class
    {
        protected readonly string _connectionString;
        protected readonly ILogger _logger;
        protected readonly string _tableName;

        protected BaseCrudRepository(IConfiguration configuration, ILogger logger, string tableName)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' no encontrado");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _tableName = tableName;
        }

        // ===== CREATE =====
        public virtual async Task<TKey> CreateAsync(TEntity entity)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync();

                _logger.LogInformation("Creando nueva entidad en tabla {TableName}", _tableName);

                // Convertir DateTimeOffset a UTC para PostgreSQL
                DateTimeOffsetHelper.ConvertDateTimeOffsetsToUtc(entity);

                var id = await connection.InsertAsync(entity);

                _logger.LogInformation("Entidad creada exitosamente con ID: {Id}", id);
                return (TKey)Convert.ChangeType(id, typeof(TKey));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creando entidad en tabla {TableName}", _tableName);
                throw;
            }
        }

        public virtual async Task<long> CreateRangeAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync();

                _logger.LogInformation("Creando múltiples entidades en tabla {TableName}", _tableName);

                var result = await connection.InsertAsync(entities);

                _logger.LogInformation("Entidades creadas exitosamente. Resultado: {Result}", result);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creando múltiples entidades en tabla {TableName}", _tableName);
                throw;
            }
        }

        // ===== READ =====
        public virtual async Task<TEntity?> GetByIdAsync(TKey id)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync();

                _logger.LogDebug("Obteniendo entidad por ID: {Id} de tabla {TableName}", id, _tableName);

                var result = await connection.GetAsync<TEntity>(id);

                if (result != null)
                    _logger.LogDebug("Entidad encontrada con ID: {Id}", id);
                else
                    _logger.LogDebug("No se encontró entidad con ID: {Id}", id);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo entidad por ID: {Id} de tabla {TableName}", id, _tableName);
                throw;
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync();

                _logger.LogDebug("Obteniendo todas las entidades de tabla {TableName}", _tableName);

                var result = await connection.GetAllAsync<TEntity>();

                _logger.LogDebug("Se obtuvieron {Count} entidades de tabla {TableName}", result.Count(), _tableName);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo todas las entidades de tabla {TableName}", _tableName);
                throw;
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetByConditionAsync(string whereClause, object? parameters = null)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync();

                var query = $"SELECT * FROM {_tableName} WHERE {whereClause}";
                _logger.LogDebug("Ejecutando consulta: {Query} en tabla {TableName}", query, _tableName);

                var result = await connection.QueryAsync<TEntity>(query, parameters);

                _logger.LogDebug("Se obtuvieron {Count} entidades con condición: {WhereClause}", result.Count(), whereClause);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo entidades con condición: {WhereClause} de tabla {TableName}", whereClause, _tableName);
                throw;
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetWhereAsync(string whereClause, object? parameters = null)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync();

                var sql = $"SELECT * FROM {_tableName} {whereClause}";
                _logger.LogDebug("Ejecutando consulta WHERE: {SQL}", sql);

                var result = await connection.QueryAsync<TEntity>(sql, parameters);

                _logger.LogDebug("Entidades obtenidas con WHERE: {Count}", result.Count());
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ejecutando consulta WHERE en tabla {TableName}: {WhereClause}", _tableName, whereClause);
                throw;
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetPagedAsync(int page, int pageSize, string? whereClause = null, string? orderBy = null, object? parameters = null)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync();

                var offset = (page - 1) * pageSize;
                var where = string.IsNullOrWhiteSpace(whereClause) ? "" : whereClause;
                var order = string.IsNullOrWhiteSpace(orderBy) ? "ORDER BY (SELECT NULL)" : orderBy;

                var sql = $@"
                    SELECT * FROM {_tableName} 
                    {where} 
                    {order}
                    LIMIT @pageSize OFFSET @offset";

                var allParameters = new DynamicParameters(parameters);
                allParameters.Add("@offset", offset);
                allParameters.Add("@pageSize", pageSize);

                _logger.LogDebug("Ejecutando consulta paginada: {SQL}", sql);

                var result = await connection.QueryAsync<TEntity>(sql, allParameters);

                _logger.LogDebug("Entidades paginadas obtenidas: {Count} (Página {Page})", result.Count(), page);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error ejecutando consulta paginada en tabla {TableName}", _tableName);
                throw;
            }
        }

        // ===== UPDATE =====
        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync();

                _logger.LogInformation("Actualizando entidad en tabla {TableName}", _tableName);

                // Convertir DateTimeOffset a UTC para PostgreSQL
                DateTimeOffsetHelper.ConvertDateTimeOffsetsToUtc(entity);

                var result = await connection.UpdateAsync(entity);

                if (result)
                    _logger.LogInformation("Entidad actualizada exitosamente en tabla {TableName}", _tableName);
                else
                    _logger.LogWarning("No se pudo actualizar la entidad en tabla {TableName}", _tableName);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error actualizando entidad en tabla {TableName}", _tableName);
                throw;
            }
        }

        public virtual async Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync();

                _logger.LogInformation("Actualizando múltiples entidades en tabla {TableName}", _tableName);

                var result = await connection.UpdateAsync(entities);

                if (result)
                    _logger.LogInformation("Entidades actualizadas exitosamente en tabla {TableName}", _tableName);
                else
                    _logger.LogWarning("No se pudieron actualizar las entidades en tabla {TableName}", _tableName);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error actualizando múltiples entidades en tabla {TableName}", _tableName);
                throw;
            }
        }

        // ===== DELETE =====
        public virtual async Task<bool> DeleteAsync(TKey id)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync();

                _logger.LogInformation("Eliminando entidad con ID: {Id} de tabla {TableName}", id, _tableName);

                var entity = await connection.GetAsync<TEntity>(id);
                if (entity == null)
                {
                    _logger.LogWarning("No se encontró entidad con ID: {Id} para eliminar", id);
                    return false;
                }

                var result = await connection.DeleteAsync(entity);

                if (result)
                    _logger.LogInformation("Entidad eliminada exitosamente con ID: {Id}", id);
                else
                    _logger.LogWarning("No se pudo eliminar la entidad con ID: {Id}", id);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error eliminando entidad con ID: {Id} de tabla {TableName}", id, _tableName);
                throw;
            }
        }

        public virtual async Task<bool> DeleteAsync(TEntity entity)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync();

                _logger.LogInformation("Eliminando entidad de tabla {TableName}", _tableName);

                var result = await connection.DeleteAsync(entity);

                if (result)
                    _logger.LogInformation("Entidad eliminada exitosamente de tabla {TableName}", _tableName);
                else
                    _logger.LogWarning("No se pudo eliminar la entidad de tabla {TableName}", _tableName);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error eliminando entidad de tabla {TableName}", _tableName);
                throw;
            }
        }

        public virtual async Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync();

                _logger.LogInformation("Eliminando múltiples entidades de tabla {TableName}", _tableName);

                var result = await connection.DeleteAsync(entities);

                if (result)
                    _logger.LogInformation("Entidades eliminadas exitosamente de tabla {TableName}", _tableName);
                else
                    _logger.LogWarning("No se pudieron eliminar las entidades de tabla {TableName}", _tableName);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error eliminando múltiples entidades de tabla {TableName}", _tableName);
                throw;
            }
        }

        // ===== COUNT =====
        public virtual async Task<int> CountAsync(string? whereClause = null, object? parameters = null)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync();

                var where = string.IsNullOrWhiteSpace(whereClause) ? "" : $"WHERE {whereClause}";
                var sql = $"SELECT COUNT(*) FROM {_tableName} {where}";
                _logger.LogDebug("Ejecutando conteo: {SQL}", sql);

                var count = await connection.QuerySingleAsync<int>(sql, parameters);

                _logger.LogDebug("Total de entidades en tabla {TableName}: {Count}", _tableName, count);
                return count;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error contando entidades en tabla {TableName}", _tableName);
                throw;
            }
        }

        public virtual async Task<int> CountByConditionAsync(string whereClause, object? parameters = null)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync();

                var query = $"SELECT COUNT(*) FROM {_tableName} WHERE {whereClause}";
                _logger.LogDebug("Contando entidades con condición: {WhereClause} en tabla {TableName}", whereClause, _tableName);

                var result = await connection.QuerySingleAsync<int>(query, parameters);

                _logger.LogDebug("Total de entidades con condición en tabla {TableName}: {Count}", _tableName, result);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error contando entidades con condición: {WhereClause} en tabla {TableName}", whereClause, _tableName);
                throw;
            }
        }

        // ===== EXISTS =====
        public virtual async Task<bool> ExistsAsync(TKey id)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync();

                _logger.LogDebug("Verificando existencia de entidad con ID: {Id} en tabla {TableName}", id, _tableName);

                var entity = await connection.GetAsync<TEntity>(id);
                var exists = entity != null;

                _logger.LogDebug("Entidad con ID: {Id} {ExistsText} en tabla {TableName}", id, exists ? "existe" : "no existe", _tableName);
                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verificando existencia de entidad con ID: {Id} en tabla {TableName}", id, _tableName);
                throw;
            }
        }

        public virtual async Task<bool> ExistsWhereAsync(string whereClause, object? parameters = null)
        {
            try
            {
                using var connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync();

                var sql = $"SELECT COUNT(1) FROM {_tableName} WHERE {whereClause}";
                _logger.LogDebug("Verificando existencia con WHERE: {SQL}", sql);

                var count = await connection.QuerySingleAsync<int>(sql, parameters);
                var exists = count > 0;

                _logger.LogDebug("Entidad existe con condición {WhereClause}: {Exists}", whereClause, exists);
                return exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verificando existencia con WHERE en tabla {TableName}: {WhereClause}", _tableName, whereClause);
                throw;
            }
        }

        public Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

