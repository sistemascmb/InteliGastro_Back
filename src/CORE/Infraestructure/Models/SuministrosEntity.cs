using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    [Table("provision")]
    public class SuministrosEntity
    {
        [Dapper.Contrib.Extensions.Key]
        public long provisionid { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public int Quantity { get; set; }
        [Column]
        public string Comments { get; set; }
        [Column]
        public decimal Price { get; set; }
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
