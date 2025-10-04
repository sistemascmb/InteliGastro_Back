using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Infraestructure.Models
{
    [Dapper.Contrib.Extensions.Table("system_parameter")]
    public class SystemParameterEntity
    {
        [ExplicitKey]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column]
        public long parameterid { get; set; }

        [Column]
        public long groupid { get; set; }

        [Column]
        public string Value1 { get; set; }

        [Column]
        public string Value2 { get; set; }

        [Column]
        public int ParentParameterId { get; set; }

        [Column]
        public int Sort { get; set; }

        [Column]
        public int Version { get; set; }

        [Column]
        public Boolean Estado { get; set; }

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
