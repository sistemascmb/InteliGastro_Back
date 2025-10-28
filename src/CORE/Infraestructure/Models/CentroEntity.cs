using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    [Table("centro")]
    public class CentroEntity
    {
        [Dapper.Contrib.Extensions.Key]
        public long centroid { get; set; }
        [Column]
        public string Nombre { get; set; }
        [Column] 
        public string Descripcion { get; set; }
        [Column] 
        public string Abreviatura { get; set; }
        [Column] 
        public string InicioAtencion { get; set; }
        [Column] 
        public string FinAtencion { get; set; }
        [Column] 
        public string Direccion { get; set; }
        [Column] 
        public string Telefono { get; set; }
        [Column] 
        public int Departamento { get; set; }
        [Column] 
        public int Provincia { get; set; }
        [Column] 
        public int Distrito { get; set; }
        [Column] 
        public int Pais { get; set; }
        [Column] 
        public string RUC { get; set; }
        [Column] 
        public Boolean Status { get; set; }
        [Column] 
        public DateTime CreatedAt { get; set; }
        [Column] 
        public string CreatedBy { get; set; }
        [Column] 
        public DateTime UpdatedAt { get; set; }
        [Column] 
        public string UpdatedBy { get; set; }
        [Column] 
        public Boolean IsDeleted { get; set; }
    }
}
