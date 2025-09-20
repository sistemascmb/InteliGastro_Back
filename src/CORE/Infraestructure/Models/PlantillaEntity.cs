using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    [Table("templates")]
    public class PlantillaEntity
    {
        [Dapper.Contrib.Extensions.Key]
        public long templatesid { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public string Description { get; set; }
        [Column]
        public int? PersonalId { get; set; }
        [Column]
        public int ExamsId { get; set; }
        [Column]
        public string Plantilla { get; set; }
        [Column]
        public bool Status { get; set; }
        [Column]
        public bool AllPersonalMed { get; set; }
        [Column]
        public DateTime CreatedAt { get; set; }
        [Column]
        public string CreatedBy { get; set; }
        [Column]
        public DateTime UpdatedAt { get; set; }
        [Column]
        public string UpdatedBy { get; set; }
        [Column]
        public bool IsDeleted { get; set; }
    }
}
