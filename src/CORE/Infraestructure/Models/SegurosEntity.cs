using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    [Table("insurance")]
    public class SegurosEntity
    {
        [Dapper.Contrib.Extensions.Key]
        public long insuranceid { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public string Identification { get; set; }
        [Column]
        public string Details { get; set; }
        [Column]
        public string Adress { get; set; }
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
