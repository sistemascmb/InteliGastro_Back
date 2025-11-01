using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    [Table("pacient")]
    public class PacienteEntity
    {
        [Dapper.Contrib.Extensions.Key]
        public long pacientid { get; set; }
        [Column]
        public int TypeDoc { get; set; }
        [Column("DocumentNumber")]
        public string DocumentNumber { get; set; }
        [Column]
        public string Names { get; set; }
        [Column]
        public string LastNames { get; set; }
        [Column]
        public DateTime Birthdate { get; set; }
        [Column]
        public int Gender { get; set; }
        [Column]
        public int StatusMarital { get; set; }
        [Column]
        public string Nationality { get; set; }
        [Column]
        public int CentroId { get; set; }
        [Column]
        public string Address { get; set; }
        [Column]
        public int Pais { get; set; }
        [Column]
        public int Department { get; set; }
        [Column]
        public int Province { get; set; }
        [Column]
        public int District { get; set; }
        [Column]
        public string PhoneNumber { get; set; }
        [Column]
        public string Email { get; set; }
        [Column]
        public Boolean Status { get; set; }
        [Column]
        public string MedicalHistory { get; set; }
        [Column]
        public DateTime CreatedAt { get; set; }
        [Column]
        public string CreatedBy { get; set; }
        [Column]
        public DateTime UpdatedAt { get; set; }
        [Column]
        public string UpdatedBy { get; set; }

        [Column("IsDeleted")]
        public Boolean IsDeleted { get; set; }
    }
}
