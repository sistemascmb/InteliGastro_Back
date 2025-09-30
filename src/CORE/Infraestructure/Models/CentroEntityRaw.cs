using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    /// <summary>
    /// Clase auxiliar para mapear datos de la base de datos con campos problemáticos como string
    /// </summary>
    public class CentroEntityRaw
    {
        public long centroid { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Abreviatura { get; set; }
        public string InicioAtencion { get; set; } // Como string para evitar problemas de conversión
        public string FinAtencion { get; set; } // Como string para evitar problemas de conversión
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int Departamento { get; set; }
        public int Provincia { get; set; }
        public int Distrito { get; set; }
        public int Pais { get; set; }
        public string RUC { get; set; }
        public Boolean Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}