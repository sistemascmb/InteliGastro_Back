using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    [Table("cie10")]
    public class Cie10Entity
    {
        [Dapper.Contrib.Extensions.Key]
        public long cieid { get; set; }
        [Column]
        public string Code { get; set; }
        [Column]
        public string Description { get; set; }
        [Column]
        public int GenderId { get; set; }
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
