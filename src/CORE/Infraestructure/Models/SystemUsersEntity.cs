using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    [Table("system_users")]

    public class SystemUsersEntity
    {
        [Dapper.Contrib.Extensions.Key]
        [Column]
        public long userid { get; set; }
        [Column]
        public long? profiletypeid { get; set; }
        [Column]
        public long? Personalid { get; set; }
        [Column]
        public string Usuario { get; set; }
        [Column]
        public string Contraseña { get; set; }
        [Column]
        public string ContraseñaC { get; set; }
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
