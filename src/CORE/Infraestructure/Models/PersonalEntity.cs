using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    [Table("personal")]
    public class PersonalEntity
    {
        [Dapper.Contrib.Extensions.Key]
        public long personalid { get; set; }
        [Column]
        public int CentroId { get; set; }
        [Column]
        public string Documento { get; set; }
        [Column]
        public string Nombres { get; set; }
        [Column]
        public string Apellidos { get; set; }
        [Column]
        public DateTime FecNac { get; set; }
        [Column]
        public string Genero { get; set; }
        [Column]
        public string Telefono { get; set; }
        [Column]
        public string Celular { get; set; }
        [Column]
        public string Correo { get; set; }
        [Column]
        public string Direccion { get; set; }
        [Column]
        public Boolean Estado { get; set; }
        [Column]
        public string Titulo { get; set; }
        [Column]
        public string Grado { get; set; }
        [Column]
        public string NLicencia { get; set; }
        [Column]
        public string TipoTrabajo { get; set; }
        [Column]
        public int Departamento { get; set; }
        [Column]
        public int Provincia { get; set; }
        [Column]
        public int Distrito { get; set; }
        [Column]
        public int Pais { get; set; }
        [Column]
        public int TipoDoc { get; set; }
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
        [Column]
        public string Photo { get; set; }
        [Column]
        public string Firma { get; set; }
        [Column]
        public string CabeceraPlantilla { get; set; }
    }
}
