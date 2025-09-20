namespace WebApi.Application.DTO.MedicoReferencia
{
    public class MedicoReferenciaDto
    {
        public long referraldoctorsd { get; set; }
        public string Names { get; set; }
        public string Surnames { get; set; }
        public int Gender { get; set; }
        public DateTime Date_of_birth { get; set; }
        public string Profession { get; set; }
        public Boolean Status { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
