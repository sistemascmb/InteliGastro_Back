using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Infraestructure.Repositories.Base
{
    public interface IBaseCrudRepository<TEntity, TKey> where TEntity : class
    {
        Task<TKey> CreateAsync(TEntity entity);

        /// <summary>
        /// Inserta múltiples entidades
        /// </summary>
        Task<long> CreateRangeAsync(IEnumerable<TEntity> entities);

        // ===== READ =====
        /// <summary>
        /// Obtiene una entidad por su ID
        /// </summary>
        Task<TEntity?> GetByIdAsync(TKey id);

        /// <summary>
        /// Obtiene todas las entidades (usar con cuidado en tablas grandes)
        /// </summary>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Obtiene entidades con una condición WHERE simple
        /// </summary>
        Task<IEnumerable<TEntity>> GetWhereAsync(string whereClause, object? parameters = null);

        /// <summary>
        /// Obtiene una página de entidades
        /// </summary>
        Task<IEnumerable<TEntity>> GetPagedAsync(int page, int pageSize, string? whereClause = null, string? orderBy = null, object? parameters = null);

        /// <summary>
        /// Cuenta el total de registros con una condición WHERE
        /// </summary>
        Task<int> CountAsync(string? whereClause = null, object? parameters = null);

        // ===== UPDATE =====
        /// <summary>
        /// Actualiza una entidad completa
        /// </summary>
        Task<bool> UpdateAsync(TEntity entity);

        /// <summary>
        /// Actualiza múltiples entidades
        /// </summary>
        Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities);

        // ===== DELETE =====
        /// <summary>
        /// Elimina una entidad por ID
        /// </summary>
        Task<bool> DeleteAsync(TKey id);

        /// <summary>
        /// Elimina una entidad
        /// </summary>
        Task<bool> DeleteAsync(TEntity entity);

        /// <summary>
        /// Elimina múltiples entidades
        /// </summary>
        Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities);

        // ===== UTILITIES =====
        /// <summary>
        /// Verifica si existe una entidad con el ID especificado
        /// </summary>
        Task<bool> ExistsAsync(TKey id);

        /// <summary>
        /// Verifica si existe una entidad que cumpla la condición
        /// </summary>
        Task<bool> ExistsWhereAsync(string whereClause, object? parameters = null);
        Task<bool> ExistsAsync(int id);
    }
}
