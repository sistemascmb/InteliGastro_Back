using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    [Table("studies")]
    public class EstudiosEntity
    {
        [Dapper.Contrib.Extensions.Key]
        public long studiesid { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public string Abbreviation { get; set; }
        [Column]
        public string Description { get; set; }
        [Column]
        public int OperatingHours { get; set; }
        [Column]
        public Boolean Status { get; set; }
        [Column]
        public Boolean InformedConsent { get; set; }
        [Column]
        public int CentroId { get; set; }
        [Column]
        public decimal Price { get; set; }
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
