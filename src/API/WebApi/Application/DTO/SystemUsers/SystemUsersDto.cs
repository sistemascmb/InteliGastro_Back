namespace WebApi.Application.DTO.SystemUsers
{
    public class SystemUsersDto
    {
        //--------------------------------------------------------------
        // Propiedades DTO
        //--------------------------------------------------------------
       
        public long userid { get; set; }

        public int? profiletypeid { get; set; }
        public int? Personalid { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public string ContraseñaC { get; set; }
        public Boolean Estado { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
