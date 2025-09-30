using Dapper;
using System.Data;

namespace Infraestructure.TypeHandlers
{
    /// <summary>
    /// TypeHandler personalizado para manejar conversiones seguras de DateTimeOffset
    /// Convierte valores inválidos como "PCMB" a valores por defecto
    /// </summary>
    public class SafeDateTimeOffsetTypeHandler : SqlMapper.TypeHandler<DateTimeOffset>
    {
        public override void SetValue(IDbDataParameter parameter, DateTimeOffset value)
        {
            parameter.Value = value;
        }

        public override DateTimeOffset Parse(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return DateTimeOffset.MinValue;
            }

            // Si es una cadena, intentar convertir
            if (value is string stringValue)
            {
                // Si la cadena está vacía o contiene valores inválidos como "PCMB"
                if (string.IsNullOrWhiteSpace(stringValue) || 
                    stringValue.Equals("PCMB", StringComparison.OrdinalIgnoreCase) ||
                    !DateTimeOffset.TryParse(stringValue, out var parsedDate))
                {
                    // Retornar un valor por defecto para valores inválidos
                    return DateTimeOffset.MinValue;
                }
                return parsedDate;
            }

            // Si es un DateTime, convertir a DateTimeOffset
            if (value is DateTime dateTime)
            {
                return new DateTimeOffset(dateTime);
            }

            // Si es un DateTimeOffset, retornar directamente
            if (value is DateTimeOffset dateTimeOffset)
            {
                return dateTimeOffset;
            }

            // Para cualquier otro tipo, intentar convertir a string y luego parsear
            try
            {
                var stringRepresentation = value.ToString();
                if (DateTimeOffset.TryParse(stringRepresentation, out var result))
                {
                    return result;
                }
            }
            catch
            {
                // Si falla la conversión, retornar valor por defecto
            }

            // Valor por defecto para casos no manejados
            return DateTimeOffset.MinValue;
        }
    }
}