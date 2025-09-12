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
        public long? ProfileTypeId { get; set; }
        [Column]
        public Boolean IsInternalUser { get; set; }
        [Column]
        public string FirstName { get; set; }
        [Column]
        public string LastName { get; set; }
        [Column]
        public string SecondLastName { get; set; }
        [Column]
        public string Email { get; set; }
        [Column]
        public string ContactNumber { get; set; }
        [Column]
        public string Address { get; set; }
        [Column]
        public DateTime DateOfBirth { get; set; }
        [Column]
        public long? GenderId { get; set; }
        [Column]
        public string ProfilePicture { get; set; }
        [Column]
        public long? DocumentTypeId { get; set; }
        [Column]
        public string DocumentNumber { get; set; }
        [Column]
        public int Version { get; set; }
        [Column]
        public int Estado { get; set; }
        [Column]
        public DateTime FechaCreacion { get; set; }
        [Column]
        public bool RegistroEliminado { get; set; }
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
