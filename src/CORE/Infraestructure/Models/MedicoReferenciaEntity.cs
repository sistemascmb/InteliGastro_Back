using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    [Table("referral_doctors")]
    public class MedicoReferenciaEntity
    {
        [Dapper.Contrib.Extensions.Key]
        public long referraldoctorsd { get; set; }
        [Column]
        public string Names { get; set; }
        [Column]
        public string Surnames { get; set; }
        [Column]
        public int Gender { get; set; }
        [Column]
        public DateTime Date_of_birth { get; set; }
        [Column]
        public string Profession { get; set; }
        [Column]
        public Boolean Status { get; set; }
        [Column]
        public string PhoneNumber { get; set; }
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
