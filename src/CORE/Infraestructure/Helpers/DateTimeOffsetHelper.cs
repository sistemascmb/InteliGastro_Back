using System;

namespace Infraestructure.Helpers
{
    public static class DateTimeOffsetHelper
    {
        /// <summary>
        /// Convierte un DateTimeOffset a UTC para compatibilidad con PostgreSQL
        /// </summary>
        /// <param name="dateTimeOffset">El DateTimeOffset a convertir</param>
        /// <returns>DateTimeOffset en UTC</returns>
        public static DateTimeOffset ToUtc(this DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.ToUniversalTime();
        }

        /// <summary>
        /// Convierte un DateTimeOffset nullable a UTC para compatibilidad con PostgreSQL
        /// </summary>
        /// <param name="dateTimeOffset">El DateTimeOffset nullable a convertir</param>
        /// <returns>DateTimeOffset nullable en UTC</returns>
        public static DateTimeOffset? ToUtc(this DateTimeOffset? dateTimeOffset)
        {
            return dateTimeOffset?.ToUniversalTime();
        }

        /// <summary>
        /// Convierte un objeto con propiedades DateTimeOffset a UTC
        /// </summary>
        /// <typeparam name="T">Tipo del objeto</typeparam>
        /// <param name="obj">Objeto a convertir</param>
        /// <returns>Objeto con DateTimeOffset convertidos a UTC</returns>
        public static T ConvertDateTimeOffsetsToUtc<T>(T obj) where T : class
        {
            if (obj == null) return obj;

            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(DateTimeOffset))
                {
                    var value = (DateTimeOffset)property.GetValue(obj);
                    property.SetValue(obj, value.ToUniversalTime());
                }
                else if (property.PropertyType == typeof(DateTimeOffset?))
                {
                    var value = (DateTimeOffset?)property.GetValue(obj);
                    if (value.HasValue)
                    {
                        property.SetValue(obj, value.Value.ToUniversalTime());
                    }
                }
            }

            return obj;
        }
    }
}