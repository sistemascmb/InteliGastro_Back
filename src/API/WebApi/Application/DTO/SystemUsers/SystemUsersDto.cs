namespace WebApi.Application.DTO.SystemUsers
{
    public class SystemUsersDto
    {
        //--------------------------------------------------------------
        // Propiedades DTO
        //--------------------------------------------------------------
       
        public long userid { get; set; }

        public long AddressUserId { get; set; }
        public long? ProfileTypeId { get; set; }
        public Boolean IsInternalUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public long? GenderId { get; set; }
        public string ProfilePicture { get; set; }
        public long? DocumentTypeId { get; set; }
        public string DocumentNumber { get; set; }
        public int Version { get; set; }
        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool RegistroEliminado { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
