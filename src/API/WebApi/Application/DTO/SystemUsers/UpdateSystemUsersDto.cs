using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Application.DTO.SystemUsers
{
    public class UpdateSystemUsersDto
    {
        public long UserId { get; set; }
        public int? profiletypeid { get; set; }
        public int? Personalid { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public string ContraseñaC { get; set; }
        public Boolean Estado { get; set; }

        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }


    }
}
