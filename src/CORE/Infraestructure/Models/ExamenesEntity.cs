using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    [Table("exams")]
    public class ExamenesEntity
    {
        [Dapper.Contrib.Extensions.Key]
        public long examsid { get; set; }
        [Column]
        public string Description { get; set; }
        [Column]
        public string Abbreviation { get; set; }
        [Column]
        public Boolean Status { get; set; }
        [Column]
        public int Type { get; set; }
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
