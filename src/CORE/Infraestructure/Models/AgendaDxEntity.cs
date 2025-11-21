using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    [Table("medical_schedule_dx")]

    public class AgendaDxEntity
    {
        [Dapper.Contrib.Extensions.Key]
        public long medicalscheduledxid { get; set; }
        [Column]
        public int Medical_ScheduleId { get; set; }
        [Column]
        public int cie10id { get; set; }
        [Column]
        public string Description { get; set; }
        [Column]
        public DateTime CreatedAt { get; set; }
        [Column]
        public string CreatedBy { get; set; }
        [Column]
        public DateTime UpdatedAt { get; set; }
        [Column]
        public string? UpdatedBy { get; set; }
        [Column]
        public Boolean IsDeleted { get; set; }
    }
}
