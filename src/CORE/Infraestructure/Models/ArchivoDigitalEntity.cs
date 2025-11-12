using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    [Table("digital_file")]
    public class ArchivoDigitalEntity
    {
        [Dapper.Contrib.Extensions.Key]
        public long digitalfileid { get; set; }
        [Column]
        public DateTime Date { get; set; }
        [Column]
        public string Hour { get; set; }
        [Column]
        public string Desktop { get; set; }
        [Column]
        public byte[] Archive { get; set; }
        [Column]
        public string Description { get; set; }
        [Column]
        public string TypeArchive { get; set; }
        [Column]
        public int Medical_ScheduleId { get; set; }
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
