namespace WebApi.Application.DTO.MedicoReferencia
{
    public class UpdateMedicoReferenciaDto
    {
        public long referraldoctorsd { get; set; }
        public string Names { get; set; }
        public string Surnames { get; set; }
        public int Gender { get; set; }
        public DateTime Date_of_birth { get; set; }
        public string Profession { get; set; }
        public Boolean Status { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
